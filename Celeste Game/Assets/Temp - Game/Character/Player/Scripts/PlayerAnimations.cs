using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
#region Var
        [Header("PlayerController refence")]
        [SerializeField] PlayerController _player;

        [Header("Particles")]
        [SerializeField] ParticleSystem _inpactDust_PS;

        // Animations
        Animator _animator => GetComponent<Animator>();
        readonly int Hash_IsMoving = Animator.StringToHash("Is Moving");
        readonly int Hash_IsGrounded = Animator.StringToHash("Is Grounded");
        readonly int Hash_IsJumping = Animator.StringToHash("Is Jumping");
        readonly int Hash_IsFalling = Animator.StringToHash("Is Falling");
        readonly int Hash_Flip = Animator.StringToHash("Flip");
#endregion

#region Initializing
        // Defaut MonoBehaviour Function.
        void Awake(){
            _animator.SetBool(Hash_IsGrounded, true);
        }
#endregion

#region Updating
        // Called every frame by PlayerController On Update after all logics.
        public void UpdateAnimations(){
            // Flip animation.
            if(_player.Data.AxisInput.x != 0)
                Flip();
        }

        void Flip(){
            if(_player.Data.FacingDirection != _player.Data.AxisInput.x){
                _player.Data.FacingDirection = _player.Data.AxisInput.x;
                _animator.SetBool(Hash_Flip, true);
                _player.Data.PlayerRenderer.flipX = !_player.Data.PlayerRenderer.flipX;
                Invoke("SetFlipAnimationFalse", 0.1f);
            }
        }

        void SetFlipAnimationFalse(){
                _animator.SetBool(Hash_Flip, false);
        }
#endregion

#region External Events Inputs

    #region Is Grounded
        // Called when the Grounded state starts
        public void StartGrounded(){
            _animator.SetBool(Hash_IsGrounded, true);
        }

        // Called when the Grounded state ends
        public void EndGrounded(){
            _animator.SetBool(Hash_IsGrounded, false);
        }
    #endregion
    
    #region Is Jumping
        // Called when the jump state starts
        public void StartJump(){
            _animator.SetBool(Hash_IsJumping, true);
            _inpactDust_PS.Play();
        }

        // Called when the jump state ends
        public void EndJump(){
            _animator.SetBool(Hash_IsJumping, false);
        }
    #endregion
    
    #region Is Falling
        // Called when the Fall state starts
        public void StartFall(){
            _animator.SetBool(Hash_IsFalling, true);
        }

        // Called when the Fall state ends
        public void EndFall(){
            _animator.SetBool(Hash_IsFalling, false);
            _inpactDust_PS.Play();
        }
    #endregion
    
    #region Is Moving
        // Called when the Move state starts
        public void StartMove(){
            _animator.SetBool(Hash_IsMoving, true);
        }

        // Called when the Move state ends
        public void EndMove(){
            _animator.SetBool(Hash_IsMoving, false);
        }
    #endregion
#endregion
    }
}