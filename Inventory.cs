using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();//획득한 아이템들을 담을 리스트 
    // Start is called before the first frame update



    #region Singleton 
    //특정 인스턴스가 게임에 단 한개 존재 하게 함
    public static Inventory instance;
    private void Awake()
    {
        Debug.Log("awake");
        if (instance != null)
        {
            Debug.Log("싱글톤");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnSlotCountChange(int val);//아이템 슬롯ui에 추가되게 함
    // 인벤토리의 수를 다른것들이 다 알수 있게 해주는대리자 정의

    public OnSlotCountChange onSlotCountChange;//대리자 인스턴스화

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public int slotCnt;

    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    void Start()
    {
        //slotCnt = 49;
        SlotCnt = 49;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Additem(InventoryItem _item )//획득한 아이템을 리스트에items  추가
    {
        items.Add(_item);
        
        if(onChangeItem!=null)
            onChangeItem.Invoke();
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision) // 아이템을 먹었을때
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItem fieldItems = collision.GetComponent<FieldItem>();
            if (Additem(fieldItems.GetItem()))//먹었을 경우 트루 반환
                fieldItems.DestroyItem();//드랍되어있는 아이템 파괴하는 함수

        }
    }
}
