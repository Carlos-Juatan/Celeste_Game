using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player.Testing
{
    public class TestingJumpHigh : MonoBehaviour
    {
#region References
        [SerializeField] CronometerTime _cronometerTime;
        [SerializeField] Animator _anim;
#endregion



#region Var
        public float _jumpForce = 10f;
        public float _jumpTime = 0.3f;
        public float _jumpVelocityFalloff = 2f;
        public float _fallMultiplier = 0.08f;


        float _currentTimer;
        float _vertVelocity;
        bool _justOnceTime = false;
        bool _canGrounded = false;
        Rigidbody2D rb2D => GetComponent<Rigidbody2D>();

#endregion

#region Start
        //void Start() => Time.timeScale = 0.1f;
#endregion

#region Update.
        // Update is called once per frame
        void Update(){
                _currentTimer -= Time.deltaTime;

                if(Input.GetButtonDown("Jump")){
                    ExecuteJump();
                    _anim.SetBool("Jump", true);
                }

                if(_justOnceTime && (Input.GetButtonUp("Jump") || _currentTimer <= 0f)){
                    CancelJump();
                    _anim.SetBool("Jump", false);
                }
        }
#endregion

#region OnTriggerEnter
        void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.CompareTag("Player")){
                _cronometerTime.SetTarget(true);
            }
        }
#endregion

        // Execute the Jump
        void ExecuteJump(){

            _justOnceTime = true;
            _canGrounded = true;

            Vector2 force = Vector2.zero;
            force.x = 0f;
            force.y = _jumpForce;

            rb2D.velocity = Vector2.zero;

            rb2D.AddForce(force, ForceMode2D.Impulse);

            _currentTimer = _jumpTime;
        }

        // Update The Jump
        void FixedUpdate(){

            // Fall faster and allow small jumps. _jumpVelocityFalloff is the point at which we start adding extra gravity. Using 0 causes floating
            if (rb2D.velocity.y < _jumpVelocityFalloff){
                _vertVelocity = rb2D.velocity.y + (_fallMultiplier * Physics.gravity.y);
                rb2D.velocity = new Vector2(rb2D.velocity.x, _vertVelocity);
            }

            if(rb2D.velocity.y < 0f && _canGrounded){
                _cronometerTime.SetHightTime();
            }

            if(rb2D.velocity.y < 0f && _canGrounded && transform.localPosition.y <= -0.985013f){
                // Stop the counter of the mechanics
                _cronometerTime.StopCounter();
                _canGrounded = false;
            }
        }

        // cancel the Jump
        void CancelJump(){

            _justOnceTime = false;
            
            rb2D.velocity = Vector2.zero;

        }
    }
}