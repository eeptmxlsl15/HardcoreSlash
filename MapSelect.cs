using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public GameObject mapSelectionUI; // �� ���� UI
    public Button[] mapButtons; // �� ���� ��ư �迭
    public Transform[] mapStartPositions; // �� ���� ��ġ �迭
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject[] Map;


    private void Start()
    {
        // �� ���� UI ��Ȱ��ȭ
        mapSelectionUI.SetActive(false);
        

        // �� �� ��ư�� �̺�Ʈ ������ �߰�
        for (int i = 0; i < mapButtons.Length; i++)
        {
            int index = i; // Ŭ������ ����Ͽ� �ݺ��� ������ �ùٸ� �ε����� ����
            mapButtons[i].onClick.AddListener(() => SelectMap(index));
        }

    }

    // Ư�� ������Ʈ�� ������ �� ���� UI�� Ȱ��ȭ
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

    // �� ���� �� ȣ��Ǵ� �Լ�
    public void SelectMap(int mapIndex)
    {
        // �� ���� �� �� ����
        Map[mapIndex].SetActive(true);
        GameManager.instance.difficult = mapIndex + 1;
        // ���õ� ���� ���� ��ġ�� �÷��̾� �̵�
        if (mapIndex < mapStartPositions.Length)
        {
            player.transform.position = mapStartPositions[mapIndex].position;
        }

        // �� ���� UI ��Ȱ��ȭ
        mapSelectionUI.SetActive(false);
    }
}