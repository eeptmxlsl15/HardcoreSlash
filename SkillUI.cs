using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//선택한 스킬 옆에 보이게 하기
public class SkillUI : MonoBehaviour
{
    public Sprite[] icon; // 아이콘 스프라이트 배열
    public Image[] item; // UI 아이템 이미지 배열

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
            int skillIndex = GameManager.instance.PickSkills[i]; // 현재 스킬 인덱스
            if (skillIndex >= 0 && skillIndex < icon.Length) // 유효한 스킬 인덱스인 경우에만 실행
            {
                item[i].sprite = icon[skillIndex]; // 해당 UI 아이템 이미지에 아이콘 설정
            }
            else
            {
                Debug.LogError("유효하지 않은 스킬 인덱스입니다: " + skillIndex);
            }
        }
    }
}
