using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public GameObject mapSelectionUI; // 맵 선택 UI
    public Button[] mapButtons; // 맵 선택 버튼 배열
    public Transform[] mapStartPositions; // 맵 시작 위치 배열
    public GameObject player; // 플레이어 오브젝트
    public GameObject[] Map;


    private void Start()
    {
        // 맵 선택 UI 비활성화
        mapSelectionUI.SetActive(false);
        

        // 각 맵 버튼에 이벤트 리스너 추가
        for (int i = 0; i < mapButtons.Length; i++)
        {
            int index = i; // 클로저를 사용하여 반복문 내에서 올바른 인덱스를 전달
            mapButtons[i].onClick.AddListener(() => SelectMap(index));
        }

    }

    // 특정 오브젝트에 닿으면 맵 선택 UI를 활성화
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapSelectionUI.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapSelectionUI.SetActive(false);
        }
    }

    // 맵 선택 시 호출되는 함수
    public void SelectMap(int mapIndex)
    {
        // 맵 선택 시 맵 생성
        Map[mapIndex].SetActive(true);
        GameManager.instance.difficult = mapIndex + 1;
        // 선택된 맵의 시작 위치로 플레이어 이동
        if (mapIndex < mapStartPositions.Length)
        {
            player.transform.position = mapStartPositions[mapIndex].position;
        }

        // 맵 선택 UI 비활성화
        mapSelectionUI.SetActive(false);
    }
}