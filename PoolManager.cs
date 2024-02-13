using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;//�������� �����ϴ� ����

    List<GameObject>[] pools;//Ǯ ����� �ϴ� ����Ʈ��

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //����Ʈ�̱� ������ new�� �����. Ǯ�� ��� �迭 �ʱ�ȭ

        for(int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();//�迭�ȿ� ����Ʈ�� �ʱ�ȭ

        }

    }
    public GameObject Get(int index)//����ִ� ������Ʈ�� ��ȯ�ϴ� �Լ�
    {
        GameObject select = null;

        //������ Ǯ�� ����ִ� ���� ������Ʈ ����
        //�߰��ϸ�  select �Ҵ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)// ���� Ȱ��ȭ�����ʾҴٸ�
            {
                select = item;
                select.SetActive(true);// Ȱ��ȭ
                break;

            }
        }

        //��� �����ִٸ� �����ؼ� select�� �Ҵ�
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);// ������Ʈ�� �����ϴ� �Լ�. ���� , �ڱ� �ڽſ��� ����
            pools[index].Add(select);//pools�� ���


        }
        return select;

    }

    


}
