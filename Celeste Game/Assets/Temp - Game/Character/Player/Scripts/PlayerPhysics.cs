using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerPhysics : MonoBehaviour
    {
#region Var
        [Header("PlayerController refence")]
        [SerializeField] PlayerController _player;
#endregion

#region Updating
        // Called every frame by PlayerController On Update after all updates before that
        public void UpdatePhysicsAfterAll(){

            // Update the WasGrounded in the same frame that IsGrounded, but with a little delay after the states.
            _player.Data.WasGrounded = _player.Data.IsGrounded;

            if(_player.Data.WasGrounded != _player.Data.IsGrounded)
                Debug.Log("Bateu Bateu Bateu");
        }
#endregion

#region Physics Calculatings
        // Called by the PlayerController FixedUpdate
        public void PhysicsCalculatings(){
            GroundCheck();
        }

        void GroundCheck(){
            Vector2 playerPos  = new Vector2(transform.position.x, transform.position.y);

            Collider2D[] leftColliders = Physics2D.OverlapCircleAll(
                playerPos + _player.Data.LeftPointOffset, _player.Data.PointsRadius, _player.Data.GroundLayers
            );
            Collider2D[] rightColliders = Physics2D.OverlapCircleAll(
                playerPos + _player.Data.RightPointOffset, _player.Data.PointsRadius, _player.Data.GroundLayers
            );
            
            _player.Data.LeftEdgeGrounded = leftColliders.Length > 0;
            _player.Data.RightEdgeGrounded = rightColliders.Length > 0;

            _player.Data.IsGrounded = (_player.Data.LeftEdgeGrounded || _player.Data.RightEdgeGrounded);
        }
#endregion

#region On Draw Gizmos
        void OnDrawGizmos() {
            if(_player.Data.ShowPointsOnGizmos){
                Vector2 playerPos  = new Vector2(transform.position.x, transform.position.y);

                //UnityEditor.Handles.color = _player.Data.LeftPointColor;
                //Gizmos.DrawWireSphere(playerPos + _player.Data.LeftPointOffset, _player.Data.PointsRadius);

                // Draw a circle on the fist ground check point
                Gizmos.color = _player.Data.LeftPointColor;
                Gizmos.DrawSphere(playerPos + _player.Data.LeftPointOffset, _player.Data.PointsRadius);

                // Second ground check point
                Gizmos.color = _player.Data.RightPointColor;
                Gizmos.DrawSphere(playerPos + _player.Data.RightPointOffset, _player.Data.PointsRadius);
            }
        }
#endregion
    }
}