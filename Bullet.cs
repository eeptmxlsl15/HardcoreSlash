using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;//����

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -2)
        {//��������� per�� -2�� �ϰ� ���Ÿ� ���Ⱑ ���Ѱ��� �ɶ� -1�� ����. OnTriggerEnter2D �Լ� ���ǵ� �ٲ���
            rigid.velocity = dir*15f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;
        per--;//������� �پ��

        if(per == -1)//������� ���Ѵٸ�
        {
            rigid.velocity = Vector2.zero;//��Ȱ��ȭ ���� �̸� ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false);//��Ȱ��ȭ
        }
    }

}
