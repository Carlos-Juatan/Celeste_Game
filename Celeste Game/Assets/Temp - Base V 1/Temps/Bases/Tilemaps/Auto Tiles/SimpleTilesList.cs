using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Tilemaps.Tiles
{
    [System.Serializable]
    public class SimpleTilesList
    {
#region Var
        [Header("Top Tiles Block")]
        [SerializeField] Sprite[] _upLeft;
        [SerializeField] Sprite[] _up;
        [SerializeField] Sprite[] _upRight;

        [Header("Side Tiles Block")]
        [SerializeField] Sprite[] _left;
        [SerializeField] Sprite[] _right;

        [Header("Middle Tiles Block")]
        [SerializeField] Sprite[] _center;

        [Header("Bottom Tiles Block")]
        [SerializeField] Sprite[] _downLeft;
        [SerializeField] Sprite[] _down;
        [SerializeField] Sprite[] _downRight;

        [Header("Inside Corner Tiles Block")]
        [SerializeField] Sprite[] _insideCornerUpLeft;
        [SerializeField] Sprite[] _insideCornerUpRight;
        [SerializeField] Sprite[] _insideCornerDownLeft;
        [SerializeField] Sprite[] _insideCornerDownRight;

        [Header("Inside Double Corner Tiles Block")]
        [SerializeField] Sprite[] _upLeft_upRight;
        [SerializeField] Sprite[] _downLeft_downRight;
        [SerializeField] Sprite[] _upLeft_downLeft;
        [SerializeField] Sprite[] _upRight_downRight;
        [SerializeField] Sprite[] _upLeft_downRight;
        [SerializeField] Sprite[] _upRight_downleft;

        [Header("Inside Triple Corner Tiles Block")]
        [SerializeField] Sprite[] _upLeft_upRight_downLeft;
        [SerializeField] Sprite[] _upLeft_upRight_downRight;
        [SerializeField] Sprite[] _downLeft_downRight_upLeft;
        [SerializeField] Sprite[] _downLeft_downRight_upRight;

        [Header("Inside Solo Center Corner Tiles Block")]
        [SerializeField] Sprite[] _soloCenter;

        [Header("Solo Tiles Block")]
        [SerializeField] Sprite[] _soloSingle;
        [SerializeField] Sprite[] _soloHorizontal;
        [SerializeField] Sprite[] _soloVertical;
        [SerializeField] Sprite[] _soloUp;
        [SerializeField] Sprite[] _soloDown;
        [SerializeField] Sprite[] _soloLeft;
        [SerializeField] Sprite[] _soloRight;

        [Header("Solo Corners Block")]
        [SerializeField] Sprite[] _soloUp_Left;
        [SerializeField] Sprite[] _soloUp_Right;
        [SerializeField] Sprite[] _soloDown_Left;
        [SerializeField] Sprite[] _soloDown_Right;

        [Header("T Solo Corners Block")]
        [SerializeField] Sprite[] _soloUp_Down_Left;
        [SerializeField] Sprite[] _soloUp_Down_Right;
        [SerializeField] Sprite[] _soloLeft_Right_Up;
        [SerializeField] Sprite[] _soloLeft_Right_Down;

        [Header("Inside T Solo Corners Block")]
        [SerializeField] Sprite[] _tInsideCornerUp_Left_up;
        [SerializeField] Sprite[] _tInsideCornerUp_Left_left;
        [SerializeField] Sprite[] _tInsideCornerUp_Right_up;
        [SerializeField] Sprite[] _tInsideCornerUp_Right_right;
        [SerializeField] Sprite[] _tInsideCornerDown_Left_down;
        [SerializeField] Sprite[] _tInsideCornerDown_Left_left;
        [SerializeField] Sprite[] _tInsideCornerDown_Right_down;
        [SerializeField] Sprite[] _tInsideCornerDown_Right_right;

#endregion

#region Get Sprite By Index
        public Sprite GetSpriteByIndex(byte index){
                // 1    2   4
                // 8    0   16
                // 32   64  128
            switch (index){

    #region Solo horizontal ( Left, Middle and Right )
    
        // Right Variations
                case   9: // 8 + 1
                case  12: // 8 + 4
                case  13: // 8 + 4 + 1
                case  41: // 8 + 32
                case  42: // 8 + 32 + 1
                case  44: // 8 + 32 + 4
                case  45: // 8 + 32 + 4 + 1
                case 136: // 8 + 128
                case 137: // 8 + 128 + 1
                case 140: // 8 + 128 + 4
                case 141: // 8 + 128 + 4 + 1
                case 168: // 8 + 128 + 32
                case 169: // 8 + 128 + 32 + 1
                case 172: // 8 + 128 + 32 + 4
                case 173: // 8 + 128 + 32 + 4 + 1
                case 8: return CheckExistence(_soloRight);

        // Horizontal Variations
                case 24: return CheckExistence(_soloHorizontal);

        // Left Variations
                case 16: return CheckExistence(_soloLeft);
    #endregion

    #region Solo vertical ( Top, Middle and bottom )

        // Up Variations
                case 64: return CheckExistence(_soloUp);

        // Vertical Variations
                case 66: return CheckExistence(_soloVertical);

        // Down Variations
                case 2: return CheckExistence(_soloDown);
    #endregion

    #region All Centers

            // Inside Solo Corners
                case  90: return CheckExistence(_soloCenter);

            // Center
                case 255: return CheckExistence(_center);
    #endregion

                default: return CheckSoloSingleExistence();
        }}

    #region Check Sprites Existence
        Sprite CheckExistence(Sprite[] inCheck){

            Sprite img = WhiteImg();

            if(inCheck != null){
                int listLength  = inCheck.Length;
                if(listLength > 0)
                    img = inCheck[Random.Range(0, listLength)];
            }

            return img;
        }

        Sprite CheckSoloSingleExistence(){

            Sprite img = WhiteImg();

            if(_soloSingle != null){
                int listLength  = _soloSingle.Length;
                if(listLength > 0)
                    img = _soloSingle[Random.Range(0, listLength)];
            }

            return img;
        }

        Sprite WhiteImg(){
            Texture2D texture = new Texture2D(8, 8);
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 8.0f);
        }
    #endregion
#endregion
    }
}