using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player
{
#region Player State.
    public enum PlayerStates{
        Idle,
        Move,
        Grounded,
        Jump,
        Fall
    }

    public class PlayerStateFactory
    {
        PlayerController _player;
        Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

        public PlayerStateFactory(PlayerController currentPlayer){
            _player = currentPlayer;
            _states[PlayerStates.Grounded]  = new PlayerGroundedState(_player);
            _states[PlayerStates.Idle]      = new PlayerIdleState(_player);
            _states[PlayerStates.Move]      = new PlayerMoveState(_player);
            _states[PlayerStates.Jump]      = new PlayerJumpState(_player);
            _states[PlayerStates.Fall]      = new PlayerFallState(_player);
        }

        public PlayerBaseState SelectState(PlayerStates newState){ return _states[newState]; }
    }
#endregion

#region Player Data.
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
#region Var.
        [Header("Ground Check")]
        [Range(0, 1)]
        [SerializeField] float _pointsRadius;
        [Space(5)]
        [SerializeField] Color _leftPointColor = Color.red;
        [SerializeField] Vector2 _leftPointOffset;
        [Space(5)]
        [SerializeField] Color _rightPointColor = Color.blue;
        [SerializeField] Vector2 _rightPointOffset;
        [Space(5)]
        [SerializeField] bool _showPointsOnGizmos;
        [Space(5)]
        [SerializeField] LayerMask _groundLayers;

        [Header("Move")]
        [SerializeField] float _maxMoveSpeed = 15f;
        [SerializeField] float _moveAcceleration = 8f;
        [SerializeField] float _moveReduce = 5f;
        [SerializeField] float _airMult = .65f;

        [Header("Jump")]
        [SerializeField] float _maxJumpHeight = 30f;
        [SerializeField] float _minJumpHeight = 30f;
        [SerializeField] float _timeToTop = 0.5f;
        [SerializeField] int _jumpCount = 1;

        [Header("Fall")]
        [SerializeField, Range(0, .5f)] float _fallMultiplier = 5f;
        [SerializeField] float _jumpVelocityFalloff = 2f;

        // Inputs
        Vector2Int _axisInput;
        bool _jumpInput;

        // Logics
        // Ground Check
        bool _isGrounded;

        // Moving
        bool _isMoving;
        int _facingDirection;

        // Jump
        int _currentJumpCount;

        // Components
        PlayerBaseState _currentState;
        PlayerStateFactory _factory;
        SpriteRenderer _playerRenderer;
        Rigidbody2D _rigidbody2D;
#endregion

#region Getters And Setters.
        // Ground Check
        public float PointsRadius       { get { return _pointsRadius; } }
        public Color LeftPointColor     { get { return _leftPointColor; } }
        public Vector2 LeftPointOffset  { get { return _leftPointOffset; } }
        public Color RightPointColor    { get { return _rightPointColor; } }
        public Vector2 RightPointOffset { get { return _rightPointOffset; } }
        public bool ShowPointsOnGizmos  { get { return _showPointsOnGizmos; } }
        public LayerMask GroundLayers   { get { return _groundLayers; } }
        public bool IsGrounded          { get { return _isGrounded; } set { _isGrounded = value; } }

        // Inputs
        public Vector2Int AxisInput { get { return _axisInput; } set { _axisInput = value; } }
        public bool JumpInput       { get { return _jumpInput; } set { _jumpInput = value; } }

        // Moving
        public float MaxMoveSpeed     { get { return _maxMoveSpeed; } }
        public float MoveAcceleration { get { return _moveAcceleration; } }
        public float MoveReduce       { get { return _moveReduce; } }
        public float AirMult          { get { return _airMult; } }
        public bool IsMoving          { get { return _isMoving; } set { _isMoving = value; } }
        public int FacingDirection    { get { return _facingDirection; } set { _facingDirection = value; } }

        // Jump
        public float MaxJumpHeight  { get { return _maxJumpHeight; } }
        public float MinJumpHeight  { get { return _minJumpHeight; } }
        public float TimeToTop      { get { return _timeToTop; } }
        public int JumpCount        { get { return _jumpCount; } }
        public int CurrentJumpCount { get { return _currentJumpCount; } set { _currentJumpCount = value; } }
        
        // Fall
        public float FallMultiplier      { get { return _fallMultiplier; } }
        public float JumpVelocityFalloff { get { return _jumpVelocityFalloff; } }

        // Components
        public PlayerBaseState CurrentState  { get { return _currentState; } set { _currentState = value; } }
        public PlayerStateFactory Factory    { get { return _factory; } }
        public SpriteRenderer PlayerRenderer { get { return _playerRenderer; } set { _playerRenderer = value; } }
        public Rigidbody2D Rigidbody2D       { get { return _rigidbody2D; } }
        
#endregion

#region Initialize and Find Components.
        public void Initialize(PlayerController currentPlayer){
            _factory = new PlayerStateFactory(currentPlayer);
            _facingDirection = 1;

            FindComponents(currentPlayer);
        }

        // Responsable to find components on start scene
        void FindComponents(PlayerController currentPlayer){
            _playerRenderer = currentPlayer.GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = currentPlayer.GetComponentInChildren<Rigidbody2D>();
        }
#endregion
    }
#endregion
}