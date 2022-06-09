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
        Fall,
        WallSlider,
        WallJump
    }

    public class PlayerStateFactory
    {
        PlayerController _player;
        Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

        public PlayerStateFactory(PlayerController currentPlayer){
            _player = currentPlayer;
            _states[PlayerStates.Grounded]   = new PlayerGroundedState(_player);
            _states[PlayerStates.Idle]       = new PlayerIdleState(_player);
            _states[PlayerStates.Move]       = new PlayerMoveState(_player);
            _states[PlayerStates.Jump]       = new PlayerJumpState(_player);
            _states[PlayerStates.Fall]       = new PlayerFallState(_player);
            _states[PlayerStates.WallSlider] = new PlayerWallSlider(_player);
            _states[PlayerStates.WallJump]   = new PlayerWallJumpState(_player);
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

        [Header("Wall Slider Check")]
        [SerializeField] Vector2 _sideSliderPointSize;
        [SerializeField] Vector2 _sideSliderPointOffset;
        [SerializeField] float _playerYCenterOffset;
        [SerializeField] Color _sideSliderPointsColor = Color.yellow;
        [SerializeField] bool _showSideSliderPointsOnGizmos;
        [SerializeField] LayerMask _wallSliderLayers;

        [Header("Wall Jump Check")]
        [SerializeField] Vector2 _sideJumpPointSize;
        [SerializeField] Vector2 _sideJumpPointOffset;
        [SerializeField] Color _sideJumoPointsColor = new Color(0.6F, 0.3F, 0.08F);
        [SerializeField] bool _showSideJumpPointsOnGizmos;
        [SerializeField] LayerMask _walJumplLayers;

        [Header("Simulate Outside Forces")]
        [SerializeField] Vector2 _simulateOutsideForce;

        [Header("Move")]
        [SerializeField] float _maxMoveSpeed = 8f;
        [SerializeField] float _moveAcceleration = 1f;
        [SerializeField] float _moveReduce = 2f;
        [SerializeField] float _airMult = .65f;

        [Header("Jump")]
        [SerializeField] float _jumpSpeed = 26f; // 12 frames = 28f | 10 frames = 26f
        [SerializeField] float _reduceMultiplier = 1.8f; // 12 frames = 2f | 10 frames = 1.8f
        [SerializeField] float _releaseReduceMult = 8f; // 12 frames = 8f | 10 frames = 26f
        [SerializeField] float _maxReduceSpeed = 26f; // 12 frames = 28f | 10 frames = 26f
        [SerializeField] float _minStayTime = 0.05f;
        [SerializeField] float _coyoteTime = 0.2f;
        [SerializeField] float _jumpInputBuffer = 0.2f;
        [SerializeField] int _jumpCount = 1;

        [Header("Fall")]
        [SerializeField] float _holdingBonusTime = 0.1f;
        [SerializeField] float _fallMultiplier = 3f; // 12 frames = 3f | 10 frames = 2f
        [SerializeField] float _maxFallSpeed = 20f; // 12 frames = 20f | 10 frames = 14f

        [Header("Wall Slider")]
        [SerializeField] float _wallSliderVelocity;

        [Header("Wall Jump")]
        [SerializeField] float _wallJumpForce = 4f;
        [SerializeField] float _wallJumpTime = 0.2f;

        // Inputs
        Vector2Int _axisInput;

        // Logics
        bool _hasSpawned = true;
        
        // Ground Check
        bool _isGrounded;
        bool _leftEdgeGrounded;
        bool _rightEdgeGrounded;
        bool _wasGrounded;

        // Roof Check
        //bool _leftEdgeRoof;
        //bool _rightEdgeRoof;

        // Moving
        bool _isMoving;
        int _facingDirection;

        // Jump
        int _currentJumpCount;

        // Wall Slider
        bool _wallSliderInteract;
        bool _upSideSlideWall;
        bool _downSideSlideWall;

        // Wall Jump
        bool _onWallJump;
        bool _canWallJump;
        bool _leftWallJump;
        bool _rightWallJump;

        // Components
        PlayerBaseState _currentState;
        PlayerStateFactory _factory;
        SpriteRenderer _playerRenderer;
        Rigidbody2D _rigidbody2D;
        PlayerPhysics _playerPhysics;
        PlayerAnimations _playerAnimations;
#endregion

#region Getters And Setters.
        // Inputs
        public Vector2Int AxisInput { get { return _axisInput; } set { _axisInput = value; } }

        // Logics
        public bool HasSpawned { get { return _hasSpawned; } set { _hasSpawned = value; } }

        // Ground Check
        public float GroundPointsRadius      { get { return _groundPointsRadius; } }
        public Color GroundPointsColor       { get { return _groundPointsColor; } }
        public Vector2 GroundPointsOffset    { get { return _groundPointsOffset; } }
        public bool ShowGroundPointsOnGizmos { get { return _showGroundPointsOnGizmos; } }
        public LayerMask GroundLayers        { get { return _groundLayers; } }
        public bool IsGrounded               { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool LeftEdgeGrounded         { get { return _leftEdgeGrounded; } set { _leftEdgeGrounded = value; } }
        public bool RightEdgeGrounded        { get { return _rightEdgeGrounded; } set { _rightEdgeGrounded = value; } }
        public bool WasGrounded              { get { return _wasGrounded; } set { _wasGrounded = value; } }

        // Roof Edge Correction
        public Vector2 RoofPointsSize      { get { return _roofPointsSize; } }
        public Vector2 RoofPointsOffset    { get { return _roofPointsOffset; } }
        public float DistanceForFrame      { get { return _distanceForFrame; } }
        public Color RoofPointsColor       { get { return _roofPointsColor; } }
        public bool ShowRoofPointsOnGizmos { get { return _showRoofPointsOnGizmos; } }
        public LayerMask RoofLayers        { get { return _roofLayers; } }

        // Wall Slider Check
        public Vector2 SideSliderPointSize       { get { return _sideSliderPointSize; } }
        public Vector2 SideSliderPointOffset     { get { return _sideSliderPointOffset; } }
        public float PlayerYCenterOffset         { get { return _playerYCenterOffset; } }
        public Color SideSliderPointsColor       { get { return _sideSliderPointsColor; } }
        public bool ShowSideSliderPointsOnGizmos { get { return _showSideSliderPointsOnGizmos; } }
        public LayerMask WallSliderLayers        { get { return _wallSliderLayers; } }
        public float WallSliderVelocity          { get { return _wallSliderVelocity; } }
        public bool WallSliderInteract           { get { return _wallSliderInteract; } set { _wallSliderInteract = value; } }
        public bool UpSideSlideWall              { get { return _upSideSlideWall; } set { _upSideSlideWall = value; } }
        public bool DownSideSlideWall            { get { return _downSideSlideWall; } set { _downSideSlideWall = value; } }

        // Wall Jump Check
        public Vector2 SideJumpPointSize       { get { return _sideJumpPointSize; } }
        public Vector2 SideJumpPointOffset     { get { return _sideJumpPointOffset; } }
        public Color SideJumoPointsColor       { get { return _sideJumoPointsColor; } }
        public bool ShowSideJumpPointsOnGizmos { get { return _showSideJumpPointsOnGizmos; } }
        public LayerMask WalJumplLayers        { get { return _walJumplLayers; } }
        public float WallJumpForce             { get { return _wallJumpForce; } }
        public float WallJumpTime              { get { return _wallJumpTime; } }
        public bool OnWallJump                { get { return _onWallJump; } set { _onWallJump = value; } }
        public bool CanWallJump                { get { return _canWallJump; } set { _canWallJump = value; } }
        public bool LeftWallJump               { get { return _leftWallJump; } set { _leftWallJump = value;} }
        public bool RightWallJump              { get { return _rightWallJump; } set { _rightWallJump = value;} }

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
        public float JumpSpeed         { get { return _jumpSpeed; } }
        public float ReduceMultiplier  { get { return _reduceMultiplier; } }
        public float ReleaseReduceMult { get { return _releaseReduceMult; } }
        public float MaxReduceSpeed    { get { return _maxReduceSpeed; } }
        public float MinStayTime       { get { return _minStayTime; } }
        public float CoyoteTime        { get { return _coyoteTime; } }
        public float JumpInputBuffer   { get { return _jumpInputBuffer; } }
        public int JumpCount           { get { return _jumpCount; } }
        public int CurrentJumpCount    { get { return _currentJumpCount; } set { _currentJumpCount = value; } }
        
        // Fall
        public float HoldingBonusTime { get { return _holdingBonusTime; } }
        public float FallMultiplier   { get { return _fallMultiplier; } }
        public float MaxFallSpeed     { get { return _maxFallSpeed; } }

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