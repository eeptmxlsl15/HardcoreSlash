using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���� ��ũ��Ʈ
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;//���� ü��
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public Slider healthBarSlider;
    bool isLive;
    
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;//���� �Ƚ��������Ʈ���� ��. �ڷ�ƾ �Լ����� ���.
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
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            //��Ʈ�ɶ� �����̴� ������ ���������. ���� �ִϸ��̼��� ���¿��� ���̽� ���̾�. ���̾ ���̽��� �����ִ�
            return;

        Vector2 dirVec = target.position - rigid.position;//Ÿ�ٰ� �� �����ٵ��� �Ÿ�
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // rigid.velocity = Vector2.zero;// 0���� ���� ������ ���� ���������� �����̵�->���ν�Ƽ�� ���� �̵��� �ݺ���
        
        //spriter.sortingOrder = Mathf.FloorToInt(15 - (transform.position.y));//y���� ��ġ�� ���� ������ order �ݿ�


    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        if (!isLive)
            return;
        spriter.flipX = target.position.x > rigid.position.x;
    }
    void OnEnable()//Ǯ������ ��Ȱ���� ��
    {
        Transform shadow = transform.Find("Shadow");//�ڽ� ������Ʈ �׸���

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        //Ǯ������ �ٽ� Ȱ��ȭ �Ǹ� �׾��� �� ��Ȱ��ȭ �ߴ� �͵��� Ȱ��ȭ �������
        coll.enabled = true;//�׾��⶧���� ���ʿ��� �浹 ���� 
        rigid.simulated = true;//������Ģ ����
        spriter.sortingOrder = 2;//���� 2����
        anim.SetBool("Dead", false);
        shadow.gameObject.SetActive(true);//�׸��� Ȱ��ȭ
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
        Transform shadow = transform.Find("Shadow");//�ڽ� ������Ʈ�� �׸���

        if (!collision.CompareTag("Bullet") || !isLive) // Bullet�� �������� �����ϱ����� ���� ����.��ü�� �浹�ÿ��� ����ġ ��� ���� �������� !isLive �߰�
            return;

        health -= collision.GetComponent<Bullet>().damage;//�������� ��
        
        StartCoroutine(KnockBack());

        if (health > 0)//���� ���������
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);//�Ҹ�
        }
        else//�׾�����
        {
            isLive = false;
            coll.enabled = false;//�׾��⶧���� ���ʿ��� �浹 ���� 
            rigid.simulated = false;//������Ģ ����
            spriter.sortingOrder = 1;//2���� ���� ����
            anim.SetBool("Dead",true);
            GameManager.instance.kill++;//��������� ų�� �߰�
            GameManager.instance.GetExp();
            //���� ���� �߰��� �κ�
            if (shadow != null)
            {
                shadow.gameObject.SetActive(false);
            }
            
            //weapon.SmiteDead(transform);
            Invoke("Dead",15f);//n���� ������ �Լ� ����
            //�������

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);//�Ҹ�
            
        }

    }

    IEnumerator KnockBack()//�ڷ�ƾ �Լ�. ��Ʈ�� �˹�
    {
        yield return wait;//���� �ϳ��� ���� �����ӱ��� ��ٸ�.
        //�÷��̾��� �ݴ�� �˹�
        Vector3 playerPos = GameManager.instance.player.transform.position;//�÷��̾� ��ġ
        Vector3 dirVector = transform.position - playerPos;//�÷��̾�� �ݴ� ����� ũ��
        rigid.AddForce(dirVector.normalized*3,ForceMode2D.Impulse);//ũ�⸦ 1�� �Ϲ�ȭ�ϰ� �˹�ũ��� 3. ���� �˹�ũ�� ���⼭ ����. �������� ���̹Ƿ� impulse
        

    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
    
}
