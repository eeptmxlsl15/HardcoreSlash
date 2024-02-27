using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;//프리펩화한 인벤토리 슬롯들
    bool activeInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        //inventoryPanel.SetActive(activeInventory);
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
            Debug.Log("i");
        }
    }
}
