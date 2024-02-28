using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDB : MonoBehaviour
{
    public static InventoryItemDB instance;
    public GameObject fieldItemPrefab;
    public Vector3[] pos;//드랍 위치
    public List<InventoryItem> inventoryItemDB = new List<InventoryItem>();
    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        
    }

}
