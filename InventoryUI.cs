using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel;//������ȭ�� �κ��丮 ���Ե�
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
        slots = slotHolder.GetComponentsInChildren<Slot>();//�ڽ� ������Ʈ���� ��� ������Ʈ���� ���� �� �� �ִ�. s ����
        inven.onChangeItem += RedrawSlotUI;
        //inventoryPanel.SetActive(activeInventory);
        inven.onSlotCountChange += SlotChange;
        Debug.Log(slots.Length);
    }


    private void SlotChange(int val)//����cnt ��ŭ�� ���� Ȱ��ȭ
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
        if(Input.GetKeyDown(KeyCode.I)){ // i�� ���������� ���� ������ ����
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
    void RedrawSlotUI()//�ݺ����� ���� ���Ե��� �ʱ�ȭ�ϰ� �������� ������ƴ ������ ä������ 
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
