using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager
{
    public class GlobalExternals : MonoBehaviour
    {
        public static GlobalExternals instace { get; private set; }

        void Awake()
        {
            if(instace == null){
                instace = this;
            }
        }

        //To support external coroutines being called:
        public void ExternalCoroutine(IEnumerator coroutineMethod)
        {
            StartCoroutine(coroutineMethod);
        }
    }
}