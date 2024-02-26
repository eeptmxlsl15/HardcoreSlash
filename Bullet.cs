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
        float firstdamage = (damage + GameManager.instance.baseDamage);//데미지는 무기 베이스 데미지와 크리티컬 데미지.
        // 크리티컬 데미지는 무기 데미지에만 영향을 끼친다
        float passiveAddDmg = GameManager.instance.player.moveSpeed * GameManager.instance.MoveSpeedPerDmg;//다른 패시브 효과로 올라가는 데미지
        
        this.damage = (firstdamage + passiveAddDmg) *(1+GameManager.instance.incDamage);
        //%로 데미지를 증가시키는 계산식

        GameManager.instance.nowDamage = this.damage;
        this.per = per;

        //Debug.Log(firstdamage+"+"+xpassiveAddDmg +"=" + damage );
        if (per > -2)
        {//근접무기는 per를 -2로 하고 원거리 무기가 무한관통 될때 -1로 하자. OnTriggerEnter2D 함수 조건도 바꾸자
            rigid.velocity = dir* GameManager.instance.projectileSpeed;
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
