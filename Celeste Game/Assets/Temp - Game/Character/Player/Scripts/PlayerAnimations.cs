using System.Collections;
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

        [Header("Animation Clips")]
        [SerializeField] AnimationClip _clip_Flip;

        // Animations
        Animator _animator => GetComponent<Animator>();

        bool _flipIsRuning = false;
        float _animationStartFrame = 0.0f;
        int _currentClipHash;

        readonly int _hash_Idle = Animator.StringToHash("Idle");
        readonly int _hash_IdleA = Animator.StringToHash("IdleA");
        readonly int _hash_IdleB = Animator.StringToHash("IdleB");
        readonly int _hash_IdleC = Animator.StringToHash("IdleC");
        readonly int _hash_Move = Animator.StringToHash("Move");
        readonly int _hash_Flip = Animator.StringToHash("Flip");
        readonly int _hash_Jump = Animator.StringToHash("Jump");
        readonly int _hash_FallJump = Animator.StringToHash("FallJump");
        readonly int _hash_FallLand_Start = Animator.StringToHash("FallLand_Start");
        readonly int _hash_FallLand_Loop = Animator.StringToHash("FallLand_Loop");
#endregion

#region Initializing
        // Defaut MonoBehaviour Function.
        void Awake(){
            //_animator.SetBool(Hash_IsGrounded, true);
        }
#endregion

#region Updating
        // Called every frame by PlayerController On Update after all logics.
        public void UpdateAnimations(){
            // Flip animation.
            Flip();
        }

        void Flip(){
            if(_player.Data.AxisInput.x != 0){
                if(_player.Data.FacingDirection != _player.Data.AxisInput.x){
                    _player.Data.FacingDirection = _player.Data.AxisInput.x;
                    _player.Data.PlayerRenderer.flipX = !_player.Data.PlayerRenderer.flipX;

                    if(_player.Data.IsGrounded){
                        _animator.Play(_hash_Flip);
                        //_animationStartFrame = 0.0f;
                        StartCoroutine(WaitAnimationsFinishing());
        }}}}
#endregion

#region External Events Inputs

    #region Is Grounded
        // Called when the Grounded state starts
        public void StartGrounded(){
            if(_player.Data.IsMoving){
                StartMove();
            }else{
                EndMove();
            }
        }

        // Called when the Grounded state ends
        public void EndGrounded(){
            
        }
    #endregion
    
    #region Is Jumping
        // Called when the jump state starts
        public void StartJump(){
            _animator.Play(_hash_Jump);
                _currentClipHash = _hash_Jump;
            _inpactDust_PS.Play();
        }

        // Called when the jump state ends
        public void EndJump(){
        }
    #endregion
    
    #region Is Falling
        // Called when the Fall state starts
        public void StartFall(){
            if(!_player.Data.WasGrounded){
                _animator.Play(_hash_FallJump);
                _currentClipHash = _hash_FallJump;
            }else{
                _animator.Play(_hash_FallLand_Start);
                _currentClipHash = _hash_FallLand_Loop;
                StartCoroutine(WaitAnimationsFinishing());
            }
        }

        // Called when the Fall state ends
        public void EndFall(){
            _inpactDust_PS.Play();
        }
    #endregion
    
    #region Is Moving
        // Called when the Move state starts
        public void StartMove(){
            if(_player.Data.IsGrounded){
                if(_player.Data.FacingDirection == _player.Data.AxisInput.x){
                    _animator.Play(_hash_Move);
                }
                _currentClipHash = _hash_Move;
            }
        }

        // Called when the Move state ends
        public void EndMove(){
            if(_player.Data.IsGrounded){
                _animator.Play(_hash_Idle);
                _currentClipHash = _hash_Idle;
            }
        }
    #endregion
#endregion

#region Compare Animation
        bool CompareAnimation(AnimationClip watingClip){
            // Verify if animation current is the waitingClip
            int allClipsLength = _animator.GetCurrentAnimatorClipInfo(0).Length;
            bool animationsMatch = false;

            for (int i = 0; i < allClipsLength; i += 1){
                if(_animator.GetCurrentAnimatorClipInfo(0)[i].clip == watingClip){
                    animationsMatch = true;
            }}

            return animationsMatch;
        }
#endregion

#region Coroutines

    #region Wait Animations Finishing
        IEnumerator WaitAnimationsFinishing(){
            // Waiting animation finished
            do{
                yield return null;
            }while(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f);

            // When animation has finished switch clip to current animation clip
            _animator.Play(_currentClipHash);

            // (<The clip, on the case a int hash> , <the layer of the animator> , <And the Position how starts the animation>)
            //_animator.Play(_currentClipHash, 0, _animationStartFrame);
            
            // here the transition takes 0.25 % of the clip
            //_animator.CrossFade("PlayerRunning", 0.25f, 0, 0, _animationStartFrame);

            // here the transition takes 0.25 seconds
            //animator.CrossFadeInFixedTime("PlayerRunningShoot", 0.25f, -1, 0, newNormalizedTime);
        }
    #endregion

#endregion

    }
}