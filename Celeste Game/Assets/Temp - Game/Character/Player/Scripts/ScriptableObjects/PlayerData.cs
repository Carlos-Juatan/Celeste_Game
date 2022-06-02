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
        [SerializeField] float _groundPointsRadius; // 0.15f
        [SerializeField] Vector2 _groundPointsOffset; // x = 0.35, y = -2
        [SerializeField] Color _groundPointsColor = Color.green;
        [SerializeField] bool _showGroundPointsOnGizmos;
        [SerializeField] LayerMask _groundLayers;

        [Header("Roof Edge Correction")]
        [SerializeField] Vector2 _roofPointsSize; // x = 0.46, y = 0.2
        [SerializeField] Vector2 _roofPointsOffset; // x = 0.26, y = -0.15
        [SerializeField] float _distanceForFrame = 0.125f;
        [SerializeField] Color _roofPointsColor = Color.blue;
        [SerializeField] bool _showRoofPointsOnGizmos;
        [SerializeField] LayerMask _roofLayers;

        [Header("Simulate Outside Forces")]
        [SerializeField] Vector2 _simulateOutsideForce;

        [Header("Move")]
        [SerializeField] float _maxMoveSpeed = 8f;
        [SerializeField] float _moveAcceleration = 1f;
        [SerializeField] float _moveReduce = 2f;
        [SerializeField] float _airMult = .65f;

        [Header("Jump")]
        [SerializeField] float _jumpTime = 0.35f;
        [SerializeField] float _jumpForce = 12f;
        [SerializeField] float _holdJumpTime = 0.05f;
        [SerializeField] float _curtInertiaHightJump = 2f;
        [SerializeField] float _curtInertiaLowJump = 3f;
        [SerializeField] float _resetJumpInertia = 3f;
        [SerializeField] float _coyoteTime = 0.2f;
        [SerializeField] float _jumpInputBuffer = 0.2f;
        [SerializeField] int _jumpCount = 1;

        [Header("Fall")]
        [SerializeField, Range(0, .5f)] float _fallMultiplier = 0.08f;
        [SerializeField] float _jumpVelocityFalloff = 2f;

        // Inputs
        Vector2Int _axisInput;

        // Logics
        bool _hasSpawned = true;
        
        // Ground Check
        bool _isGrounded;
        bool _leftEdgeGrounded;
        bool _rightEdgeGrounded;
        bool _wasGrounded;

        // Ground Check
        //bool _leftEdgeRoof;
        //bool _rightEdgeRoof;

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
        PlayerPhysics _playerPhysics;
        PlayerAnimations _playerAnimations;
#endregion

#region Getters And Setters.
        // Logics
        public bool HasSpawned { get { return _hasSpawned; } set { _hasSpawned = value; } }

        // Ground Check
        public float GroundPointsRadius       { get { return _groundPointsRadius; } }
        public Color GroundPointsColor        { get { return _groundPointsColor; } }
        public Vector2 GroundPointsOffset     { get { return _groundPointsOffset; } }
        public bool ShowGroundPointsOnGizmos  { get { return _showGroundPointsOnGizmos; } }
        public LayerMask GroundLayers         { get { return _groundLayers; } }
        public bool IsGrounded                { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool LeftEdgeGrounded          { get { return _leftEdgeGrounded; } set { _leftEdgeGrounded = value; } }
        public bool RightEdgeGrounded         { get { return _rightEdgeGrounded; } set { _rightEdgeGrounded = value; } }
        public bool WasGrounded               { get { return _wasGrounded; } set { _wasGrounded = value; } }

        // Roof Edge Correction
        public Vector2 RoofPointsSize      { get { return _roofPointsSize; } }
        public Vector2 RoofPointsOffset    { get { return _roofPointsOffset; } }
        public float DistanceForFrame      { get { return _distanceForFrame; } }
        public Color RoofPointsColor       { get { return _roofPointsColor; } }
        public bool ShowRoofPointsOnGizmos { get { return _showRoofPointsOnGizmos; } }
        public LayerMask RoofLayers        { get { return _roofLayers; } }

        // Inputs
        public Vector2Int AxisInput { get { return _axisInput; } set { _axisInput = value; } }

        // Simulate Forces
        public Vector2 SimulateOutsideForce { get { return _simulateOutsideForce; } set { _simulateOutsideForce = value; } }

        // Moving
        public float MaxMoveSpeed     { get { return _maxMoveSpeed; } }
        public float MoveAcceleration { get { return _moveAcceleration; } }
        public float MoveReduce       { get { return _moveReduce; } }
        public float AirMult          { get { return _airMult; } }
        public bool IsMoving          { get { return _isMoving; } set { _isMoving = value; } }
        public int FacingDirection    { get { return _facingDirection; } set { _facingDirection = value; } }

        // Jump
        public float JumpTime             { get { return _jumpTime; } }
        public float JumpForce            { get { return _jumpForce; } }
        public float HoldJumpTime         { get { return _holdJumpTime; } }
        public float CurtInertiaHightJump { get { return _curtInertiaHightJump; } }
        public float CurtInertiaLowJump   { get { return _curtInertiaLowJump; } }
        public float ResetJumpInertia     { get { return _resetJumpInertia; } }
        public float CoyoteTime           { get { return _coyoteTime; } }
        public float JumpInputBuffer      { get { return _jumpInputBuffer; } }
        public int JumpCount              { get { return _jumpCount; } }
        public int CurrentJumpCount       { get { return _currentJumpCount; } set { _currentJumpCount = value; } }
        
        // Fall
        public float FallMultiplier      { get { return _fallMultiplier; } }
        public float JumpVelocityFalloff { get { return _jumpVelocityFalloff; } }

        // Components
        public PlayerBaseState CurrentState      { get { return _currentState; } set { _currentState = value; } }
        public PlayerStateFactory Factory        { get { return _factory; } }
        public SpriteRenderer PlayerRenderer     { get { return _playerRenderer; } set { _playerRenderer = value; } }
        public Rigidbody2D Rigidbody2D           { get { return _rigidbody2D; } }
        public PlayerPhysics PlayerPhysics       { get { return _playerPhysics; } }
        public PlayerAnimations PlayerAnimations { get { return _playerAnimations; } }
        
#endregion

#region Initialize and Find Components.
        public void Initialize(PlayerController currentPlayer){
            _factory = new PlayerStateFactory(currentPlayer);
            _facingDirection = 1;
            _hasSpawned = true;

            FindComponents(currentPlayer);
        }

        // Responsable to find components on start scene
        void FindComponents(PlayerController currentPlayer){
            _playerRenderer = currentPlayer.GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = currentPlayer.GetComponentInChildren<Rigidbody2D>();
            _playerPhysics = currentPlayer.GetComponent<PlayerPhysics>();
            _playerAnimations = currentPlayer.GetComponent<PlayerAnimations>();
        }
#endregion
    }
#endregion
}