using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Celeste
{
    public class Actor : MonoBehaviour
    {
        public virtual void Added(Scene scene){}
        public virtual void Removed(Scene scene){}
        public virtual void SceneEnd(Scene scene){}
        public virtual void Render(){}
        public virtual void DebugRender(Camera camera){}

        public virtual bool IsRiding(Solid solid){ return false; }
        public virtual bool IsRiding(JumpThru jumpThru){ return false; }
        
        protected virtual void OnSquish(CollisionData data){}
    }

    public class PlayerInventory {}
    public class PlayerDeadBody {}
    public class Holdable {}
    public class Solid {}
    public class JumpThru {}
    public class CollisionData {}
    public class WallBooster {}
    public class Booster {}
    public class Level {}
    public class SoundSource {}
    public class ParticleType {}
    public class Facings {}
    public class PlayerSprite {}
    public class PlayerHair {}
    public class StateMachine {}
    public class Leader {}
    public class VertexLight {}
    public class Hitbox { public Hitbox(int x, int y, int z, int i) { } }
    public class Trigger {}
    public class Entity {}
    public class MirrorReflection {}
    public class PlayerSpriteMode {}
    public class DreamBlock {}
    public class BloomPoint {}
    public class SimpleCurve {}
    public class Tween {}
    public class ChaserState {}
    public class ChaserStateSound {}

    public class Calc
    {
        public const int DtR = 0;

        public static Color HexToColor(string hexColor)
        {
            Color color;
			if ( ColorUtility.TryParseHtmlString("#9D0D82", out color))
                return color;
            return Color.white;
        }
    }
    //public class Chooser<string> { public IEnumerator<string> data() { get; set; } }

    namespace FMOD.Studio{ public class EventInstance {} }
}