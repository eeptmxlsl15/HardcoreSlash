using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;//������ȭ�� �κ��丮 ���Ե�
    bool activeInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        //inventoryPanel.SetActive(activeInventory);
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
            Debug.Log("i");
        }
    }
}
