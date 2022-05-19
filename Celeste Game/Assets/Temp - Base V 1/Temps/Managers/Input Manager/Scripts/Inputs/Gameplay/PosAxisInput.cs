using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class PosAxisInput : BaseVector2Input 
    {
        public override void EnterInput()
        {
            //vector2InputBase += GameManager.instance.Data.PlayerController.m_playerMove.MovePlayer;
            //vector2InputBase += GameManager.instance.Data.MapInterationManager.ClickedTile;
        }
        
        public override void ExitInput()
        {
            //vector2InputBase -= GameManager.instance.Data.PlayerController.m_playerMove.MovePlayer;
            //vector2InputBase -= GameManager.instance.Data.MapInterationManager.ClickedTile;
        }
    }
}