using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour // 캐릭들의 특성을 한번에 다루는 스크립트
{
    public static float Speed // 함수가 아닌 속성 // 플레이어의 온에이블, 기어의 스피드
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f:1f; }//플레이어 아이디가 0이면 1.1f 아니면 1f 
    }

    public static float WeaponRate // 함수가 아닌 속성 // //원거리 공속//웨폰의 이닛에 스위치,기어 레이트업 // 무기 공격 속도
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }//플레이어 아이디가 0이면 1.1f 아니면 1f 
    }
    public static float WeapobSpeed // 함수가 아닌 속성 // 웨폰의 이닛에 스위치,기어 레이트업 // 무기 회전 속도
    {
        get { return GameManager.instance.playerId == 2 ? 1.1f : 1f; }//플레이어 아이디가 0이면 1.1f 아니면 1f 
    }

    /*
    public static int Count // 함수가 아닌 속성 // 웨폰에 이닛 , 레벨업
    {
    
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }//플레이어 아이디가 0이면 1.1f 아니면 1f 
    }
    */
    public static float Damage // 함수가 아닌 속성 // 웨폰에 이닛 , 레벨업 // 숫자 아직 안정함
    {
        get { return GameManager.instance.playerId == 3 ? 1.2f : 1f; }//플레이어 아이디가 0이면 1.1f 아니면 1f 
    }
}
