using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;//관통

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
        {//근접무기는 per를 -2로 하고 원거리 무기가 무한관통 될때 -1로 하자. OnTriggerEnter2D 함수 조건도 바꾸자
            rigid.velocity = dir*15f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;
        per--;//관통력이 줄어듦

        if(per == -1)//관통력이 다한다면
        {
            rigid.velocity = Vector2.zero;//비활성화 전에 미리 물리 속도 초기화
            gameObject.SetActive(false);//비활성화
        }
    }

}
