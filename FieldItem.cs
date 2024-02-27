using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public IventoryItem iventoryItem;
    public SpriteRenderer image;
    
    public void SetItem(IventoryItem Setitem) //아이템이 생성될때(드랍될때) 실행되는 함수
    {
        iventoryItem.itemName = Setitem.itemName;
        iventoryItem.itemImage = Setitem.itemImage;
        iventoryItem.itemType = Setitem.itemType;

        image.sprite = Setitem.itemImage;
    }

    public IventoryItem GetItem()//드랍된 아이템을 먹을 때 실행되는 함수 
    {
        return iventoryItem;
    }

    public void DestroyItem() // 드랍된 아이템을 먹고 아이템을 파괴하는 함수
    {
        Destroy(gameObject);
    }
}
