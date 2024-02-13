using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour // ĳ������ Ư���� �ѹ��� �ٷ�� ��ũ��Ʈ
{
    public static float Speed // �Լ��� �ƴ� �Ӽ� // �÷��̾��� �¿��̺�, ����� ���ǵ�
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f:1f; }//�÷��̾� ���̵� 0�̸� 1.1f �ƴϸ� 1f 
    }

    public static float WeaponRate // �Լ��� �ƴ� �Ӽ� // //���Ÿ� ����//������ �̴ֿ� ����ġ,��� ����Ʈ�� // ���� ���� �ӵ�
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }//�÷��̾� ���̵� 0�̸� 1.1f �ƴϸ� 1f 
    }
    public static float WeapobSpeed // �Լ��� �ƴ� �Ӽ� // ������ �̴ֿ� ����ġ,��� ����Ʈ�� // ���� ȸ�� �ӵ�
    {
        get { return GameManager.instance.playerId == 2 ? 1.1f : 1f; }//�÷��̾� ���̵� 0�̸� 1.1f �ƴϸ� 1f 
    }

    /*
    public static int Count // �Լ��� �ƴ� �Ӽ� // ������ �̴� , ������
    {
    
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }//�÷��̾� ���̵� 0�̸� 1.1f �ƴϸ� 1f 
    }
    */
    public static float Damage // �Լ��� �ƴ� �Ӽ� // ������ �̴� , ������ // ���� ���� ������
    {
        get { return GameManager.instance.playerId == 3 ? 1.2f : 1f; }//�÷��̾� ���̵� 0�̸� 1.1f �ƴϸ� 1f 
    }
}
