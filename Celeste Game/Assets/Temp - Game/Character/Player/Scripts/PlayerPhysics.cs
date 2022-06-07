using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerPhysics : MonoBehaviour
    {
#region Var
        [Header("PlayerController refence")]
        [SerializeField] PlayerController _player;

    #region Ground Check points
        Vector2 _rightPointOffset { get{
            Vector2 value = _player.Data.GroundPointsOffset;
            value.x += transform.position.x;
            value.y += transform.position.y;
            return value;
        }}

        Vector2 _leftPointOffset{ get{
            Vector2 value = _rightPointOffset;
            value.x = transform.position.x - _player.Data.GroundPointsOffset.x;
            return value;
        }}
    #endregion

    #region Roof Edge Check points
        Vector2 _rightRoofPos { get{
            Vector2 value = _player.Data.RoofPointsOffset;
            value.x += transform.position.x;
            value.y += transform.position.y;
            return value;
        }}

        Vector2 _leftRoofPos{ get{
            Vector2 value = _rightRoofPos;
            value.x = transform.position.x - _player.Data.RoofPointsOffset.x;
            return value;
        }}
    #endregion

    #region Wall Slider Check
        // Up right
        Vector2 _rightUpSliderPos { get{
            Vector2 value = _player.Data.SideSliderPointOffset;
            value.x += transform.position.x;
            value.y += transform.position.y - _player.Data.PlayerYCenterOffset;
            return value;
        }}

        // Up left
        Vector2 _leftUpSliderPos{ get{
            Vector2 value = _rightUpSliderPos;
            value.x = transform.position.x - _player.Data.SideSliderPointOffset.x;
            return value;
        }}

        // Down Right
        Vector2 _rightDownSliderPos{ get{
            Vector2 value = _rightUpSliderPos;
            value.y = transform.position.y - _player.Data.PlayerYCenterOffset - _player.Data.SideSliderPointOffset.y;
            return value;
        }}

        // Down left
        Vector2 _leftDownSliderPos{ get{
            Vector2 value = _leftUpSliderPos;
            value.y = transform.position.y - _player.Data.PlayerYCenterOffset - _player.Data.SideSliderPointOffset.y;
            return value;
        }}
    #endregion

    #region  Wall Jump Check
        // Right
        Vector2 _rightWallJumpPos { get{
            Vector2 value = _player.Data.SideJumpPointOffset;
            value.x += transform.position.x;
            value.y += transform.position.y;
            return value;
        }}

        // Left
        Vector2 _leftWallJumpPos{ get{
            Vector2 value = _rightWallJumpPos;
            value.x = transform.position.x - _player.Data.SideJumpPointOffset.x;
            return value;
        }}
    #endregion
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
            
            // Else if have just 1 o any collisions continue the jump and make a correction case need it
            }else{

                // if need correction because the right roof corner
                if(rightEdgeRoof != null && leftEdgeRoof == null){
                    RoofEdgeCorrection(_rightRoofPos, -1);

                // else if need correction because the left roof corner
                }else if(leftEdgeRoof != null && rightEdgeRoof == null){
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

    #region Wall Slider Check
        public bool WallSliderCheckDetection(){

            // Point to check the Wall slider
            Collider2D rightUpWallSlider = Physics2D.OverlapBox(
                _rightUpSliderPos, _player.Data.SideSliderPointSize, 0, _player.Data.WallSliderLayers);

            Collider2D leftUpWallSlider = Physics2D.OverlapBox(
                _leftUpSliderPos, _player.Data.SideSliderPointSize, 0, _player.Data.WallSliderLayers);

            Collider2D rightDownWallSlider = Physics2D.OverlapBox(
                _rightDownSliderPos, _player.Data.SideSliderPointSize, 0, _player.Data.WallSliderLayers);

            Collider2D leftDownWallSlider = Physics2D.OverlapBox(
                _leftDownSliderPos, _player.Data.SideSliderPointSize, 0, _player.Data.WallSliderLayers);

            // Set Values
            _player.Data.UpSideSlideWall = rightUpWallSlider != null || leftUpWallSlider != null;
            _player.Data.DownSideSlideWall = rightDownWallSlider != null || leftDownWallSlider != null;


            if((rightUpWallSlider != null || rightDownWallSlider != null) && _player.Data.AxisInput.x == 1){
                // if the right up or down have collider with something and the directional is right start the wall slider
                return true;

            }else if((leftUpWallSlider != null || leftDownWallSlider != null) && _player.Data.AxisInput.x == -1){
                // else if the left up or down have collider with something and the directional is left start the wall slider
                return true;
                
            }else{
                // Else cancel the wall slider
                return false;
            }
        }
    #endregion

    #region Wall Jump Check
        public bool WallJumpCheckDetection(){

            // Point to check the Wall to Jump
            Collider2D rightWallJump = Physics2D.OverlapBox(
                _rightWallJumpPos, _player.Data.SideJumpPointSize, 0, _player.Data.WalJumplLayers);

            Collider2D leftWallJump = Physics2D.OverlapBox(
                _leftWallJumpPos, _player.Data.SideJumpPointSize, 0, _player.Data.WalJumplLayers);

            _player.Data.RightWallJump = rightWallJump != null ? true : false;
            _player.Data.LeftWallJump = leftWallJump != null ? true : false;

            // If the left and the right haven't collisions with anything disable the wall jump
            if(!_player.Data.RightWallJump && !_player.Data.LeftWallJump){
                //_player.Data.CanWallJump = false;
                return false;

            // Else if have just 1 o any collisions enable the wall jump
            }else{
                //_player.Data.CanWallJump = true;
                return true;
            }
        }
    #endregion

#endregion

#region On Draw Gizmos
#if UNITY_EDITOR
        void OnDrawGizmos() {
            
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

            // Draw the Wall Jump detection points.
            if(_player.Data.ShowSideJumpPointsOnGizmos){

                // Wall Jump check color
                Gizmos.color = _player.Data.SideJumoPointsColor;

                // Draw Cubes on the roof check points
                Gizmos.DrawCube(_rightWallJumpPos, _player.Data.SideJumpPointSize);
                Gizmos.DrawCube(_leftWallJumpPos, _player.Data.SideJumpPointSize);
            }

            // Draw the Wall Slider detection points.
            if(_player.Data.ShowSideSliderPointsOnGizmos){

                // Wall Jump check color
                Gizmos.color = _player.Data.SideSliderPointsColor;

                // Draw Cubes on the roof check points
                Gizmos.DrawCube(_rightUpSliderPos, _player.Data.SideSliderPointSize);
                Gizmos.DrawCube(_leftUpSliderPos, _player.Data.SideSliderPointSize);
                Gizmos.DrawCube(_rightDownSliderPos, _player.Data.SideSliderPointSize);
                Gizmos.DrawCube(_leftDownSliderPos, _player.Data.SideSliderPointSize);
            }
        }
#endif
#endregion
    }
}