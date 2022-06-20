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

// mUDEI O NOME É PRA APAGAR DEPOIS DE VER QUAIS IMAGENS ERAM QUE ESTAVAM ALI
[Header("mUDEI O NOME")]
[SerializeField] Sprite[] _tInsideCornerUpLeft_up;
[SerializeField] Sprite[] _tInsideCornerUpLeft_left;
[SerializeField] Sprite[] _tInsideCornerUpRight_up;
[SerializeField] Sprite[] _tInsideCornerUpRight_right;
[SerializeField] Sprite[] _tInsideCornerDownLeft_down;
[SerializeField] Sprite[] _tInsideCornerDownLeft_left;
[SerializeField] Sprite[] _tInsideCornerDownRight_down;
[SerializeField] Sprite[] _tInsideCornerDownRight_right;
// APAGAR O BLOCO DE CIMA AI

        [Header("Inside T Solo Corners Block")]
        [SerializeField] Sprite[] _tInsideCornerUpLeft_up;
        [SerializeField] Sprite[] _tInsideCornerUpLeft_left;
        [SerializeField] Sprite[] _tInsideCornerUpRight_up;
        [SerializeField] Sprite[] _tInsideCornerUpRight_right;
        [SerializeField] Sprite[] _tInsideCornerDownLeft_down;
        [SerializeField] Sprite[] _tInsideCornerDownLeft_left;
        [SerializeField] Sprite[] _tInsideCornerDownRight_down;
        [SerializeField] Sprite[] _tInsideCornerDownRight_right;
        
        [Header("Inside T Solo Cross Block")]
        [SerializeField] Sprite[] _tInsideCrossUp_Up;
        [SerializeField] Sprite[] _tInsideCrossDown_Down;
        [SerializeField] Sprite[] _tInsideCrossLeft_Left;
        [SerializeField] Sprite[] _tInsideCrossRight_Right;

#endregion

