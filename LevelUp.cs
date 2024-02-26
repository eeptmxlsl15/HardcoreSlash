using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//������â�� Ű�� �ݴ� ��ũ��Ʈ
//������ ���� ���� ��Ƽ�� ��ų - ���� �нú� ��ų - ���� ��ų
//������ �߰��ɋ����� �ٲ���,,,
public class LevelUp : MonoBehaviour
{
    
    
    
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);//�����ֵ��� �ֱ� ������ true�� �ִ´�
    }

    public void Show()
    {
        Next();
        GameManager.instance.Stop();//�ð� ����
        
        rect.localScale = Vector3.one;//������ ui�� ������
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);//������ ȿ���� �Ҹ�
        AudioManager.instance.EffectBgm(true);//����� ����
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;//������ iu�� �����鼭
        GameManager.instance.Resume();//�ð� �簳

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);//������ ȿ���� �Ҹ�
        AudioManager.instance.EffectBgm(false);//����� ����
    }

    public void Select(int index)
    {
        Debug.Log("����Ʈ");
       
        items[index].OnClick();

      
    }

    //�������� �¿��̺� �Լ��� ���� ��Ȱ��ȭ �ϰ� ������ 3���� ������ ��ũ��Ʈ

   void Next()
    {
        //1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        //2. �� �� ���� 3�� ������ Ȱ��ȭ
        int[] ran = new int[3];//���� 3��
        if (GameManager.instance.level == 0 && GameManager.instance.playerId == 0) //ó�� ���� ������ ���
        {
            ran[0] = 0;//5���ΰ�� ���̴� 5���� 0 1 2 3 4�̴�
            ran[1] = 1;
            ran[2] = 2;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 1) //ó�� ���� ������ ���
        {
            ran[0] = 3;//5���ΰ�� ���̴� 5���� 0 1 2 3 4�̴�
            ran[1] = 4;
            ran[2] = 4;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 2) //ó�� ���� �ü��� ���
        {
            ran[0] = 6;//5���ΰ�� ���̴� 5���� 0 1 2 3 4�̴�
            ran[1] = 7;
            ran[2] = 8;

        }
        else if (GameManager.instance.level == 0 && GameManager.instance.playerId == 3) //ó�� ���� ������ ���
        {
            ran[0] = 9;//5���ΰ�� ���̴� 5���� 0 1 2 3 4�̴�
            ran[1] = 10;
            ran[2] = 10;

        }
        else if ( GameManager.instance.PickSkills.Count == 5)
        {
            ran[0] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
            ran[1] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
            ran[2] = GameManager.instance.PickSkills[Random.Range(0, GameManager.instance.PickSkills.Count)];
        }
        else // �ι�°����
        {foreach(int items in GameManager.instance.AllSkills)
            {
                Debug.Log(items);
            }
            while (true)
            {
                
                ran[0] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];//5���ΰ�� ���̴� 5���� 0 1 2 3 4�̴�
                ran[1] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];
                ran[2] = GameManager.instance.AllSkills[Random.Range(0, GameManager.instance.AllSkills.Count)];

               
                if (ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0])
                    break;
            }
        }

        for(int index=0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

        //3. ���� �������� ��� �Һ���������� ��ü
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
