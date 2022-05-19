using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager.CameraManagement
{
    public class FixeCameraResize : MonoBehaviour
    {
        Camera _camInstance;

        [Header("Set Camera On Play Level")]
        public Vector3 cameraPosition;
        public float orthographicSize;

        void Start()
        {
            _camInstance = Camera.main;
            _camInstance.transform.position = cameraPosition;
            _camInstance.orthographicSize = orthographicSize;
        }
    }
}