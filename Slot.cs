using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public InventoryItem item;
    public Image itemIcon;

    public void UpdateSlotUI()//�κ��丮�� ������ ���� ���������� �ٲ���
    {
        itemIcon.sprite = item.itemIcon;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()//�������� ������
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

}
