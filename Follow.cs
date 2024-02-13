using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 캐릭터를 따라다니게 하는 스크립트. 체력바 ui가 사용
/// </summary>
public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        //월드 좌표를 스크린 좌표로 옮겨줌 이게 없으면 카메라좌표로 가게됨

            
    }
}
