using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 몬스터 스크립트
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;//현재 체력
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public Slider healthBarSlider;
    bool isLive;
    
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;//다음 픽스드업데이트까지 쉼. 코루틴 함수에서 사용.
    Weapon weapon;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            //히트될때 움직이는 로직을 멈춰줘야함. 현재 애니메이션의 상태에서 베이스 레이어. 레이어를 베이스만 쓰고있다
            return;

        Vector2 dirVec = target.position - rigid.position;//타겟과 이 리짓바디의 거리
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // rigid.velocity = Vector2.zero;// 0으로 하지 않으면 무브 포지션으로 순간이동->벨로시티로 인한 이동이 반복됨
        
        //spriter.sortingOrder = Mathf.FloorToInt(15 - (transform.position.y));//y값에 위치에 따라 몬스터의 order 반영


    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        if (!isLive)
            return;
        spriter.flipX = target.position.x > rigid.position.x;
    }
    void OnEnable()//풀링에서 재활용할 때
    {
        Transform shadow = transform.Find("Shadow");//자식 컴포넌트 그림자

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        //풀링에서 다시 활성화 되면 죽었을 때 비활성화 했던 것들을 활성화 해줘야함
        coll.enabled = true;//죽었기때문에 불필요한 충돌 방지 
        rigid.simulated = true;//물리법칙 방지
        spriter.sortingOrder = 2;//원래 2였다
        anim.SetBool("Dead", false);
        shadow.gameObject.SetActive(true);//그림자 활성화
        health = maxHealth;

    }
    
    public void Init(SpawnData data)
    {
        data.spriteType = Random.Range(0, 5);
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Transform shadow = transform.Find("Shadow");//자식 오브젝트의 그림자

        if (!collision.CompareTag("Bullet") || !isLive) // Bullet과 닿을때만 적용하기위한 필터 역할.시체와 충돌시에도 경험치 얻는 것을 막기위헤 !isLive 추가
            return;

        health -= collision.GetComponent<Bullet>().damage;//데미지를 줌
        
        StartCoroutine(KnockBack());

        if (health > 0)//몹이 살아있을때
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);//소리
        }
        else//죽었을때
        {
            isLive = false;
            coll.enabled = false;//죽었기때문에 불필요한 충돌 방지 
            rigid.simulated = false;//물리법칙 방지
            spriter.sortingOrder = 1;//2에서 순서 변경
            anim.SetBool("Dead",true);
            GameManager.instance.kill++;//사망했을때 킬수 추가
            GameManager.instance.GetExp();
            //내가 따로 추가한 부분
            if (shadow != null)
            {
                shadow.gameObject.SetActive(false);
            }
            
            //weapon.SmiteDead(transform);
            Invoke("Dead",15f);//n초후 이하의 함수 실행
            //여기까지

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);//소리
            
        }

    }

    IEnumerator KnockBack()//코루틴 함수. 히트시 넉백
    {
        yield return wait;//다음 하나의 물리 프레임까지 기다림.
        //플레이어의 반대로 넉백
        Vector3 playerPos = GameManager.instance.player.transform.position;//플레이어 위치
        Vector3 dirVector = transform.position - playerPos;//플레이어와 반대 방향과 크기
        rigid.AddForce(dirVector.normalized*3,ForceMode2D.Impulse);//크기를 1로 일반화하고 넉백크기는 3. 추후 넉백크기 여기서 조종. 순간적인 힘이므로 impulse
        

    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
    
}
