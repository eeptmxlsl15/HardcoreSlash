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
        switch (type) // applyGear�� �ȵ��� �Ǵ� �÷��̾� ��ġ �ٲٴ� �͵�
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
                case 3: //���� ȸ���� �ٶ�
                    weapon.speed = 300 * (1f + rate);
                    break;
                case 9:
                    weapon.speed = 150 * (1f + rate);
                    break;
                case 2://����
                case 0://�����̻�
                    weapon.speed = 1f * (1f - rate);
                    break;

                case 1://�Ƶ�
                    weapon.speed = 2f * (1f - rate);
                    //Adol();
                    break;
               

                
                case 4: //��Ÿ
                    weapon.speed = 2f * (1f - rate);
                    break;
                case 5://���భŸ
                    weapon.speed = 2f * (1f - rate);
                    break;
                case 6: //�ٹ� ���
                    weapon.speed = 1f * (1f - rate);
                    break;
                case 7: //���� ���
                    weapon.speed = 1.5f * (1f - rate);
                    break;
                case 8: //���� ���
                    weapon.speed = 0.5f * (1f - rate);
                    break;
                
                case 10://�ܰ� ������
                    weapon.speed = 1f * (1f - rate);

                    break;
                case 11://���Ÿ� ���� ����
                    weapon.speed = 0.6f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 5 ; // �⺻ ���ǵ� // ĳ�� Ư��
        GameManager.instance.player.moveSpeed = 5 + speed * rate;

    }
}
