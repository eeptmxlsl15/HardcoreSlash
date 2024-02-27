using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public IventoryItem iventoryItem;
    public SpriteRenderer image;
    
    public void SetItem(IventoryItem Setitem) //�������� �����ɶ�(����ɶ�) ����Ǵ� �Լ�
    {
        iventoryItem.itemName = Setitem.itemName;
        iventoryItem.itemImage = Setitem.itemImage;
        iventoryItem.itemType = Setitem.itemType;

        image.sprite = Setitem.itemImage;
    }

    public IventoryItem GetItem()//����� �������� ���� �� ����Ǵ� �Լ� 
    {
        return iventoryItem;
    }

    public void DestroyItem() // ����� �������� �԰� �������� �ı��ϴ� �Լ�
    {
        Destroy(gameObject);
    }
}
