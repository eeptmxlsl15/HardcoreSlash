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
    public int itemID;//�ߺ� �Ұ�
    public string itemDesc;
    public int itemCount;//������ ���� ����

    public void inventoryItem(int _itemID , string _itemName , string _itemDesc , ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDesc = _itemDesc;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("itemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;//�ش����� ������ �������� ��.
        //�װ��� ��������Ʈ ���·� ������
    }
    public bool Use()
    {
        return false;
    }
}
