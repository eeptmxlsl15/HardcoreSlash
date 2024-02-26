using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//������ ��ų ���� ���̰� �ϱ�
public class SkillUI : MonoBehaviour
{
    public Sprite[] icon; // ������ ��������Ʈ �迭
    public Image[] item; // UI ������ �̹��� �迭

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
            int skillIndex = GameManager.instance.PickSkills[i]; // ���� ��ų �ε���
            if (skillIndex >= 0 && skillIndex < icon.Length) // ��ȿ�� ��ų �ε����� ��쿡�� ����
            {
                item[i].sprite = icon[skillIndex]; // �ش� UI ������ �̹����� ������ ����
            }
            else
            {
                Debug.LogError("��ȿ���� ���� ��ų �ε����Դϴ�: " + skillIndex);
            }
        }
    }
}
