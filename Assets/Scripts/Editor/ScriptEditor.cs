using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(Tile))]
public class ScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tile tile = (Tile)target;
        DrawDefaultInspector();
        switch (tile.type)
        {
            case Tile.TileType.Island:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Island Details: ");
                tile.option1data = EditorGUILayout.IntField(tile.option1data, GUILayout.MaxWidth(50));
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.EndHorizontal();
                break;
            case Tile.TileType.PirateCove:
                EditorGUILayout.LabelField("Pirate Cove Details: ");
                break;
            case Tile.TileType.Ocean:
                EditorGUILayout.LabelField("Ocean Details: ");
                break;
            case Tile.TileType.RoyalPort:
                EditorGUILayout.LabelField("Royal Port Details: ");
                break;
        }
    }
    
}
