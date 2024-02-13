using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemData data;
    public int level=0;
    public Weapon weapon;
    public Gear gear;
    

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        //�������� �ؽ�Ʈ ������ �������� �ڽ�����
        //�α� ������ ������������ �������� 
        //�̹����� �ڱ� �ڽŰ� �������� �迭�� �����ͼ� 
        //1�� �ι�°�� �����ܸ� ������
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];//�ؽ�Ʈ�� �ϳ��̹Ƿ� 
        textName = texts[1];//ĵ���� - ������ -  �г� - ������ �׷� - �ش� �������� �ν����� ���� ����
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    void OnEnable()//������ ui �ؽ�Ʈ
    {
        textLevel.text = "Lv. " + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc,data.damages[level]*100,data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]*100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }

    }

  
    public void OnClick() // �������� ���� �Ǿ�����
    {
        Debug.Log(data.itemId);
        if (!GameManager.instance.AllSkills.Contains(data.itemId))
        {
            GameManager.instance.AllSkills.Add(data.itemId);
        }



        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:            
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else // ��ų�� ������ ���� �� ����
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;
                    int nextProjectileNum = 0;

                    nextDamage += data.damages[level];
                    nextCount += data.counts[level];
                    nextProjectileNum += data.projectileNum[level];
                    weapon.LevelUp(nextDamage, nextCount, nextProjectileNum);
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    gear = newWeapon.AddComponent<Gear>();
                    gear.Init(data);

                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;//�ｺ ������ �� ���� ��
                break;
        }

        

        //�ִ뷹�� ���� �� ������ ���� ���
        //damages�� ���� �����͸�  �־���
        if (level == data.damages.Length)
            GetComponent<Button>().interactable = false;

    }
}
