using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        RoyalGalleon, Merchant, Pirate, Kracken, SeaSerpent, DavyJones, Megalodon
    }
    public InteractableType type;

    [HideInInspector] public int ShipDamage, ShipHealth, ShipManpower, ShipLoot, ShipRenownValue;
}
