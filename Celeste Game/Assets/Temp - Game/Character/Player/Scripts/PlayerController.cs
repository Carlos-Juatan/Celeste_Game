using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
#region Var
        [Header("Player Data"), SerializeField]
        PlayerData _data;
#endregion

#region Getters and Setters
        public PlayerData Data { get { return _data; } }
#endregion

#region Initializing
        void Awake(){
            _data.Initialize(this);
            _data.CurrentState = _data.Factory.SelectState(PlayerStates.Grounded);
            _data.CurrentState.EnterStates();
        }

        void Start() {
            
        }
#endregion

#region Updating.
        // Called every frame by AxisInput of the InputManager.
        public void UpdateAxisInput(Vector2Int input){
            _data.CurrentState.UpdateStates();

            _data.AxisInput = input;

            _data.IsMoving = _data.AxisInput.x != 0;
            
            if(_data.IsMoving)
                Flip();
        }

        void Flip(){
            if(_data.FacingDirection != _data.AxisInput.x){
                _data.FacingDirection = _data.AxisInput.x;
                _data.PlayerRenderer.flipX = !_data.PlayerRenderer.flipX;
            }
        }

        // Called by InputManager envery time the Jump input has pressed
        public void JumpingInput(){
            Debug.Log("Has Jump");
        }
#endregion

#region Physics Calculating.
        void FixedUpdate() {
            GroundCheck();
        }

        void GroundCheck(){
            Vector2 playerPos  = new Vector2(transform.position.x, transform.position.y);

            Collider2D[] leftColliders = Physics2D.OverlapCircleAll(
                playerPos + _data.LeftPointOffset, _data.PointsRadius, _data.GroundLayers
            );
            Collider2D[] rightColliders = Physics2D.OverlapCircleAll(
                playerPos + _data.RightPointOffset, _data.PointsRadius, _data.GroundLayers
            );
            
            bool leftPoint = leftColliders.Length > 0;
            bool rightPoint = rightColliders.Length > 0;

            // send to animator the points ground check boolean

            _data.IsGrounded = (leftPoint || rightPoint);
        }
#endregion

#region On Draw Gizmos
        void OnDrawGizmos() {
            if(_data.ShowPointsOnGizmos){
                Vector2 playerPos  = new Vector2(transform.position.x, transform.position.y);

                //UnityEditor.Handles.color = _data.LeftPointColor;
                //Gizmos.DrawWireSphere(playerPos + _data.LeftPointOffset, _data.PointsRadius);

                // Draw a circle on the fist ground check point
                Gizmos.color = _data.LeftPointColor;
                Gizmos.DrawSphere(playerPos + _data.LeftPointOffset, _data.PointsRadius);

                // Second ground check point
                Gizmos.color = _data.RightPointColor;
                Gizmos.DrawSphere(playerPos + _data.RightPointOffset, _data.PointsRadius);
            }
        }
#endregion
    }
}