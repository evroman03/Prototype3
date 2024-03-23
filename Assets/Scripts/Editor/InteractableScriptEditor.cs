using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableScripEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        DrawDefaultInspector();
        if(interactable.type == Interactable.InteractableType.Pirate || interactable.type == Interactable.InteractableType.RoyalGalleon || interactable.type == Interactable.InteractableType.Merchant)
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
        }
    }
}
