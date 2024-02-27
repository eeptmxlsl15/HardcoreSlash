using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
    {
        Helmet,
        BodyArmour,
        Boots,
        Gloves,
        Ring,
        Amulet,
        Consumable,
        Sword,
        GreatSword,
        Bow,
        Knife
    }

[System.Serializable]
public class IventoryItem : MonoBehaviour
{

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;
    }
}
