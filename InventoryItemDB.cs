using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDB : MonoBehaviour
{
    public static InventoryItemDB instance;
    public GameObject fieldItemPrefab;
    public Vector3[] pos;//��� ��ġ
    public List<InventoryItem> inventoryItemDB = new List<InventoryItem>();
    private void Awake()
    {
        instance = this;

    }

    void Start()
    {for(int i =0; i < 3; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);

            go.GetComponent<FieldItem>().SetItem(inventoryItemDB[Random.Range(0, inventoryItemDB.Count)]);
        }
    }

    //������� 
    

}
