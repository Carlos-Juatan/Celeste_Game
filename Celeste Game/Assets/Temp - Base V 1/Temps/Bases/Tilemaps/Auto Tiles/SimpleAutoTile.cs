using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

namespace GameSystems.Tilemaps.Tiles
{
    public class SimpleAutoTile : Tile
    {
#region Var
        public Sprite m_Preview;
        public SimpleTilesList _customTiles;
#endregion

#region Refreshing
        // This refreshes itself and other SimpleAutoTiles that are orthogonally and diagonally adjacent
        public override void RefreshTile(Vector3Int location, ITilemap tilemap)
        {
            for (int yd = -1; yd <= 1; yd++){
                for (int xd = -1; xd <= 1; xd++){
                    Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                    if (CheckTile(tilemap, position))
                        tilemap.RefreshTile(position);
            }}
        }
#endregion

#region Getting Data
        // This determines which sprite is used based on the RoadTiles that are adjacent to it and rotates it to fit the other tiles.
        // As the rotation is determined by the RoadTile, the TileFlags.OverrideTransform is set for the tile.
        public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData){
            base.GetTileData(location, tilemap, ref tileData);

            byte mask = GetMask(tilemap, location);

            tileData.sprite = _customTiles.GetSpriteByIndex(mask);
            //tileData.color = Color.white;
            //tileData.flags = TileFlags.LockTransform;
            //tileData.colliderType = ColliderType.Sprite;
        }
    #region Check Tile
        // This determines if the Tile at the position is the same RoadTile.
        bool CheckTile(ITilemap tilemap, Vector3Int position){

            bool hasSameTile = tilemap.GetTile(position) == this;

            return hasSameTile;
        }
    #endregion

    #region Get Mask
        byte GetMask(ITilemap tilemap, Vector3Int position){
                // 1    2   4
                // 8    0   16
                // 32   64  128
            byte mask = 0;
            mask += CheckTile(tilemap, position + new Vector3Int(-1,  1, 0)) ? (byte)   1 : (byte)0; // Left Up
            mask += CheckTile(tilemap, position + new Vector3Int( 0,  1, 0)) ? (byte)   2 : (byte)0; // Up
            mask += CheckTile(tilemap, position + new Vector3Int( 1,  1, 0)) ? (byte)   4 : (byte)0; // Right Up

            mask += CheckTile(tilemap, position + new Vector3Int(-1,  0, 0)) ? (byte)   8 : (byte)0; // Left Up
            mask += CheckTile(tilemap, position + new Vector3Int( 1,  0, 0)) ? (byte)  16 : (byte)0; // Right Up

            mask += CheckTile(tilemap, position + new Vector3Int(-1, -1, 0)) ? (byte)  32 : (byte)0; // Left Down
            mask += CheckTile(tilemap, position + new Vector3Int( 0, -1, 0)) ? (byte)  64 : (byte)0; // Down
            mask += CheckTile(tilemap, position + new Vector3Int( 1, -1, 0)) ? (byte) 128 : (byte)0; // Right Down

            return mask;
        }
    #endregion
    
#endregion

#region Create Scriptable Objects
#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a Tile Asset
        [MenuItem("Assets/Create/Tilemaps/Auto Tiles")]
        public static void CreateSimpleTile(){

            string path = EditorUtility.SaveFilePanelInProject("Save Simple Tile", "New Simple Tile", "Asset", "Save Sinple Tile", "Assets");
            if (path == "")
                return;
            
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SimpleAutoTile>(), path);
        }
#endif
#endregion

    }
}