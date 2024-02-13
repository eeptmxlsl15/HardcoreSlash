using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate; // ������ ������

    public void Init(ItemData data) // �ʱ�ȭ �Լ�
    {
        //Basic set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();


    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();

    }

    void ApplyGear() // �尩�� �Ź� ����
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp() //�÷��̾ ������ �ִ� ��� ������ ����
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach(Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 3:
                    weapon.speed = 250 * (1f + rate);
                    break;
                case 9:
                    weapon.speed = 250 * (1f + rate);
                    break;
                default:
                    
                    weapon.speed = weapon.speed*(1f-rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3 ; // �⺻ ���ǵ� // ĳ�� Ư��
        GameManager.instance.player.moveSpeed = 3 + speed * rate;

    }
}
