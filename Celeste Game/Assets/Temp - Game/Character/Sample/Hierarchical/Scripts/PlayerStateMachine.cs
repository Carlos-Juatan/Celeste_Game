using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Player.Hierarchically
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] Transform _groundCheckCollider;
        [SerializeField] float _groundCheckRadius;
        [SerializeField] LayerMask _groundLayers;

        [Header("Move Speed")]
        [SerializeField] float _moveSpeed = 4f; 

        [Header("Jump ( NOTE: only update on start )")]
        [SerializeField] float _maxJumpHeigth = 4.0f;
        [SerializeField] float _maxJumpTime = .75f;
        
        // Declare reference variables.
        CharacterController _characterController => GetComponent<CharacterController>();
        Animator _animator => GetComponent<Animator>();
        //PlayerInput _playerInput; // NOTE: PlayerInput class must be generate from New Input System In Inspector

        // Animations
        // variables to store optimized setter/getter parameters IDs
        int _isWalkingHash = Animator.StringToHash("isWalking");
        int _isRunningHash = Animator.StringToHash("isRunning");
        int _isJumpingHash = Animator.StringToHash("isJumping");
        int _jumpCountHash = Animator.StringToHash("JumpCount");
        int _isFallingHash = Animator.StringToHash("IsFalling");

        // variables to store player input values
        Vector2 _currentMovimentInput;
        Vector3 _currentMoviment;
        Vector3 _appliedMoviment;
        bool _isMovimentPressed;
        bool _isRunPressed;

        // Constants
        float _rotationFactorPerFrame = 15.0f;
        float _runMultiplier = 4.0f;
        int _zero = 0;

        // Gravity variables
        float _gravity = -9.8f;

        // Jump variables
        bool _isGrounded;
        bool _isJumpPressed = false;
        bool _isJumping = false;
        bool _requireNewJumpPress = false;
        int _jumpCount = 0;
        float _initialJumpVelocity;
        Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
        Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
        Coroutine _currentJumpResetRoutine = null;

        // state varibles
        PlayerBaseState _currentState;
        PlayerStateFactory _states;

        // getters and setters.
        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public Animator Animator { get { return _animator; } }
        public CharacterController CharacterController { get { return _characterController; } }
        public Coroutine CurrentJumpResetRoutine { get { return _currentJumpResetRoutine; } set { _currentJumpResetRoutine = value; } }
        public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
        public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
        public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
        public int IsJumpingHash { get { return _isJumpingHash; } }
        public int JumpCountHash { get { return _jumpCountHash; } }
        public int IsWalkingHash { get { return _isWalkingHash; } }
        public int IsRunningHash { get { return _isRunningHash; } }
        public int IsFallingHash { get { return _isFallingHash; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
        public bool IsJumpPressed { get { return _isJumpPressed; } }
        public bool IsMovimentPressed { get { return _isMovimentPressed; } }
        public bool IsRunPressed { get { return _isRunPressed; } }
        public float Gravity { get { return _gravity; } }
        public float RunMultiplier { get { return _runMultiplier; } }
        public float MoveSpeed { get { return _moveSpeed; } }
        public float CurrentMovimentY { get { return _currentMoviment.y; } set { _currentMoviment.y = value; } }
        public float CurrentMovimentX { get { return _currentMoviment.x; } set { _currentMoviment.x = value; } }
        public float AppliedMovimentY { get { return _appliedMoviment.y; } set { _appliedMoviment.y = value; } }
        public float AppliedMovimentX { get { return _appliedMoviment.x; } set { _appliedMoviment.x = value; } }
        public Vector2 CurrentMovimentInput { get { return _currentMovimentInput; } }

#region Initial Setup.
        void Awake(){
            // setup states
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();

            // setup initial velocity and gravity
            SetupJumpVariables();
        }

        // set the initial velocity and gravity using jump heights and durations
        void SetupJumpVariables(){
            float timeToApex = _maxJumpTime / 2;

            float initialGravity = (-2 * _maxJumpHeigth) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * _maxJumpHeigth) / timeToApex;

            float secondJumpGravity =         (-2 * (_maxJumpHeigth + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
            float secondJumpInitialVelocity = ( 2 * (_maxJumpHeigth + 2)) / (timeToApex * 1.25f);
            float thirdJumpGravity =          (-2 * (_maxJumpHeigth + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
            float thirdJumpInitialVelocity =  ( 2 * (_maxJumpHeigth + 4)) / (timeToApex * 1.5f);
            
            _initialJumpVelocities.Add(1, _initialJumpVelocity);
            _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
            _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

            _jumpGravities.Add(0, initialGravity);
            _jumpGravities.Add(1, initialGravity);
            _jumpGravities.Add(2, secondJumpGravity);
            _jumpGravities.Add(3, thirdJumpGravity);
        }

        void Start() => _characterController.Move(_appliedMoviment * Time.deltaTime);
#endregion

#region Updating.
        void Update(){
            // NOTE: temporary get inputs
            _currentMovimentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //_isMovimentPressed = _currentMovimentInput.x != _zero || _currentMovimentInput.y != _zero;
            _isMovimentPressed = _currentMovimentInput.x != _zero;

            _isJumpPressed = Input.GetButtonDown("Jump");
            if (IsJumpPressed)
                _requireNewJumpPress = false;

            _isRunPressed = Input.GetKey(KeyCode.LeftControl);

            HandleRotation();
            _currentState.UpdateStates();

            //if(_isGrounded){
                //_appliedMoviment.y = 0f;
                //_currentMoviment.y =0f;
            //}
            _characterController.Move(_appliedMoviment * Time.deltaTime);
        }
        
        void FixedUpdate(){
            _currentState.FisicsCalculateState();
            //GroundedCheck();
        }

        void HandleRotation(){
            Vector3 positionToLookAt;
            // the change in position our character should point to
            //positionToLookAt.x = _currentMovimentInput.x;
            positionToLookAt.x = _zero;
            positionToLookAt.y = _zero;
            //positionToLookAt.z = _currentMovimentInput.y;
            positionToLookAt.z = _currentMovimentInput.x;
            // the current rotation of our character
            Quaternion currentRotation = transform.rotation;

            if(_isMovimentPressed)
            {
                // create a new rotation based on where the player is current pressing
                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
                // rotate the character to face the positionToLookAt
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
            }
        }

        void GroundedCheck(){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckCollider.position, _groundCheckRadius, _groundLayers);
            if(colliders.Length > 0)
                _isGrounded = true;
        }
        
#endregion
    }
}