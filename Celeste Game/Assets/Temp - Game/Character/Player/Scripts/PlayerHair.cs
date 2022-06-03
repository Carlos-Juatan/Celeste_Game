using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerHair : MonoBehaviour
    {
#region Var
        [Header("Components")]
        [SerializeField] PlayerController _player;
        [SerializeField] SpriteRenderer _playerSprite;
        [SerializeField] Transform _hairAnchor;
        [SerializeField] Transform[] _hairsParts;

        [Header("Hair Setup")]
        [SerializeField] float _lerpSpeed = 20f;
        [SerializeField] Vector2 _partsOffset;
        [SerializeField] Vector2 _idleOffset;

        // Parts Offset
        Vector2 _partsCurrentOffset = Vector2.zero;
        Vector2 _targetPosition = Vector2.zero;
        Vector2 _newPos = Vector2.zero;

        // Flip hair
        bool _flipHair = false;
        int _facingMult = -1;

#endregion

#region Initializing
        void Awake(){
            _partsCurrentOffset = _partsOffset;
        }
#endregion

#region Updating
        void Update(){
            FlipHair();

            CalculateHairOffset();

            ApplyingHairMovement();
        }

    #region Flip Hair
        void FlipHair(){
            
            if(_playerSprite.flipX != _flipHair){
                _flipHair = _playerSprite.flipX;
                _facingMult *= -1;
                
                transform.eulerAngles = new Vector3(0f, !_flipHair ? 0f : 180f, 0f);
            }
        }
    #endregion

    #region Calculate Hair Offset
        void CalculateHairOffset(){

            // Horizontal Offset
            if(_player.Data.AxisInput.x != 0){
                _partsCurrentOffset.x = _partsOffset.x * Mathf.Clamp01(Mathf.Abs(_player.Data.Rigidbody2D.velocity.x)) * _facingMult;

            }else{
                _partsCurrentOffset.x = Mathf.MoveTowards(_partsCurrentOffset.x, _idleOffset.x, Time.deltaTime);
            }

            // Vertical Offset
            if(!_player.Data.IsGrounded){
                _partsCurrentOffset.y = _partsOffset.y * Mathf.Clamp01(Mathf.Abs(_player.Data.Rigidbody2D.velocity.y));

                if(_player.Data.Rigidbody2D.velocity.y < 0){
                    _partsCurrentOffset.y *= -1f;
                }
                
            }else{
                _partsCurrentOffset.y = _idleOffset.y;
            }
        }
    #endregion

    #region Applying Hair Movement
        void ApplyingHairMovement(){

            Transform peaceToFollow = _hairAnchor;

            foreach (Transform hairPart in _hairsParts)
            {
                if(!hairPart.Equals(_hairAnchor))
                    _targetPosition = (Vector2)(peaceToFollow.position) + _partsCurrentOffset;
                    _newPos = Vector2.Lerp(hairPart.position, _targetPosition, Time.deltaTime * _lerpSpeed);

                    hairPart.position = _newPos;
                    peaceToFollow = hairPart;
            }
        }
    #endregion
#endregion
        
    }
}