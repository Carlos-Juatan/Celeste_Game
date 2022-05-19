using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager.InputManagement
{
    public class Test : MonoBehaviour 
    {
        public static Test instance;
        InputManager manager;

        void Start()
        {
            if(instance == null) { instance = this; }
            manager = FindObjectOfType<InputManager>();
        }

        public void Moving(Vector2Int velocity) => Debug.Log(velocity);

        public void Jumping() => Debug.Log("Has Jump");

        public void Pause() => manager.SwitchState(manager._InputPauseState);

        public void Unpause() => manager.SwitchState(manager._InputGameplayState);
    }
}