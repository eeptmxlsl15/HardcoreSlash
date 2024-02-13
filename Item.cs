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
        //아이템은 텍스트 레벨과 아이템을 자식으로
        //두기 때문에 컴컴포넌츠로 가져오면 
        //이미지인 자기 자신과 아이템을 배열로 가져와서 
        //1로 두번째인 아이콘만 가져옴
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];//텍스트는 하나이므로 
        textName = texts[1];//캔버스 - 레벨업 -  패널 - 아이템 그룹 - 해당 아이템의 인스펙터 상의 순서
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    void OnEnable()//아이템 ui 텍스트
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

  
    public void OnClick() // 아이템이 선택 되었을때
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
                else // 스킬의 데미지 레벨 업 수식
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
                GameManager.instance.health = GameManager.instance.maxHealth;//헬스 누르면 피 전부 참
                break;
        }

        

        //최대레벨 도달 시 렙업을 막는 기능
        //damages에 레벨 데이터를  넣었다
        if (level == data.damages.Length)
            GetComponent<Button>().interactable = false;

    }
}
