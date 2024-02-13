using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;//�ټ��� �˻��ؾ��ؼ� �迭
    public Transform nearsetTarget;//���� ����� Ÿ��
    public Transform randomTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        //���� ���·� �˻��ϰڴ�. 
        //ĳ���� ���� ��ġ, ���� ������(�˻� ����),ĳ���� ����(��� ����),��� ������ ����(��Ÿ�),��� ���̾�
        nearsetTarget = GetNearest(); // �˻��� �͵� �� ���� ����� ��.
        randomTarget = GetRandom();
    }

    Transform GetNearest()
    {
        Transform result = null; //�ӽ� ��ȯ ��
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;// �� ��ġ
            Vector3 targetPos = target.transform.position;// Ÿ���� ��ġ
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)//�Ÿ��� �� ����� ������ �̵�
            {
                diff = curDiff;
                result = target.transform;
            }

        }

        return result;

    }
    Transform GetRandom()
    {
        Transform result = null; //�ӽ� ��ȯ ��
        
        int randomIndex = Random.Range(0, targets.Length); // ������ �ε��� ����
        result = targets[randomIndex].transform;


        return result;
    }
}
