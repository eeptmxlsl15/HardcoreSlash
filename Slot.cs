using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public InventoryItem item;
    public Image itemIcon;

    public void UpdateSlotUI()//인벤토리의 슬롯을 얻은 아이템으로 바꿔줌
    {
        itemIcon.sprite = item.itemIcon;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()//아이템을 없애줌
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

}
