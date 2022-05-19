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
        public Vector2 vector2Value;

        public InputUse() { }

        public InputUse(Vector2 vector2Value) { this.vector2Value = vector2Value; }
    }

    public class BaseSimpleInput : BaseInputs
    {
        protected delegate void InputBase();
		protected event InputBase inputBase;

        public override void UseInput(InputUse use) => inputBase?.Invoke();
    }

    public class BaseVector2Input : BaseInputs
    {
        protected delegate void Vector2InputBase(Vector2 vector2Value);
		protected event Vector2InputBase vector2InputBase;

		public override void UseInput(InputUse use) => vector2InputBase?.Invoke(use.vector2Value);
    }
}