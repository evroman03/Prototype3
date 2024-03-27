using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;


[CustomEditor(typeof(Tile))]
public class IslandScriptEditor: Editor
{
    public override void OnInspectorGUI()
    {
        Tile tile = (Tile)target;
        DrawDefaultInspector();
        switch (tile.type)
        {
            case Tile.TileType.Island:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Total Loot On Island");
                tile.lootAmount = EditorGUILayout.IntField(tile.lootAmount, GUILayout.MaxWidth(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Chance to find loot");
                tile.lootChance = EditorGUILayout.IntField(tile.lootChance, GUILayout.MaxWidth(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Soldiers, natives, and beasts...");
                tile.Hostiles = EditorGUILayout.IntField(tile.Hostiles, GUILayout.MaxWidth(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(new GUIContent("Island details", "Yeah i know the text field doesnt show up idk"));
                tile.description = EditorGUILayout.TextArea(tile.description, EditorStyles.textArea, GUILayout.Height(100));
                EditorGUILayout.EndVertical();
                break;
            case Tile.TileType.PirateCove:

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(new GUIContent("Pirate Cove details", "Yeah i know the text field doesnt show up idk"));
                tile.description = EditorGUILayout.TextArea(tile.description, EditorStyles.textArea, GUILayout.Height(100));
                EditorGUILayout.EndVertical();
                break;
            case Tile.TileType.Ocean:

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(new GUIContent("Ocean details", "Yeah i know the text field doesnt show up idk"));
                tile.description = EditorGUILayout.TextArea(tile.description, EditorStyles.textArea, GUILayout.Height(100));
                EditorGUILayout.EndVertical();
                break;
            case Tile.TileType.RoyalPort:

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Total Loot On Island");
                tile.lootAmount = EditorGUILayout.IntField(tile.lootAmount, GUILayout.MaxWidth(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Soldiers, natives, and beasts...");
                tile.Hostiles = EditorGUILayout.IntField(tile.Hostiles, GUILayout.MaxWidth(50));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(new GUIContent("Royal Port details", "Yeah i know the text field doesnt show up idk"));
                tile.description = EditorGUILayout.TextArea(tile.description, EditorStyles.textArea, GUILayout.Height(100));
                EditorGUILayout.EndVertical();
                break;
        }
    }
    
}