#region Get Sprite By Index
        public Sprite GetSpriteByIndex(byte index){
                // 1    2   4
                // 8    0   16
                // 32   64  128
            switch (index){

#region Modelo==================================
        
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
#endregion

    #region Top Corners Block (Left, Middle and Right)

            // Up Left ( 208 = 128 + 64 + 16 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case 208: return CheckExistence(_upLeft);

            // Up Middle ( 248 = 128 + 64 + 32 + 16 + 8 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case 248: return CheckExistence(_up);

            // Up Right ( 104 = 64 + 32 + 8 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case 104: return CheckExistence(_upRight);
    #endregion

    #region Middle Corners Block (Left and Right)

            // Middle Left ( 214 = 128 + 64 + 16 + 4 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case 214: return CheckExistence(_left);

            // Middle Right ( 107 = 64 + 32 + 8 + 2 + 1 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case 107: return CheckExistence(_right);
    #endregion

    #region Bottom Corners Block (Left, Middle and Right)

            // Bottom Left ( 22 = 16 + 4 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case  22: return CheckExistence(_downLeft);

            // Bottom Middle ( 31 = 16 + 8 + 4 + 2 + 1 )
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 32
                case  31: return CheckExistence(_down);

            // Bottom Right ( 11 = 8 + 2 + 1 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 4
                case  11: return CheckExistence(_downRight);
    #endregion

    #region Solo Corners Block ()

            // "Solo Corners Up_Left ( 80 = 64 + 16 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case 80: return CheckExistence(_soloUp_Left);

            // "Solo Corners Up_Right ( 72 = 64 + 8 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case 72: return CheckExistence(_soloUp_Right);

            // "Solo Corners Down_Left ( 18 = 16 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case 18: return CheckExistence(_soloDown_Left);

            // "Solo Corners Down_Right ( 10 = 8 + 2 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 4
                case 10: return CheckExistence(_soloDown_Right);
    #endregion

    #region T Solo Corners Block ()

            // "T Solo Corners Up_Left ( 82 = 64 + 16 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case 82: return CheckExistence(_soloUp_Down_Left);

            // "T Solo Corners Up_Right ( 74 = 64 + 8 + 2 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case 74: return CheckExistence(_soloUp_Down_Right);

            // "T Solo Corners Down_Left ( 26 = 16 + 8 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case 26: return CheckExistence(_soloLeft_Right_Up);

            // "T Solo Corners Down_Right ( 88 = 64 + 16 + 8 )
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 32
                case 88: return CheckExistence(_soloLeft_Right_Down);
    #endregion

                // 1    2   4
                // 8    0   16
                // 32   64  128
    #region Inside T Solo Corners Block ()

            // "Inside T Solo Corners Up_Left ( 210 = ( 208 = 128 + 64 + 16 ) + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case 210: return CheckExistence(_tInsideCornerUpLeft_up);

            // "Inside T Solo Corners Up_Right ( 216 = ( 208 = 128 + 64 + 16 ) + 8 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case 216: return CheckExistence(_tInsideCornerUpLeft_left);

            // "Inside T Solo Corners Down_Left ( 106 = ( 104 = 64 + 32 + 8 ) + 2 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case 106: return CheckExistence(_tInsideCornerUpRight_up);

            // "Inside T Solo Corners Down_Right ( 120 = ( 104 = 64 + 32 + 8 ) + 16 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case 120: return CheckExistence(_tInsideCornerUpRight_right);

            // "Inside T Solo Corners Up_Left ( 86 = ( 22 = 16 + 4 + 2 ) + 64 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case 86: return CheckExistence(_tInsideCornerDownLeft_down);

            // "Inside T Solo Corners Up_Right ( 30 = ( 22 = 16 + 4 + 2 ) + 8 )
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 32
                case 30: return CheckExistence(_tInsideCornerDownLeft_left);

            // "Inside T Solo Corners Down_Left ( 75 = ( 11 = 8 + 2 + 1 ) + 64 )
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 4
                case 75: return CheckExistence(_tInsideCornerDownRight_down);

            // "Inside T Solo Corners Down_Right ( 27 = ( 11 = 8 + 2 + 1 ) + 16 )
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 32
                case 27: return CheckExistence(_tInsideCornerDownRight_right);
    #endregion

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

        // Horizontal Variations ( 24 = 16 + 8)
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
                case 24: return CheckExistence(_soloHorizontal);

        // Left Variations
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
                case 16: return CheckExistence(_soloLeft);
    #endregion

    #region Solo vertical ( Top, Middle and bottom )

        // Up Variations
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
                case 64: return CheckExistence(_soloUp);

        // Vertical Variations ( 66 = 64 + 2 )
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
                case 66: return CheckExistence(_soloVertical);

        // Down Variations
                case fdafa: // fdafa + 1
                case fdafa: // fdafa + 4
                case fdafa: // fdafa + 4 + 1
                case fdafa: // fdafa + 32
                case fdafa: // fdafa + 32 + 1
                case fdafa: // fdafa + 32 + 4
                case fdafa: // fdafa + 32 + 4 + 1
                case fdafa: // fdafa + 128
                case fdafa: // fdafa + 128 + 1
                case fdafa: // fdafa + 128 + 4
                case fdafa: // fdafa + 128 + 4 + 1
                case fdafa: // fdafa + 128 + 32
                case fdafa: // fdafa + 128 + 32 + 1
                case fdafa: // fdafa + 128 + 32 + 4
                case fdafa: // fdafa + 128 + 32 + 4 + 1
                case 2: return CheckExistence(_soloDown);
    #endregion

//  |   |   |  Don't Need Variations Any More  |   |   |   |   |
//  |   |   |  Don't Need Variations Any More  |   |   |   |   |
// \|/ \|/ \|/ Don't Need Variations Any More \|/ \|/ \|/ \|/ \|/


    #region Inside T Solo Cross Block ()

            // "Inside T Solo Cross Up_Left ( 250 = ( 248 = 128 + 64 + 32 + 16 + 8 ) +2 )
                case 250: return CheckExistence(_tInsideCrossUp_Up);

            // "Inside T Solo Cross Up_Right ( 95 = ( 31 = 16 + 8 + 4 + 2 + 1 ) + 64 )
                case 95: return CheckExistence(_tInsideCrossDown_Down);

            // "Inside T Solo Cross Down_Left ( 222 = ( 214 = 128 + 64 + 16 + 4 + 2 ) + 8 )
                case 222: return CheckExistence(_tInsideCrossLeft_Left);

            // "Inside T Solo Cross Down_Right ( 123 = ( 107 = 64 + 32 + 8 + 2 + 1 ) + 16 )
                case 123: return CheckExistence(_tInsideCrossRight_Right);
    #endregion

    #region Inside Corners Block (Up_Left, Up_Right, Down_Left and Down_Right)

            // Inside Up_Left ( 254 = 255 - 1 )
                case 254: return CheckExistence(_insideCornerUpLeft);

            // Inside Up_Right ( 251 = 255 - 4 )
                case 251: return CheckExistence(_insideCornerUpRight);

            // Inside Down_Left ( 223 = 255 - 32 )
                case 223: return CheckExistence(_insideCornerDownLeft);

            // Inside Down_Right ( 127 = 255 - 128 )
                case 127: return CheckExistence(_insideCornerDownRight);
    #endregion

    #region Inside Double Corners Block ()

            // Inside Double UpLeft_UpRight ( 250 = 255 - 4 - 1 )
                case 250: return CheckExistence(_upLeft_upRight);

            // Inside Double DownLeft_DownRight ( 95 = 255 - 128 - 32 )
                case  95: return CheckExistence(_downLeft_downRight);

            // Inside Double UpLeft_DownLeft ( 222 = 255 - 32 - 1 )
                case 222: return CheckExistence(_upLeft_downLeft);

            // Inside Double UpRight_DownRight ( 123 = 255 - 128 - 4 )
                case 123: return CheckExistence(_upRight_downRight);

            // Inside Double UpLeft_DownRight ( 126 = 255 - 128 - 1 )
                case 126: return CheckExistence(_upLeft_downRight);

            // Inside Double UpRight_Downleft ( 219 = 255 - 32 - 4 )
                case 219: return CheckExistence(_upRight_downleft);
    #endregion

    #region Inside Triple Corners Block ()

            // Inside Triple Up_Left ( 218 = 255 - 32 - 4 - 1 )
                case 218: return CheckExistence(_upLeft_upRight_downLeft);

            // Inside Triple Up_Right ( 122 = 255 - 128 - 4 - 1 )
                case 122: return CheckExistence(_upLeft_upRight_downRight);

            // Inside Triple Down_Left ( 94 = 255 - 128 - 32 - 1 )
                case 94: return CheckExistence(_downLeft_downRight_upLeft);

            // Inside Triple Down_Right ( 91 = 255 - 128 - 32 - 4 )
                case 91: return CheckExistence(_downLeft_downRight_upRight);
    #endregion

    #region All Centers

            // Inside Solo Corners ( 90 = 255 - 128 - 32 - 4 - 1 )
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