using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager.InputManagement
{
    public class BaseInputs 
    {
        public virtual void EnterInput(){}
        public virtual void UseInput(InputUse use){}
        public virtual void ExitInput(){}
    }

    public class InputUse
    {
        public Vector2Int vector2IntValue = new Vector2Int();

        public void SetValue(int x, int y){
            vector2IntValue.x = x;
            vector2IntValue.y = y;
        }

        public InputUse() { }

        public InputUse(Vector2Int vector2IntValue) { this.vector2IntValue = vector2IntValue; }
    }

    public class BaseSimpleInput : BaseInputs
    {
        protected delegate void InputBase();
		protected event InputBase inputBase;

        public override void UseInput(InputUse use) => inputBase?.Invoke();
    }

    public class BaseVector2IntInput : BaseInputs
    {
        protected delegate void Vector2IntInputBase(Vector2Int vector2IntValue);
		protected event Vector2IntInputBase vector2IntInputBase;

		public override void UseInput(InputUse use) => vector2IntInputBase?.Invoke(use.vector2IntValue);
    }
}