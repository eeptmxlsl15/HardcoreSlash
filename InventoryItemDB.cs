using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDB : MonoBehaviour
{
    public static InventoryItemDB instance;
    public GameObject fieldItemPrefab;
    public Vector3[] pos;//��� ��ġ
    public List<IventoryItem> IventoryItemDB = new List<IventoryItem>();
    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        
    }

}
