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
        [SerializeField] ParticleSystem _wallSliderDust_PS;
        [SerializeField] ParticleSystem _upWallSliderDust_PS;

        //[Header("Animation Clips")]
        //[SerializeField] AnimationClip _clip_Flip;

        [Header("Squeeze Animatins")]
        [SerializeField] float _timeToJumpSqueeze = 0.2f;
        [SerializeField] float _timeToFallSqueeze = 0.1f;

        [Header("Idle Animatins")]
        [SerializeField] float _timeToNextIdle = 6f;
        [SerializeField] int _IdleAChance = 5;
        [SerializeField] int _IdleBChance = 3;
        [SerializeField] int _IdleCChance = 1;

        // Components
        Animator _animator => GetComponent<Animator>();
        Transform _playerSpriteTransform => GetComponentInChildren<SpriteRenderer>().transform;

        // Waiting Animations
        bool _waitingAnimationEnd = false;
        float _animationStartFrame = 0.0f;
        int _currentClipHash;

        // Idle Animations
        bool _isIdle = true;
        float _currentIdleTimer;

        // Walk Animations
        bool _isWalkToWall = false;
        bool _canChangeWalk = false;

        // Wall Slider Animatins
        bool _onWallSlider = false;
        bool _lastWallUpCheck = false;
        bool _canChageWallEffects = false;

        readonly int _hash_Idle = Animator.StringToHash("Idle");
        readonly int _hash_IdleA = Animator.StringToHash("IdleA");
        readonly int _hash_IdleB = Animator.StringToHash("IdleB");
        readonly int _hash_IdleC = Animator.StringToHash("IdleC");
        readonly int _hash_LookUp = Animator.StringToHash("LookUp");
        readonly int _hash_Duck = Animator.StringToHash("Duck");
        readonly int _hash_EdgeFront = Animator.StringToHash("EdgeFront");
        readonly int _hash_EdgeBack = Animator.StringToHash("EdgeBack");
        readonly int _hash_Move = Animator.StringToHash("Move");
        readonly int _hash_Flip = Animator.StringToHash("Flip");
        readonly int _hash_Jump = Animator.StringToHash("Jump");
        readonly int _hash_FallJump = Animator.StringToHash("FallJump");
        readonly int _hash_FallLand_Start = Animator.StringToHash("FallLand_Start");
        readonly int _hash_FallLand_Loop = Animator.StringToHash("FallLand_Loop");

        readonly int _hash_Push = Animator.StringToHash("Push");
        readonly int _hash_WallSlider = Animator.StringToHash("WallSlider");
        readonly int _hash_Dangling = Animator.StringToHash("Dangling");
        readonly int _hash_Climb_Move = Animator.StringToHash("Climb_Move");
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
            // Idle Timer
            _currentIdleTimer -= Time.deltaTime;

            // Flip animation.
            Flip();

            // Idle animations
            IdleAnimations();

            // Push or walk animation
            CheckPushAnimation();

            // Wall Slider animations
            CheckWallSliderAnimation();
        }

    #region Play flip Animation
        void Flip(){
            if(_player.Data.OnWallJump){
                    _player.Data.PlayerRenderer.flipX = _player.Data.FacingDirection == 1 ? false : true;
                    // Flip Particles
                    FlipParticles();

            }else if(_player.Data.AxisInput.x != 0){
                if(_player.Data.FacingDirection != _player.Data.AxisInput.x){
                    _player.Data.FacingDirection = _player.Data.AxisInput.x;
                    _player.Data.PlayerRenderer.flipX = !_player.Data.PlayerRenderer.flipX;

                    // Flip Particles
                    FlipParticles();

                    if(_player.Data.IsGrounded){
                        _animator.Play(_hash_Flip);
                        //_animationStartFrame = 0.0f;
                        StartCoroutine(WaitAnimationsFinishing());
        }}}}

        void FlipParticles(){
            // Wall Slider Particles
            Vector2 particlePos = _wallSliderDust_PS.transform.localPosition;
            particlePos.x = _player.Data.FacingDirection == 1 ? Mathf.Abs(particlePos.x) : (particlePos.x * -1);
            _wallSliderDust_PS.transform.localPosition = particlePos;
        }
    #endregion

    #region Random Idle, edge falling and Duck animations
        // Play all animation how chan run on idle state, like crouch, look up, edge, random idles animations
        void IdleAnimations(){
            if(_isIdle){

                if(_player.Data.AxisInput.y > 0){
                    _currentClipHash = _hash_LookUp;

                }else if(_player.Data.AxisInput.y < 0){
                    _currentClipHash = _hash_Duck;

                }else{
                    // Verify if player is grounded on edge of the platform
                    if(_player.Data.LeftEdgeGrounded && !_player.Data.RightEdgeGrounded){
                        _currentClipHash = EdgeDirection(1);
                        
                    }else if(!_player.Data.LeftEdgeGrounded && _player.Data.RightEdgeGrounded){
                        _currentClipHash = EdgeDirection(-1);

                    }else{
                        //_currentClipHash = _hash_Idle;
                        _currentClipHash = ChooseIdle();
                }}

                if(!_waitingAnimationEnd){
                    _animator.Play(_currentClipHash);
        }}}

        // Check how direction to player the edge animation
        int EdgeDirection(int dir){
            if(_player.Data.FacingDirection == dir){
                return _hash_EdgeFront;

            }else{
                return _hash_EdgeBack;
        }}

        // Chooser randomly a idle animation if stay on animation state for more than "_timeToNextIdle" and return it
        int ChooseIdle(){
            if(_currentIdleTimer > 0){
                return _hash_Idle;

            }else{
                int randomRange = _IdleAChance + _IdleBChance + _IdleCChance + 1;
                int randomIdle = Random.Range(1, randomRange);
                _currentIdleTimer = _timeToNextIdle;

                if(randomIdle <= _IdleAChance){
                    _animator.Play(_hash_IdleA);
                    StartCoroutine(WaitAnimationsFinishing());

                }else if(randomIdle <= _IdleAChance + _IdleBChance){
                    _animator.Play(_hash_IdleB);
                    StartCoroutine(WaitAnimationsFinishing());

                }else{
                    _animator.Play(_hash_IdleC);
                    StartCoroutine(WaitAnimationsFinishing());
            }}

            return _hash_Idle;
        }
    #endregion

    #region Walk and push animations
        void CheckPushAnimation(){

            if(_player.Data.IsGrounded && _player.Data.WasGrounded && _player.Data.AxisInput.x != 0f && !_waitingAnimationEnd){
                Debug.Log("Vai Se Fooooooooooooooooooooooooo");

                if(_isWalkToWall != _player.Data.WallSliderInteratc){
                    _isWalkToWall = !_isWalkToWall;
                    _canChangeWalk = true;
                }

                if(_canChangeWalk){
                    _canChangeWalk = false;

                    if(!_player.Data.WallSliderInteratc){
                        _animator.Play(_hash_Move);

                    }else{
                        _animator.Play(_hash_Push);
                    }
                }
            }
        }
    #endregion

    #region Wall slider animations
        void CheckWallSliderAnimation(){
            if(_onWallSlider){

                if(_lastWallUpCheck != (_player.Data.UpSideSlideWall && !_player.Data.DownSideSlideWall)){
                    _lastWallUpCheck = (_player.Data.UpSideSlideWall && !_player.Data.DownSideSlideWall);
                    _canChageWallEffects = true;
                }
                
                if(_canChageWallEffects){
                    _canChageWallEffects = false;

                    if(_player.Data.UpSideSlideWall && !_player.Data.DownSideSlideWall){
                        _animator.Play(_hash_Dangling);
                        _wallSliderDust_PS.Stop();
                        _upWallSliderDust_PS.Play();

                    }else{
                        _animator.Play(_hash_WallSlider);
                        _upWallSliderDust_PS.Stop();
                        _wallSliderDust_PS.Play();
                    }
                }
            }else{
                _wallSliderDust_PS.Stop();
                _upWallSliderDust_PS.Stop();

            }
        }
    #endregion

