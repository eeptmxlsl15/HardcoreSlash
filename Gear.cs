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
        switch (type) // applyGear에 안들어가도 되는 플레이어 수치 바꾸는 것들
        {
            case ItemData.ItemType.CriticalChance:
                GameManager.instance.criticalChance += Mathf.FloorToInt(rate);
                break;
            case ItemData.ItemType.CriticalMultiple:
                GameManager.instance.criticalMultiple += rate;
                break;
            case ItemData.ItemType.MaxHealth:
                GameManager.instance.maxHealth += rate;
                GameManager.instance.health += rate;
                break;
            case ItemData.ItemType.ProjectileSpeed:
                GameManager.instance.projectileSpeed += rate;
                break;
            case ItemData.ItemType.AddProjectile:
                GameManager.instance.projectileNum += Mathf.FloorToInt(rate);
                break;
            case ItemData.ItemType.BaseDamage:
                GameManager.instance.baseDamage += Mathf.FloorToInt(rate);
                break;
            case ItemData.ItemType.IncDamage:
                GameManager.instance.incDamage += rate;
                break;
            case ItemData.ItemType.SpeedPerDamage:
                GameManager.instance.MoveSpeedPerDmg += rate;
                break;
            
        }

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
                case 3: //전사 회오리 바람
                    weapon.speed = 300 * (1f + rate);
                    break;
                case 9:
                    weapon.speed = 150 * (1f + rate);
                    break;
                case 2://만월
                case 0://늑대이빨
                    weapon.speed = 1f * (1f - rate);
                    break;

                case 1://아돌
                    weapon.speed = 2f * (1f - rate);
                    //Adol();
                    break;
               

                
                case 4: //강타
                    weapon.speed = 2f * (1f - rate);
                    break;
                case 5://도약강타
                    weapon.speed = 2f * (1f - rate);
                    break;
                case 6: //다발 사격
                    weapon.speed = 1f * (1f - rate);
                    break;
                case 7: //관통 사격
                    weapon.speed = 1.5f * (1f - rate);
                    break;
                case 8: //연속 사격
                    weapon.speed = 0.5f * (1f - rate);
                    break;
                
                case 10://단검 던지기
                    weapon.speed = 1f * (1f - rate);

                    break;
                case 11://원거리 무기 공속
                    weapon.speed = 0.6f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 5 ; // 기본 스피드 // 캐릭 특성
        GameManager.instance.player.moveSpeed = 5 + speed * rate;

    }
}
