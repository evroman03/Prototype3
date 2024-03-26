using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableScripEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        DrawDefaultInspector();
        if(interactable.type == Interactable.InteractableType.Brigantine || interactable.type == Interactable.InteractableType.RoyalGalleon || interactable.type == Interactable.InteractableType.Merchant)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ship Cannon Count");
            interactable.ShipDamage = EditorGUILayout.IntField(interactable.ShipDamage, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ship Health");
            interactable.ShipHealth = EditorGUILayout.IntField(interactable.ShipHealth, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ship Manpower");
            interactable.ShipManpower = EditorGUILayout.IntField(interactable.ShipManpower, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ship Loot");
            interactable.ShipLoot = EditorGUILayout.IntField(interactable.ShipLoot, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ship Renown Value");
            interactable.ShipRenownValue = EditorGUILayout.IntField(interactable.ShipRenownValue, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Catch Player Chance", "Basically, how fast and ready is this ship / 100?"));
            interactable.CatchPlayerChance = EditorGUILayout.IntField(interactable.CatchPlayerChance, GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginVertical();
            //EditorGUILayout.LabelField(new GUIContent("Popup Title", "This isnt the name of the prefab - what will it say when the popup shows? Maybe, 'Ship sighted'?"));
            //interactable.title =EditorGUILayout.TextField(interactable.title);
            //EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Popup Description", "Yeah i know the text field doesnt show up idk"));
            interactable.description = EditorGUILayout.TextArea(interactable.description, EditorStyles.textArea, GUILayout.Height(100));
            EditorGUILayout.EndVertical();
        }
    }
}
