using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//레벨업창을 키고 닫는 스크립트
//순서는 직업 전용 액티브 스킬 - 공용 패시브 스킬 - 전직 스킬
//직업이 추가될떄마다 바꾸자,,,
public class LevelUp : MonoBehaviour
{
    
    
    
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);//꺼진애들이 있기 때문에 true를 넣는다
    }

    public void Show()
    {
        Next();
        GameManager.instance.Stop();//시간 멈춤
        
        rect.localScale = Vector3.one;//레벨업 ui를 보여줌
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);//레벨업 효과음 소리
        AudioManager.instance.EffectBgm(true);//배경음 필터
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;//레벨업 iu를 닫으면서
        GameManager.instance.Resume();//시간 재개

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);//레벨업 효과음 소리
        AudioManager.instance.EffectBgm(false);//배경음 필터
    }

    public void Select(int index)
    {
        Debug.Log("셀렉트");
       
        items[index].OnClick();

      
    }

    //아이템의 온에이블 함수를 전부 비활성화 하고 랜덤한 3개를 보여줄 스크립트

   void Next()
    {
        //1. 모든 아이템 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        //2. 그 중 랜덤 3개 아이템 활성화
        int[] ran = new int[3];//슬롯 3개
        if (GameManager.instance.level == 0 && GameManager.instance.playerId == 0) //처음 고르는 무사인 경우
        {
            ran[0] = 0;//5개인경우 기이는 5지만 0 1 2 3 4이다
            ran[1] = 1;
            ran[2] = 2;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 1) //처음 고르는 전사인 경우
        {
            ran[0] = 3;//5개인경우 기이는 5지만 0 1 2 3 4이다
            ran[1] = 4;
            ran[2] = 4;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 2) //처음 고르는 궁수인 경우
        {
            ran[0] = 6;//5개인경우 기이는 5지만 0 1 2 3 4이다
            ran[1] = 7;
            ran[2] = 8;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 3) //처음 고르는 도적인 경우
        {
            ran[0] = 9;//5개인경우 기이는 5지만 0 1 2 3 4이다
            ran[1] = 10;
            ran[2] = 10;

        }
        else if ( GameManager.instance.PickSkills.Count == 5)
        {
            ran[0] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
            ran[1] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
            ran[2] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
        }
        else // 두번째부터
        {foreach(int items in GameManager.instance.AllSkills)
            {
                Debug.Log(items);
            }
            while (true)
            {
                
                ran[0] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];//5개인경우 기이는 5지만 0 1 2 3 4이다
                ran[1] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];
                ran[2] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];

               
                if (ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0])
                    break;
            }
        }

        for(int index=0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

        //3. 만렙 아이템의 경우 소비아이템으로 대체
            if(ranItem.level == ranItem.data.damages.Length)
            {
                //items[14].gameObject.SetActive(true);
            }
            else
            {
              ranItem.gameObject.SetActive(true);

            }
        }
    }
}
