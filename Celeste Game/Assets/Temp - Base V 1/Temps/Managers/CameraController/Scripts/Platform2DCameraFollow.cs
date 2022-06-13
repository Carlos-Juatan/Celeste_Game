using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAssets.Characters.Player;

namespace SystemManager.CameraManagement
{
    public class Platform2DCameraFollow : MonoBehaviour
    {
#region Var
        [Header("Setup Camera")]
        [SerializeField] Vector2 _DeathZone;
        [SerializeField] float _speed = 3f;

        Vector2 _threshold;

        Rigidbody2D _followRigidbody2D;
        Transform _followObject;
        Camera _mainCamera => GetComponent<Camera>();
#endregion

#region Start
        public void GameplayStart(){
            //_mainCamera = GetComponent<Camera>();
            _threshold = CalculateThreshold();

            GameObject player = FindObjectOfType<PlayerController>().gameObject;
            _followObject = player.transform;
            _followRigidbody2D = player.GetComponent<Rigidbody2D>();
        }
#endregion

#region Calculate Physics
        public void GameplayFixedUpdate(){
            Vector2 follow = _followObject.position;
            float xDiference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDiference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if(Mathf.Abs(xDiference) >= _threshold.x){
                newPosition.x = follow.x;
            }
            if(Mathf.Abs(yDiference) >= _threshold.y){
                newPosition.y = follow.y;
            }

            float moveSpeed = _followRigidbody2D.velocity.magnitude > _speed ? _followRigidbody2D.velocity.magnitude : _speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        Vector3 CalculateThreshold(){
            Rect aspect = _mainCamera.pixelRect;
            Vector2 t = new Vector2(_mainCamera.orthographicSize * aspect.width / aspect.height, _mainCamera.orthographicSize);
            t.x -= _DeathZone.x;
            t.y -= _DeathZone.y;
            return t;
        }

        void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Vector2 border = CalculateThreshold();
            Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
        }
#endregion
    }
}