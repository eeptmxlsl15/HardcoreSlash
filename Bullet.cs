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
        float firstdamage = (damage + GameManager.instance.baseDamage);//�������� ���� ���̽� �������� ũ��Ƽ�� ������.
        // ũ��Ƽ�� �������� ���� ���������� ������ ��ģ��
        float passiveAddDmg = GameManager.instance.player.moveSpeed * GameManager.instance.MoveSpeedPerDmg;//�ٸ� �нú� ȿ���� �ö󰡴� ������
        
        this.damage = (firstdamage + passiveAddDmg) *(1+GameManager.instance.incDamage);
        //%�� �������� ������Ű�� ����

        GameManager.instance.nowDamage = this.damage;
        this.per = per;

        //Debug.Log(firstdamage+"+"+xpassiveAddDmg +"=" + damage );
        if (per > -2)
        {//��������� per�� -2�� �ϰ� ���Ÿ� ���Ⱑ ���Ѱ��� �ɶ� -1�� ����. OnTriggerEnter2D �Լ� ���ǵ� �ٲ���
            rigid.velocity = dir* GameManager.instance.projectileSpeed;
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
