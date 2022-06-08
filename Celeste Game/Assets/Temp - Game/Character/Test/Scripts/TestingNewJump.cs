using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player.Testing
{
    public class TestingNewJump : MonoBehaviour
    {
#region References
        Rigidbody2D rb2D => GetComponent<Rigidbody2D>();
        [SerializeField] Animator _anim;
#endregion

#region Var
        [Header("Jump Up")]
        [SerializeField] float _jumpSpeed = 28f;

        [Header("Jump Reduce")]
        [SerializeField] float _reduceMultiplier = 2f;
        [SerializeField] float _releaseReduceMult = 8f;
        [SerializeField] float _maxReduceSpeed = 28f;

        [Header("Fall")]
        [SerializeField] float _fallMultiplier = 2f;
        [SerializeField] float _maxFallSpeed = 30f;

        Vector2 _currentVelocity;
        float _currentReduce;
        bool _jumpOn = false;
#endregion

#region Start
        //void Start() => Time.timeScale = 0.1f;
#endregion

#region Update.
        // Update is called once per frame
        void Update(){
                if(Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Alpha0)){
                    _jumpOn = true;
                    ExecuteJump();
                    _anim.SetBool("Jump", true);
                }

                // Canceling the up jump
                if(_currentVelocity.y <= 0f){
                    _jumpOn = false;
                }

                if(Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Alpha0)){
                    CancelJump();
                    _anim.SetBool("Jump", false);
                }
        }
#endregion

#region Fixed Update.
        void FixedUpdate(){
            if(_jumpOn){
                JumpOn();
            }else{
                Fall();
            }
        }
#endregion

#region jump On
        // Execute the Jump
        void ExecuteJump(){
            _currentVelocity.y = _jumpSpeed;
            _currentReduce = _reduceMultiplier;
        }

        // Update The Jump
        void JumpOn(){
            
            // Calculate Horizontal Velocity
            _currentVelocity.x = rb2D.velocity.x;

            // Calculate Vertical Velocity
            HundleGravity();

            // Applying Final Velocity
            rb2D.velocity = _currentVelocity;
        }

        void HundleGravity(){
            // If vertical velocity more than max fall velocity add volocity
            _currentVelocity.y -= _currentReduce;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_maxReduceSpeed);
        }

        // cancel the Jump
        void CancelJump(){
            _currentReduce = _releaseReduceMult;
        }
#endregion

#region Jump Of
        void Fall(){
            // If vertical velocity more than max fall velocity add volocity
            _currentVelocity.y -= _fallMultiplier;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_maxFallSpeed);

            // Applying Final Velocity
            rb2D.velocity = _currentVelocity;
        }
#endregion

    }
}