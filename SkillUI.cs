using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//������ ��ų ���� ���̰� �ϱ�
public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite[] icon; // ������ ��������Ʈ �迭
    public Image[] item; // UI ������ �̹��� �迭
    public List<int> itemList;
    int skillIndex;
    public Transform ItemGroup;
    void Update()
    {



        // PickSkills �迭�� ��� ���� ������ �迭�� ���̺��� ū�� Ȯ��
        if (GameManager.instance.PickSkills.Count > item.Length)
        {
            Debug.LogError("PickSkills �迭�� ���̰� UI �������� ������ Ů�ϴ�.");
            return;
        }

        // PickSkills �迭�� ��� ����ŭ �ݺ��Ͽ� UI �����ۿ� �ش��ϴ� �������� ����
        for (int i = 0; i < GameManager.instance.PickSkills.Count; i++)
        {
            skillIndex = GameManager.instance.PickSkills[i]; // ���� ��ų �ε���
            if (skillIndex >= 0 && skillIndex < icon.Length) // ��ȿ�� ��ų �ε����� ��쿡�� ����
            {
                item[i].sprite = icon[skillIndex]; // �ش� UI ������ �̹����� ������ ����
                if(!itemList.Contains(skillIndex)) // ���ٸ� ��ų �ε����� ����Ʈ�� �߰�
                itemList.Add(skillIndex);
            }
            else
            {
                Debug.LogError("��ȿ���� ���� ��ų �ε����Դϴ�: " + skillIndex);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺 Ŀ���� ������Ʈ ���� ���� �� ����Ǵ� �ڵ�
        Debug.Log("Mouse entered!"+ item[0]);

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺 Ŀ���� ������Ʈ���� ��� �� ����Ǵ� �ڵ�
        Debug.Log("Mouse exited!");
    }
}