#endregion

#region External Events Inputs

    #region Is Grounded
        // Called when the Grounded state starts
        public void StartGrounded(){
            if(_player.Data.IsMoving){
                StartMove();
            }else{
                StartIdle();
            }
        }

        // Called when the Grounded state ends
        public void EndGrounded(){
            _isIdle = false;
        }
    #endregion
    
    #region Is Jumping
        // Called when the jump state starts
        public void StartJump(){
            _animator.Play(_hash_Jump);
            _currentClipHash = _hash_Jump;
            _inpactDust_PS.Play();
            StopCoroutine("SpriteFlipFlopSqueeze");
            StartCoroutine(SpriteFlipFlopSqueeze(.6f, 1.4f, _timeToJumpSqueeze));
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
            StopCoroutine("SpriteFlipFlopSqueeze");
            StartCoroutine(SpriteFlipFlopSqueeze(1.5f, .5f, _timeToFallSqueeze, true));
        }
    #endregion
    
    #region Is Moving
        // Called when the Move state starts
        public void StartMove(){
            if(_player.Data.IsGrounded){
                if(_player.Data.FacingDirection == _player.Data.AxisInput.x){
                    if(!_player.Data.WallSliderInteratc){
                            _animator.Play(_hash_Move);

                        }else{
                            _animator.Play(_hash_Push);
                        }
                }
                _currentClipHash = _hash_Move;
            }
        }

        // Called when the Move state ends
        public void EndMove(){}
    #endregion

    #region Is Idle
        // Called when the Idle state starts
        public void StartIdle(){
            if(_player.Data.IsGrounded){
                _animator.Play(_hash_Idle);
                _currentClipHash = _hash_Idle;
                _isIdle = true;
                _currentIdleTimer = _timeToNextIdle;
            }
        }

        // Called when the Idle state ends
        public void EndIdle(){
            _isIdle = false;
        }
    #endregion

    #region Wall Slider
        // Called when the Wall Slider state starts
        public void StartWallSlider(){
            _wallSliderDust_PS.Play();
            _currentClipHash = _hash_WallSlider;
            _animator.Play(_hash_WallSlider);
            StopCoroutine("SpriteFlipFlopSqueeze");
            _onWallSlider = true;
        }

        // Called when the Wall Slider state ends
        public void EndWallSlider(){
            _wallSliderDust_PS.Stop();
            _onWallSlider = false;
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
        // Called when need wait a animation finish.
        IEnumerator WaitAnimationsFinishing(){

            // Set on all animations need wait for true
            _waitingAnimationEnd = true;
            

            // Waiting animation finished
            do{
                yield return null;
            }while(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f);

            // Set on all animations need wait for false
            _waitingAnimationEnd = false;

            // When animation has finished switch clip to current animation clip
            _animator.Play(_currentClipHash);

            /*
            // (<The clip, on the case a int hash> , <the layer of the animator> , <And the Position how starts the animation>)
            _animator.Play(_currentClipHash, 0, _animationStartFrame);
            
            // here the transition takes 0.25 % of the clip
            _animator.CrossFade("PlayerRunning", 0.25f, 0, 0, _animationStartFrame);

            // here the transition takes 0.25 seconds
            animator.CrossFadeInFixedTime("PlayerRunningShoot", 0.25f, -1, 0, newNormalizedTime);
            */
        }
    #endregion

    #region Sprite Squeeze
        // Deform the sprite to make more fluid motions to the animations
        IEnumerator SpriteFlipFlopSqueeze(float sizeX, float sizeY, float timer, bool reposition = false){
            Vector3 originalSize = Vector3.one;
            Vector3 originalPos = Vector3.zero;
            originalPos.z = -2;
            Vector3 newSize = new Vector3(sizeX, sizeY, originalSize.z);
            Vector3 newPos = originalPos + (Vector3.up * -sizeY);
            float t = 0f;

            // Squeezing the sprite
            while(t <= 1f){
                t += Time.deltaTime / timer;

                // if need reposition it's fall, or is jump and cancel the jump sneeze if rigidbody's y velocity is equal or less than 0
                if(reposition){
                    _playerSpriteTransform.localPosition = Vector3.Lerp(originalPos, newPos, t);
                }else if(_player.Data.Rigidbody2D.velocity.y < 0){
                    break;
                }

                _playerSpriteTransform.localScale = Vector3.Lerp(originalSize, newSize, t);
                yield return null;
            }

            Vector3 currentSize = _playerSpriteTransform.localScale;
            Vector3 currentPos = _playerSpriteTransform.localPosition;

            // Come back to original size
            t = 0f;
            while(t <= 1f){
                t += Time.deltaTime / timer;
                _playerSpriteTransform.localScale = Vector3.Lerp(currentSize, originalSize, t);
                if(reposition)
                    _playerSpriteTransform.localPosition = Vector3.Lerp(currentPos, originalPos, t);
                yield return null;
            }
        }
    #endregion

#endregion

    }
}