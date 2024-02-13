using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public Vector2 inputVec;//����Ű ��ǲ
    public float moveSpeed;
    public Scanner scanner;//�츮�� ���� Ŭ������ ������Ʈ�� ������
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    public RuntimeAnimatorController[] animCon; // �÷��̾� ĳ����



    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
    }

    void OnEnable()//ĳ���� �ִϸ��̼� ���� ����
    {

        //speed *= Character.Speed;//ĳ�� Ư��
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void Update()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal"); //���� ���� ������
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()//�������������Ӹ��� ȣ���ϴ� �Լ�
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;//���ư��� ����
        rigid.MovePosition(rigid.position + nextVec);//������ġ+���ư� ����
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.y > 0.01)
            anim.SetBool("Height", true);
        else if (inputVec.y < -0.01)
            anim.SetBool("Height", false);
    }
    void OnCollisionStay2D(Collision2D collision)//�÷��̾�� ���Ͱ� �⵿�� ü�� ����
    {
        if (!GameManager.instance.isLive)//������ ����ǰ� �־���Ѵٴ� ����
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)//�÷��̾ ���� ��� �÷��̾��� �ڽĵ��� ��Ȱ��ȭ �ؾ���
        {
            for (int index = 1; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);//�ڽ� ������Ʈ�� Ʈ�������� ��ȯ
            }

            anim.SetTrigger("Dead");//�ִϸ��̼� ����
            GameManager.instance.GameOver();
        }
    }
}
