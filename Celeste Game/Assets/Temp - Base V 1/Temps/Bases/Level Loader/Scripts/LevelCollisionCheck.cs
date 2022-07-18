using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.OcclusionCulling.LoadLevels
{
    public class LevelCollisionCheck : MonoBehaviour
    {
        LevelPeace _levelPeace;
        LevelInfo _levelInfo;

        public void SelectParent(LevelPeace levelPeace, LevelInfo levelInfo){
            _levelPeace = levelPeace;
            _levelInfo = levelInfo;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player")){
                _levelPeace.CheckingCollisionEnter(_levelInfo);
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.CompareTag("Player")){
                _levelPeace.CheckingCollisionExit(_levelInfo);
            }
        }
    }
}