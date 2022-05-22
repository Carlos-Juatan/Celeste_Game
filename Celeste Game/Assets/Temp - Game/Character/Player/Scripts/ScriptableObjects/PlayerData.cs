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

        // Inputs
        Vector2Int _axisInput;

        // Logics
        // Ground Check
        bool _isGrounded;

        // Moving
        bool _isMoving;
        int _facingDirection;

        // Components.
        PlayerBaseState _currentState;
        PlayerStateFactory _factory;
        SpriteRenderer _playerRenderer;
#endregion

#region Getters And Setters.
        // Ground Check Inspector
        public float PointsRadius       { get { return _pointsRadius; } }
        public Color LeftPointColor     { get { return _leftPointColor; } }
        public Vector2 LeftPointOffset  { get { return _leftPointOffset; } }
        public Color RightPointColor    { get { return _rightPointColor; } }
        public Vector2 RightPointOffset { get { return _rightPointOffset; } }
        public bool ShowPointsOnGizmos  { get { return _showPointsOnGizmos; } }
        public LayerMask GroundLayers   { get { return _groundLayers; } }

        // Inputs
        public Vector2Int AxisInput  { get { return _axisInput; } set { _axisInput = value; } }

        // Logics
        // Ground Check
        public bool IsGrounded  { get { return _isGrounded; } set { _isGrounded = value; } }

        // Moving
        public bool IsMoving        { get { return _isMoving; } set { _isMoving = value; } }
        public int FacingDirection  { get { return _facingDirection; } set { _facingDirection = value; } }

        // Components.
        public PlayerBaseState CurrentState  { get { return _currentState; } set { _currentState = value; } }
        public PlayerStateFactory Factory    { get { return _factory; } }
        public SpriteRenderer PlayerRenderer { get { return _playerRenderer; } set { _playerRenderer = value; } }
        
#endregion

#region Initialize and Find Components.
        public void Initialize(PlayerController currentPlayer){
            _factory = new PlayerStateFactory(currentPlayer);

            FindComponents(currentPlayer);
        }

        // Responsable to find components on start scene
        void FindComponents(PlayerController currentPlayer){
            _playerRenderer = currentPlayer.GetComponentInChildren<SpriteRenderer>();
        }
#endregion
    }
#endregion
}