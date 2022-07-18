using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.OcclusionCulling.LoadLevels
{
    public class LevelPeace : MonoBehaviour
    {
#region Var
        [Header("Top Levels")]
        [SerializeField] LevelInfo _leftUp;
        [SerializeField] LevelInfo _up;
        [SerializeField] LevelInfo _rightUp;

        [Header("Middle Levels")]
        [SerializeField] LevelInfo _left;
        [SerializeField] LevelInfo _middle;
        [SerializeField] LevelInfo _right;

        [Header("Botton Levels")]
        [SerializeField] LevelInfo _leftDown;
        [SerializeField] LevelInfo _down;
        [SerializeField] LevelInfo _rightDown;
#endregion

#region Stating
        void Start() {
            
            //Top Levels
            _leftUp.SelectParent(this);
            _up.SelectParent(this);
            _rightUp.SelectParent(this);
            
            //Middle Levels
            _left.SelectParent(this);
            _middle.SelectParent(this);
            _right.SelectParent(this);
            
            //Botton Levels
            _leftDown.SelectParent(this);
            _down.SelectParent(this);
            _rightDown.SelectParent(this);
        }
#endregion

#region Public Methods
        public void CheckingCollisionEnter(LevelInfo levelInfo){

            // Verificar se o colisor faz parte do pedaço de level atual

            // Se não for inicie o estado de espera pra trocar os leveis
        }

        public void CheckingCollisionExit(LevelInfo levelInfo){

            // Verificar se o colisor faz parte do pedaço de level atual

            // Se não for ativar  a troca de fases
        }
#endregion

    }

#region Classes
    [System.Serializable]
    public class LevelInfo
    {
#region Var
        [SerializeField] Transform _spawnPoint;
        [SerializeField] LevelCollisionCheck _checkCollisionLevel;
        [SerializeField] SceneField _directionScene;
#endregion

#region Getters and Setters
        public Transform SpawnPoint { get { return _spawnPoint; } }
#endregion

#region Public Methods
        public void SelectParent(LevelPeace levelPeace){
            if(_checkCollisionLevel != null)
                _checkCollisionLevel.SelectParent(levelPeace, this);
        }
#endregion

    }
#endregion

}