using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public SpriteRenderer image;
    
    public void SetItem(InventoryItem Setitem) //�������� �����ɶ�(����ɶ�) ����Ǵ� �Լ�
    {
        inventoryItem.itemName = Setitem.itemName;
        inventoryItem.itemImage = Setitem.itemImage;
        inventoryItem.itemType = Setitem.itemType;

        image.sprite = Setitem.itemImage;
    }

    public InventoryItem GetItem()//����� �������� ���� �� ����Ǵ� �Լ� 
    {
        return inventoryItem;
    }

    public void DestroyItem() // ����� �������� �԰� �������� �ı��ϴ� �Լ�
    {
        Destroy(gameObject);
    }
}
