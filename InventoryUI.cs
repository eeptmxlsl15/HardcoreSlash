using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel;//프리펩화한 인벤토리 슬롯들
    bool activeInventory = false;
    public Slot[] slots;
    public Transform slotHolder;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        inven = Inventory.instance;

        if (inven == null)
        {
            Debug.Log("Inventory instance is not assigned properly.");
            return;
        }
        slots = slotHolder.GetComponentsInChildren<Slot>();//자식 오브젝트들의 모든 컴포넌트들을 가져 올 수 있다. s 조심
        inven.onChangeItem += RedrawSlotUI;
        //inventoryPanel.SetActive(activeInventory);
        inven.onSlotCountChange += SlotChange;
        Debug.Log(slots.Length);
    }


    private void SlotChange(int val)//슬롯cnt 만큼만 슬롯 활성화
    {
        Debug.Log(inven.SlotCnt);
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){ // i를 누를때마다 껐다 켜지는 로직
            activeInventory = !activeInventory;
            if (activeInventory)
            {
                inventoryPanel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else
                inventoryPanel.transform.localScale = Vector3.zero;
            
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }
    void RedrawSlotUI()//반복문을 통해 슬롯들을 초기화하고 아이템의 개수만틈 슬롯을 채워넣음 
    {
        
       for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

       for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
