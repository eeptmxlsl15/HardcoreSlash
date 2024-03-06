using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();//ȹ���� �����۵��� ���� ����Ʈ 
    // Start is called before the first frame update



    #region Singleton 
    //Ư�� �ν��Ͻ��� ���ӿ� �� �Ѱ� ���� �ϰ� ��
    public static Inventory instance;
    private void Awake()
    {
        Debug.Log("awake");
        if (instance != null)
        {
            Debug.Log("�̱���");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnSlotCountChange(int val);//������ ����ui�� �߰��ǰ� ��
    // �κ��丮�� ���� �ٸ��͵��� �� �˼� �ְ� ���ִ´븮�� ����

    public OnSlotCountChange onSlotCountChange;//�븮�� �ν��Ͻ�ȭ

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

    public bool Additem(InventoryItem _item )//ȹ���� �������� ����Ʈ��items  �߰�
    {
        items.Add(_item);
        
        if(onChangeItem!=null)
            onChangeItem.Invoke();
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision) // �������� �Ծ�����
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItem fieldItems = collision.GetComponent<FieldItem>();
            if (Additem(fieldItems.GetItem()))//�Ծ��� ��� Ʈ�� ��ȯ
                fieldItems.DestroyItem();//����Ǿ��ִ� ������ �ı��ϴ� �Լ�

        }
    }
}
