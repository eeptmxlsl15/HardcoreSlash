using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate; // 레벨별 데이터

    public void Init(ItemData data) // 초기화 함수
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

    void ApplyGear() // 장갑과 신발 증가
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

    void RateUp() //플레이어가 가지고 있는 모든 공격의 연사
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
        float speed = 3 ; // 기본 스피드 // 캐릭 특성
        GameManager.instance.player.moveSpeed = 3 + speed * rate;

    }
}
