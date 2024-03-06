using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public SpriteRenderer image;
    
    public void SetItem(InventoryItem Setitem) //아이템이 생성될때(드랍될때) 실행되는 함수
    {
        inventoryItem.itemName = Setitem.itemName;
        inventoryItem.itemIcon = Setitem.itemIcon;
        inventoryItem.itemType = Setitem.itemType;

        image.sprite = Setitem.itemIcon;
    }

    public InventoryItem GetItem()//드랍된 아이템을 먹을 때 실행되는 함수 
    {
        return inventoryItem;
    }

    public void DestroyItem() // 드랍된 아이템을 먹고 아이템을 파괴하는 함수
    {
        Destroy(gameObject);
    }
}
