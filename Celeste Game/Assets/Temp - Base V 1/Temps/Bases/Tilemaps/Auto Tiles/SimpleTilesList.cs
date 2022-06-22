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

    #region Top Corners Block (Left, Middle and Right)

            // Up Left ( 208 = 128 + 64 + 16 )
                case 209: // 208 + 1
                case 212: // 208 + 4
                case 213: // 208 + 4 + 1
                case 240: // 208 + 32
                case 241: // 208 + 32 + 1
                case 244: // 208 + 32 + 4
                case 245: // 208 + 32 + 4 + 1
                case 208: return CheckExistence(_upLeft);

            // Up Middle ( 248 = 128 + 64 + 32 + 16 + 8 )
                case 249: // 248 + 1
                case 252: // 248 + 4
                case 253: // 248 + 4 + 1
                case 248: return CheckExistence(_up);

            // Up Right ( 104 = 64 + 32 + 8 )
                case 105: // 104 + 1
                case 108: // 104 + 4
                case 109: // 104 + 4 + 1
                case 232: // 104 + 128
                case 233: // 104 + 128 + 1
                case 236: // 104 + 128 + 4
                case 237: // 104 + 128 + 4 + 1
                case 104: return CheckExistence(_upRight);
    #endregion

    #region Middle Corners Block (Left and Right)

            // Middle Left ( 214 = 128 + 64 + 16 + 4 + 2 )
                case 215: // 214 + 1
                case 246: // 214 + 32
                case 247: // 214 + 32 + 1
                case 214: return CheckExistence(_left);

            // Middle Right ( 107 = 64 + 32 + 8 + 2 + 1 )
                case 111: // 107 + 4
                case 235: // 107 + 128
                case 239: // 107 + 128 + 4
                case 107: return CheckExistence(_right);
    #endregion

    #region Bottom Corners Block (Left, Middle and Right)

            // Bottom Left ( 22 = 16 + 4 + 2 )
                case  23: // 22 + 1
                case  54: // 22 + 32
                case  55: // 22 + 32 + 1
                case 150: // 22 + 128
                case 151: // 22 + 128 + 1
                case 182: // 22 + 128 + 32
                case 183: // 22 + 128 + 32 + 1
                case  22: return CheckExistence(_downLeft);

            // Bottom Middle ( 31 = 16 + 8 + 4 + 2 + 1 )
                case  63: // 31 + 32
                case 159: // 31 + 128
                case 191: // 31 + 128 + 32
                case  31: return CheckExistence(_down);

            // Bottom Right ( 11 = 8 + 2 + 1 )
                case  15: // 11 + 4
                case  43: // 11 + 32
                case  47: // 11 + 32 + 4
                case 139: // 11 + 128
                case 143: // 11 + 128 + 4
                case 171: // 11 + 128 + 32
                case 175: // 11 + 128 + 32 + 4
                case  11: return CheckExistence(_downRight);
    #endregion

    #region Solo Corners Block ()

            // "Solo Corners Up_Left ( 80 = 64 + 16 )
                case  81: // 80 + 1
                case  84: // 80 + 4
                case  85: // 80 + 4 + 1
                case 112: // 80 + 32
                case 113: // 80 + 32 + 1
                case 116: // 80 + 32 + 4
                case 117: // 80 + 32 + 4 + 1
                case 80: return CheckExistence(_soloUp_Left);

            // "Solo Corners Up_Right ( 72 = 64 + 8 )
                case  73: // 72 + 1
                case  76: // 72 + 4
                case  77: // 72 + 4 + 1
                case 200: // 72 + 128
                case 201: // 72 + 128 + 1
                case 204: // 72 + 128 + 4
                case 205: // 72 + 128 + 4 + 1
                case 72: return CheckExistence(_soloUp_Right);

            // "Solo Corners Down_Left ( 18 = 16 + 2 )
                case  19: // 18 + 1
                case  50: // 18 + 32
                case  51: // 18 + 32 + 1
                case 146: // 18 + 128
                case 147: // 18 + 128 + 1
                case 178: // 18 + 128 + 32
                case 179: // 18 + 128 + 32 + 1
                case 18: return CheckExistence(_soloDown_Left);

            // "Solo Corners Down_Right ( 10 = 8 + 2 )
                case  14: // 10 + 4
                case  42: // 10 + 32
                case  36: // 10 + 32 + 4
                case 138: // 10 + 128
                case 142: // 10 + 128 + 4
                case 170: // 10 + 128 + 32
                case 174: // 10 + 128 + 32 + 4
                case 10: return CheckExistence(_soloDown_Right);
    #endregion

    #region T Solo Corners Block ()

            // "T Solo Corners Up_Left ( 82 = 64 + 16 + 2 )
                case  83: // 82 + 1
                case 114: // 82 + 32
                case 115: // 82 + 32 + 1
                case 82: return CheckExistence(_soloUp_Down_Left);

            // "T Solo Corners Up_Right ( 74 = 64 + 8 + 2 )
                case  78: // 74 + 4
                case 202: // 74 + 128
                case 206: // 74 + 128 + 4
                case 74: return CheckExistence(_soloUp_Down_Right);

            // "T Solo Corners Down_Left ( 26 = 16 + 8 + 2 )
                case  27: // 26 + 1
                case  30: // 26 + 4
                case  31: // 26 + 4 + 1
                case 26: return CheckExistence(_soloLeft_Right_Up);

            // "T Solo Corners Down_Right ( 88 = 64 + 16 + 8 )
                case 120: // 88 + 32
                case 216: // 88 + 128
                case 248: // 88 + 128 + 32
                case 88: return CheckExistence(_soloLeft_Right_Down);
    #endregion

    #region Inside T Solo Corners Block ()

            // "Inside T Solo Corners Up_Left ( 210 = ( 208 = 128 + 64 + 16 ) + 2 )
                case 211: // 210 + 1
                case 242: // 210 + 32
                case 243: // 210 + 32 + 1
                case 210: return CheckExistence(_tInsideCornerUpLeft_up);

            // "Inside T Solo Corners Up_Right ( 216 = ( 208 = 128 + 64 + 16 ) + 8 )
                case 217: // 216 + 1
                case 220: // 216 + 4
                case 221: // 216 + 4 + 1
                case 216: return CheckExistence(_tInsideCornerUpLeft_left);

            // "Inside T Solo Corners Down_Left ( 106 = ( 104 = 64 + 32 + 8 ) + 2 )
                case 110: // 106 + 4
                case 234: // 106 + 128
                case 238: // 106 + 128 + 4
                case 106: return CheckExistence(_tInsideCornerUpRight_up);

            // "Inside T Solo Corners Down_Right ( 120 = ( 104 = 64 + 32 + 8 ) + 16 )
                case 121: // 120 + 1
                case 124: // 120 + 4
                case 125: // 120 + 4 + 1
                case 120: return CheckExistence(_tInsideCornerUpRight_right);

            // "Inside T Solo Corners Up_Left ( 86 = ( 22 = 16 + 4 + 2 ) + 64 )
                case  87: // 86 + 1
                case 118: // 86 + 32
                case 119: // 86 + 32 + 1
                case 86: return CheckExistence(_tInsideCornerDownLeft_down);

            // "Inside T Solo Corners Up_Right ( 30 = ( 22 = 16 + 4 + 2 ) + 8 )
                case 62: // 30 + 32
                case 158: // 30 + 128
                case 190: // 30 + 128 + 32
                case 30: return CheckExistence(_tInsideCornerDownLeft_left);

            // "Inside T Solo Corners Down_Left ( 75 = ( 11 = 8 + 2 + 1 ) + 64 )
                case  79: // 75 + 4
                case 203: // 75 + 128
                case 207: // 75 + 128 + 4
                case 75: return CheckExistence(_tInsideCornerDownRight_down);

            // "Inside T Solo Corners Down_Right ( 27 = ( 11 = 8 + 2 + 1 ) + 16 )
                case  59: // 27 + 32
                case 155: // 27 + 128
                case 187: // 27 + 128 + 32
                case 27: return CheckExistence(_tInsideCornerDownRight_right);
    #endregion

    #region Solo horizontal ( Left, Middle and Right )
    
        // Right Variations ( 8 )
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
                case  25: // 24 + 1
                case  28: // 24 + 4
                case  29: // 24 + 4 + 1
                case  56: // 24 + 32
                case  57: // 24 + 32 + 1
                case  60: // 24 + 32 + 4
                case  61: // 24 + 32 + 4 + 1
                case 152: // 24 + 128
                case 153: // 24 + 128 + 1
                case 156: // 24 + 128 + 4
                case 157: // 24 + 128 + 4 + 1
                case 184: // 24 + 128 + 32
                case 185: // 24 + 128 + 32 + 1
                case 188: // 24 + 128 + 32 + 4
                case 189: // 24 + 128 + 32 + 4 + 1
                case 24: return CheckExistence(_soloHorizontal);

        // Left Variations ( 16 )
                case  17: // 16 + 1
                case  20: // 16 + 4
                case  21: // 16 + 4 + 1
                case  48: // 16 + 32
                case  49: // 16 + 32 + 1
                case  52: // 16 + 32 + 4
                case  53: // 16 + 32 + 4 + 1
                case 144: // 16 + 128
                case 145: // 16 + 128 + 1
                case 148: // 16 + 128 + 4
                case 149: // 16 + 128 + 4 + 1
                case 176: // 16 + 128 + 32
                case 177: // 16 + 128 + 32 + 1
                case 180: // 16 + 128 + 32 + 4
                case 181: // 16 + 128 + 32 + 4 + 1
                case 16: return CheckExistence(_soloLeft);
    #endregion

    #region Solo vertical ( Top, Middle and bottom )

        // Up Variations ( 64 )
                case  65: // 64 + 1
                case  68: // 64 + 4
                case  69: // 64 + 4 + 1
                case  96: // 64 + 32
                case  97: // 64 + 32 + 1
                case 100: // 64 + 32 + 4
                case 101: // 64 + 32 + 4 + 1
                case 192: // 64 + 128
                case 193: // 64 + 128 + 1
                case 196: // 64 + 128 + 4
                case 197: // 64 + 128 + 4 + 1
                case 224: // 64 + 128 + 32
                case 225: // 64 + 128 + 32 + 1
                case 228: // 64 + 128 + 32 + 4
                case 229: // 64 + 128 + 32 + 4 + 1
                case 64: return CheckExistence(_soloUp);

        // Vertical Variations ( 66 = 64 + 2 )
                case  67: // 66 + 1
                case  70: // 66 + 4
                case  71: // 66 + 4 + 1
                case  98: // 66 + 32
                case  99: // 66 + 32 + 1
                case 102: // 66 + 32 + 4
                case 103: // 66 + 32 + 4 + 1
                case 194: // 66 + 128
                case 195: // 66 + 128 + 1
                case 198: // 66 + 128 + 4
                case 199: // 66 + 128 + 4 + 1
                case 226: // 66 + 128 + 32
                case 227: // 66 + 128 + 32 + 1
                case 230: // 66 + 128 + 32 + 4
                case 231: // 66 + 128 + 32 + 4 + 1
                case 66: return CheckExistence(_soloVertical);

        // Down Variations ( 2 )
                case  3: // 2 + 1
                case  6: // 2 + 4
                case  7: // 2 + 4 + 1
                case  34: // 2 + 32
                case  35: // 2 + 32 + 1
                case  38: // 2 + 32 + 4
                case  39: // 2 + 32 + 4 + 1
                case 130: // 2 + 128
                case 131: // 2 + 128 + 1
                case 134: // 2 + 128 + 4
                case 135: // 2 + 128 + 4 + 1
                case 162: // 2 + 128 + 32
                case 163: // 2 + 128 + 32 + 1
                case 166: // 2 + 128 + 32 + 4
                case 167: // 2 + 128 + 32 + 4 + 1
                case 2: return CheckExistence(_soloDown);
    #endregion

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