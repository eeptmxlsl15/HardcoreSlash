using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//선택한 스킬 옆에 보이게 하기
public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite[] icon; // 아이콘 스프라이트 배열
    public Image[] item; // UI 아이템 이미지 배열
    public List<int> itemList;
    int skillIndex;
    public Transform ItemGroup;
    void Update()
    {



        // PickSkills 배열의 요소 수가 아이템 배열의 길이보다 큰지 확인
        if (GameManager.instance.PickSkills.Count > item.Length)
        {
            Debug.LogError("PickSkills 배열의 길이가 UI 아이템의 수보다 큽니다.");
            return;
        }

        // PickSkills 배열의 요소 수만큼 반복하여 UI 아이템에 해당하는 아이콘을 설정
        for (int i = 0; i < GameManager.instance.PickSkills.Count; i++)
        {
            skillIndex = GameManager.instance.PickSkills[i]; // 현재 스킬 인덱스
            if (skillIndex >= 0 && skillIndex < icon.Length) // 유효한 스킬 인덱스인 경우에만 실행
            {
                item[i].sprite = icon[skillIndex]; // 해당 UI 아이템 이미지에 아이콘 설정
                if(!itemList.Contains(skillIndex)) // 없다면 스킬 인덱스를 리스트에 추가
                itemList.Add(skillIndex);
            }
            else
            {
                Debug.LogError("유효하지 않은 스킬 인덱스입니다: " + skillIndex);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 커서가 오브젝트 위에 있을 때 실행되는 코드
        Debug.Log("Mouse entered!"+ item[0]);

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 커서가 오브젝트에서 벗어날 때 실행되는 코드
        Debug.Log("Mouse exited!");
    }
}
