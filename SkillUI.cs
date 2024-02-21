using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//선택한 스킬 옆에 보이게 하기
public class SkillUI : MonoBehaviour
{

    public Sprite[] icon;
    
    
    public Image[] item;
    
    void Update()
    {


        foreach (int index in GameManager.instance.PickSkills)
        {

            item[index].sprite = icon[GameManager.instance.PickSkills[index]];
        }
    }
}
