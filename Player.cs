using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public Vector2 inputVec;//방향키 인풋
    public float moveSpeed;
    public Scanner scanner;//우리가 만든 클래스를 컴포넌트로 가져옴
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    public RuntimeAnimatorController[] animCon; // 플레이어 캐릭터



    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
    }

    void OnEnable()//캐릭터 애니메이션 변경 로직
    {

        //speed *= Character.Speed;//캐릭 특성
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void Update()
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        

        inputVec.x = Input.GetAxisRaw("Horizontal"); //수평 수직 움직임
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()//물리연산프레임마다 호출하는 함수
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;//나아가는 방향
        rigid.MovePosition(rigid.position + nextVec);//현재위치+나아갈 방향
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.y > 0.01)
            anim.SetBool("Height", true);
        else if (inputVec.y < -0.01)
            anim.SetBool("Height", false);
    }
    void OnCollisionStay2D(Collision2D collision)//플레이어와 몬스터가 출동시 체력 감소
    {
        if (!GameManager.instance.isLive)//게임이 실행되고 있어야한다는 필터
            return;
        /*이즈 벙커 써서 해야할듯
        // 플레이어가 Bunker에 있을 때만 자식 오브젝트 비활성화
        if (collision.gameObject.CompareTag("Bunker"))
        {
            DisableChildObjectsFromIndex(4); // 4번째 자식 오브젝트부터 모두 비활성화
        }
        else
        {
            EnableAllChildObjects(); // Bunker에 없는 경우 모든 자식 오브젝트를 활성화
        }
        */
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.health -= Time.deltaTime * 10;
        }

        if (GameManager.instance.health < 0)//플레이어가 죽을 경우 플레이어의 자식들을 비활성화 해야함
        {
            for (int index = 1; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);//자식 오브젝트의 트랜스폼을 반환
            }

            anim.SetTrigger("Dead");//애니메이션 실행
            GameManager.instance.GameOver();
        }
    }

    void DisableChildObjectsFromIndex(int startIndex)
    {
        for (int index = startIndex; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }
    }

    void EnableAllChildObjects()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(true);
        }
    }
}
