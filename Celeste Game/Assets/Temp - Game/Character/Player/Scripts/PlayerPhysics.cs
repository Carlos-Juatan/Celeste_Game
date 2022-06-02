using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerPhysics : MonoBehaviour
    {
#region Var
        [Header("PlayerController refence")]
        [SerializeField] PlayerController _player;

        // Ground Check points
        Vector2 _rightPointOffset {
            get{
                Vector2 value = _player.Data.GroundPointsOffset;
                value.x += transform.position.x;
                value.y += transform.position.y;
                return value;
            }
        }

        Vector2 _leftPointOffset{
            get{
                Vector2 value = _rightPointOffset;
                value.x = transform.position.x - _player.Data.GroundPointsOffset.x;
                return value;
            }
        }

        // Roof Edge Check points
        Vector2 _rightRoofPos {
            get{
                Vector2 value = _player.Data.RoofPointsOffset;
                value.x += transform.position.x;
                value.y += transform.position.y;
                return value;
            }
        }

        Vector2 _leftRoofPos{
            get{
                Vector2 value = _rightRoofPos;
                value.x = transform.position.x - _player.Data.RoofPointsOffset.x;
                return value;
            }
        }
#endregion

#region Updating
        // Called every frame by PlayerController On Update after all updates before that
        public void UpdatePhysicsAfterAll(){

            // Update the WasGrounded in the same frame that IsGrounded, but with a little delay after the states.
            _player.Data.WasGrounded = _player.Data.IsGrounded;
        }
#endregion

#region Physics Calculatings
        // Called by the PlayerController FixedUpdate
        public void PhysicsCalculatings(){
            GroundCheck();
        }

    #region Ground Check
        void GroundCheck(){

            // Pointo to check the ground
            Collider2D[] leftColliders = Physics2D.OverlapCircleAll(
                _leftPointOffset, _player.Data.GroundPointsRadius, _player.Data.GroundLayers
            );
            Collider2D[] rightColliders = Physics2D.OverlapCircleAll(
                _rightPointOffset, _player.Data.GroundPointsRadius, _player.Data.GroundLayers
            );
            
            _player.Data.LeftEdgeGrounded = leftColliders.Length > 0;
            _player.Data.RightEdgeGrounded = rightColliders.Length > 0;

            _player.Data.IsGrounded = (_player.Data.LeftEdgeGrounded || _player.Data.RightEdgeGrounded);
        }
    #endregion

    #region Edge Roof Correction
        public bool RoofEdgeDetection(){

            // Point to check the roof edge
            Collider2D rightEdgeRoof = Physics2D.OverlapBox(_rightRoofPos, _player.Data.RoofPointsSize, 0, _player.Data.RoofLayers);
            Collider2D leftEdgeRoof = Physics2D.OverlapBox(_leftRoofPos, _player.Data.RoofPointsSize, 0, _player.Data.RoofLayers);

            // If the left and the right have collisions with something cancel the jump
            if(leftEdgeRoof != null && rightEdgeRoof != null){
                return false;
            }
            else{
            // Else if have just 1 o any collisions continue the jump and make a correction case need it

                if(rightEdgeRoof != null && leftEdgeRoof == null){
                    // if need correction because the right roof corner
                    RoofEdgeCorrection(_rightRoofPos, -1);

                }else if(leftEdgeRoof != null && rightEdgeRoof == null){
                    // if need correction because the left roof corner
                    RoofEdgeCorrection(_leftRoofPos, 1);
                }

                return true;
            }
        }

        void RoofEdgeCorrection(Vector2 colliderPos, int direction){

            // distance to translate the player
            float translateAmount = 0;
            bool colliding = true;

            do{
                // Adding Distance
                translateAmount += _player.Data.DistanceForFrame * direction;

                // Recalculate the collider position
                colliderPos.x += translateAmount;
                
                // Detect if stay colliding with the roof
                colliding = Physics2D.OverlapBox(colliderPos, _player.Data.RoofPointsSize, 0, _player.Data.RoofLayers);

            }while(colliding);

            transform.Translate(translateAmount, 0, 0);
        }
    #endregion
#endregion

#region On Draw Gizmos
#if UNITY_EDITOR
        void OnDrawGizmos() {

            Vector2 playerPos  = new Vector2(transform.position.x, transform.position.y);
            
            // Draw the ground check points
            if(_player.Data.ShowGroundPointsOnGizmos){

                // Ground Check color
                Gizmos.color = _player.Data.GroundPointsColor;

                // Draw Spheres on the ground check points
                Gizmos.DrawSphere(_leftPointOffset, _player.Data.GroundPointsRadius);
                Gizmos.DrawSphere(_rightPointOffset, _player.Data.GroundPointsRadius);
            }

            // Draw the roof edge detection points.
            if(_player.Data.ShowRoofPointsOnGizmos){

                // Roof edge check color
                Gizmos.color = _player.Data.RoofPointsColor;

                // Draw Cubes on the roof check points
                Gizmos.DrawCube(_rightRoofPos, _player.Data.RoofPointsSize);
                Gizmos.DrawCube(_leftRoofPos, _player.Data.RoofPointsSize);
            }
        }
#endif
#endregion
    }
}