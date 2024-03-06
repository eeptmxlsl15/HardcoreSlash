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
public class InventoryItem
{

    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemID;//중복 불가
    public string itemDesc;
    public int itemCount;//아이템 소지 개수

    public void inventoryItem(int _itemID , string _itemName , string _itemDesc , ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDesc = _itemDesc;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("itemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;//해당경로의 파일을 가져오는 것.
        //그것을 스프라이트 형태로 가져옴
    }
    public bool Use()
    {
        return false;
    }
}
