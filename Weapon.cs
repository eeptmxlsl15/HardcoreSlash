using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ǯ�Ŵ������� �޾ƿ� ������� �����ϴ� ��ũ��Ʈ
/// </summary>
public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;//Ǯ �Ŵ��� ������Ʈ�� prefabs�� �����̴�.���� �ι������ 1
    public float damage;
    public float criticalDamage;
    public int projectile=2;
    
    public int count; // ���� ����
    public float speed;//�Ѵ� �����µ� �ɸ��� �ð�
    public float cooltime;
    public bool isCritical = false;
    public int adolCount=0;
    public float increasedRangeCritWF; // ���� ������ ����
    float timer;
    Player player;
    ItemData itemdata;
    Item item;
    

    void Awake()
    {
        player = GameManager.instance.player;


    }

    void Update()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        switch (id)
        {   case 0://�����̻�
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Wolves_Fang();
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;
            case 1://�Ƶ�
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Adol();
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;
            case 2://��������

                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    FullMoon();
                    
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;

            case 3://ȸ�����ٶ�

                
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);//������Ʈ������ �� ��ŸŸ���� ���ؾ� �Ѵ�. �������� �Һ��� �ð�.
                Cyclone();
                break;

            case 4: // ��Ÿ
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Smite();
                    
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;
            case 5: // ���భŸ
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();
                    //if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
                        //return;
                    LeapSlam();
                    
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;
            case 6:  // �ٹ߻��
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;

                    for(int index=0;index < projectile; index++)
                    {if (index == 0)
                            MultipleShot(0);
                        MultipleShot(index*3f);
                        MultipleShot(index *(-3f));

                    }
                    

                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;

            case 7:  // ������
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;

                    PiercingShot();


                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;

            case 8:  // ���
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;

                    SnapShot();


                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;
            case 9://ȸ�� Į��


                transform.Rotate(Vector3.back * speed * Time.deltaTime);//������Ʈ������ �� ��ŸŸ���� ���ؾ� �Ѵ�. �������� �Һ��� �ð�.
                TwistingBlade();
                break;

            case 10://�ܰ� ��ô
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;

                    


                    ThrowingKnife();
                    //Wolves_Fang_Sword(); // Į ����� ���߿� ����
                }
                break;

                break;
            case 11://�ѽ�� ���߿� ���� Į�� ȸ��
                timer += Time.deltaTime;//��� ������ �ð��� ���� ����
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    
                }
                break;
            

        }

        //test

        if (Input.GetButtonDown("Jump"))
        {
            //LevelUp(10,1);
        }

    }

    public void LevelUp(float damage, int count, int ProjectileNum)
    {
        this.damage = damage;
        Debug.Log(damage);
        this.count += count;
        this.criticalDamage = this.damage * GameManager.instance.criticalMultiple;
        this.projectile += ProjectileNum;
        Debug.Log(criticalDamage);
        /*
        if (id == 11) //���� �ܰ��϶���
            Batch();
        */
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

    }
    public void Init(ItemData data)//���� ���� �ʱ�ȭ �Լ�
    {

        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        criticalDamage = damage* GameManager.instance.criticalMultiple;
        // ���������� �����͸� �߰��ؼ� �����鿡 �����Ͱ� �׿�����. ���⿡ ���� ��ų���� �ֵ��� ����
        // ��ũ��Ʈ�� ������Ʈ�� �������� ���ؼ� �ε��� �ƴ� ���������� ����
        // ������ �����Ϳ��� �ҷ� ���ڷ� �ٲ� ���� ������ �׷� Ǯ�Ŵ��������� �ٲ����
        for (int index=0; index < GameManager.instance.pool.prefabs.Length; index++){
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0://�����̻�
                speed = 1f;
                increasedRangeCritWF = 2f * (0.3f + (GameManager.instance.Range / 100f));
                break;
            
            case 1://�Ƶ�
                speed = 2f;
                //Adol();
                break;
            case 2://����
                speed = 1f;
                //FullMoon();
                break;

            case 3://ȸ�����ٶ�
                speed = 250;
                
                Cyclone();
                break;
            case 4: //��Ÿ
                speed = 2f;
                break;
            case 5://���భŸ
                speed = 2f;
                break;
            case 6: //�ٹ� ���
                speed = 1f;
                break;
            case 7: //���� ���
                speed = 1.5f;
                break;
            case 8: //���� ���
                speed = 0.5f;
                break;
            case 9://ȸ�� Į��
                speed = 150;
                
                TwistingBlade();
                break;
            case 10://ȸ�� Į��
                speed = 1f;

                break;
            case 11://���Ÿ� ���� ����
                speed = 0.6f;
                break;

        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver); // applygear �Լ��� ������ �ִ� �͵��� ��� ����.
        //������ �ö� ���¿��� ���Ⱑ �߰� ������ �� ���Ⱑ ���� ������ �ȹޱ� ������ �ϴ� ��.
    }

    void Cyclone()//���� ��ġ �Լ�.
    {
        
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            
            //�θ� weapon �ڽ� muin
            if (index < transform.childCount) //�ڽ��� �������� ������� (������ �ִ°� ���� ���) 
            {
                bullet = transform.GetChild(index);//�ڽſ� �ִ� ���ϵ带 ����. Ǯ�Ŵ������� �Ⱦ���.
                
            }
            else//�ε����� ���� ���
            {
                bullet = GameManager.instance.pool.Get(8).transform;//Transform �� ������ �θ�� �����ؼ�
                    bullet.parent = transform; // ���� if ���� �θ� �̹� �� �ڽ��̶� ������� �̰Ÿ����ϴϱ�.
               
            }

            bullet.localPosition = Vector3.zero; // 000 ��ġ�� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; //ȸ������ ���ʹϾ��ε� ���ʹϾ��� �ʱⰪ�� ���̵�ƼƼ�̴�
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 2f, Space.World);//���忡�� �ڱ� �ڽ� ��ġ���� �Ʒ��� 1 �̵�


            CriticalCal(GameManager.instance.criticalChance);
            if (isCritical == true)
            {
                bullet.GetComponent<Bullet>().Init(criticalDamage, -1, Vector3.zero);//�������� ����Ƚ��, �������� init ȣ��
            }
            else
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//�������� ����Ƚ��, �������� init ȣ��



            //bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1�� �������� �����Ѵٴ� �ǹ�

            



            //bullet.Translate(bullet.up*1.5f,Space.World) ;//���忡�� �ڱ� �ڽ� ��ġ���� �Ʒ��� 1 �̵�


            //bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1�� �������� �����Ѵٴ� �ǹ�

        }
    }

    


    IEnumerator FadeOutAndDestroy(Transform bullet)
    {
        // ���Ⱑ �߻�� �� 1�� ���� ������ �����ϰ� ����ϴ�.
        float fadeDuration = 0.1f;
        float timer = 0.0f;
        
        SpriteRenderer renderer = bullet.GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            while (timer < fadeDuration)
            {


                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if(id == 0) //
        {
            while (timer < fadeDuration)
            {
                bullet.position = transform.position;
                bullet.Translate(bullet.up * 1.75f, Space.World);

                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if (id == 1)
        {
            while (timer < fadeDuration)
            {
                bullet.position = transform.position;
                bullet.Translate(bullet.up * 1f, Space.World);

                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if(id == 2)
        {
            fadeDuration = 0.5f;
            while (timer < fadeDuration)
            {
                bullet.position = transform.position;
                bullet.Translate(bullet.up * -0.3f, Space.World);

                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if(id==5 )//���భŸ
        {
            while (timer < fadeDuration)
            {
                bullet.position = transform.position;
                

                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            
            while (timer < fadeDuration)
            {
                bullet.position = transform.position;
                bullet.Translate(bullet.up * 2f, Space.World);

                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                timer += Time.deltaTime;
                yield return null;
            }
        }

        // ���⸦ �����մϴ�.
        bullet.gameObject.SetActive(false);
        
    }

    void Wolves_Fang()//�����̻�
    {
        int rand= Random.Range(0, 2);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(3).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        if (rand % 2 == 1) //Į�� ����
        {
            bullet.localScale = new Vector3(-bullet.localScale.x, bullet.localScale.y, bullet.localScale.z);
        }
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��
        
        CriticalCal(GameManager.instance.criticalChance);
         if (isCritical == true)
        {
            Wolves_Fang_Critical();
        }
        
        bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        
        //if(isCritical ==true){ �ٸ� �Ҹ� else)

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
        
    }
    void Wolves_Fang_Critical()//�����̻� ũ��Ƽ�ý�
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(4).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        
        
        bullet.localScale = new Vector3(increasedRangeCritWF, increasedRangeCritWF, bullet.localScale.z);
        
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��
        bullet.Translate(bullet.up * (2f * (1f + increasedRangeCritWF)), Space.World);
       
        

        bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��

       
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));

    }
    /* ���� �ٴ� Į��
    void Wolves_Fang_Sword() // Į���ϴ� ���
    {


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(4).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        bullet.Translate(bullet.up * -1f, Space.World);

       


        StartCoroutine(FadeOutAndDestroy(bullet));

    }
    */
    void CriticalCal(int criticalChance)
    {
        float randomValue = Random.Range(0, 100);
        if (randomValue < criticalChance)
        {
            isCritical = true;
             // ũ��Ƽ�� ������ ���
        }
        else
        {
            isCritical = false;
            
        }
    }

    void Adol()
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(5).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��
        //damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//ġ��Ÿ Ȯ��

        adolCount++;
        if(adolCount == 3)
        {
            adolCount = 0;
            Adol3();
        }

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        


        //if(isCritical ==true){ �ٸ� �Ҹ� else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void Adol3() // �Ƶ� 3Ÿ
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(6).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//ġ��Ÿ
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count+1, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count+1, direction);//�������� ����Ƚ��, �������� init ȣ��

        
        StartCoroutine(ProjectileRemove(bullet));
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
    }

    IEnumerator ProjectileRemove(Transform bullet)
    {
        
        

        // ���Ⱑ �߻�� �� 1�� ���� ������ �����ϰ� ����ϴ�.
        float fadeDuration = 2f;
        float timer = 0.0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                yield return null;
                
            }


        bullet.gameObject.SetActive(false);
    }

    void FullMoon()
    {
        
        Vector3 direction = Vector3.zero;



        Transform bullet = GameManager.instance.pool.Get(7).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��


        

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
       
        //if(isCritical ==true){ �ٸ� �Ҹ� else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void Smite()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(9).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��
        

        

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��



        //if(isCritical ==true){ �ٸ� �Ҹ� else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void LeapSlam()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;

        StartCoroutine(MovePlayerToTarget(mousePosition));
        Transform bullet = GameManager.instance.pool.Get(10).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��


        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��

        //if(isCritical ==true){ �ٸ� �Ҹ� else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    IEnumerator MovePlayerToTarget(Vector3 targetPosition)
    {
        /*
       GetComponent<Rigidbody2D>().AddForce(targetPosition * 10f, ForceMode2D.Impulse);
       yield return new WaitForSeconds(1f);
       */
        float distance = Vector3.Distance(player.transform.position, targetPosition);
        if (distance > 5)
            distance = 5;

        
        while (distance > 0.1f)
        {
            
            // �÷��̾ ���콺 Ŀ�� ��ġ�� �̵���ŵ�ϴ�.
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, player.moveSpeed * 2 * Time.deltaTime);
            distance = Vector3.Distance(player.transform.position, targetPosition);
            yield return null;
        }
    }

    void MultipleShot(float angle) // �ٹ� ���
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction1= (mousePosition - transform.position).normalized;

        // ���� ���͸� 3����ŭ ȸ����ŵ�ϴ�.
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 direction = rotation * direction1;
        direction = direction.normalized; 


         Transform bullet = GameManager.instance.pool.Get(2).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//ġ��Ÿ
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��

        
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
    }

    void PiercingShot() // �ٹ� ���
    {


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(11).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��




        

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//ġ��Ÿ
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��


        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
    }
    void SnapShot()//���� ���
    {
        if (!player.scanner.nearsetTarget)//Ÿ���� ���ٸ�
            return;

        Vector3 targetPos = player.scanner.randomTarget.position;//Ÿ�� ��ġ�� ����
        Vector3 dir = targetPos - transform.position;//����
        dir = dir.normalized;//dir�� ũ����� ���� �־ ��ֶ������

        Transform bullet = GameManager.instance.pool.Get(2).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//��ǥ �������� ȸ��������.up�� ��

        bullet.GetComponent<Bullet>().Init(damage, count, dir);//�������� ����Ƚ��, �������� init ȣ��

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
    }

    void TwistingBlade()//���� ��ġ �Լ�.
    {

        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            //�θ� weapon �ڽ� muin
            if (index < transform.childCount) //�ڽ��� �������� ������� (������ �ִ°� ���� ���) 
            {
                bullet = transform.GetChild(index);//�ڽſ� �ִ� ���ϵ带 ����. Ǯ�Ŵ������� �Ⱦ���.

            }
            else//�ε����� ���� ���
            {
                bullet = GameManager.instance.pool.Get(12).transform;//Transform �� ������ �θ�� �����ؼ�
                bullet.parent = transform; // ���� if ���� �θ� �̹� �� �ڽ��̶� ������� �̰Ÿ����ϴϱ�.

            }

            bullet.localPosition = Vector3.zero; // 000 ��ġ�� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; //ȸ������ ���ʹϾ��ε� ���ʹϾ��� �ʱⰪ�� ���̵�ƼƼ�̴�
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            

            bullet.Translate(bullet.up*1.75f, Space.World);//���忡�� �ڱ� �ڽ� ��ġ���� �Ʒ��� 1 �̵�


            CriticalCal(GameManager.instance.criticalChance);
            if (isCritical == true)
            {
                bullet.GetComponent<Bullet>().Init(criticalDamage, -1, Vector3.zero);//�������� ����Ƚ��, �������� init ȣ��
            }
            else
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//�������� ����Ƚ��, �������� init ȣ��



            //bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1�� �������� �����Ѵٴ� �ǹ�





            //bullet.Translate(bullet.up*1.5f,Space.World) ;//���忡�� �ڱ� �ڽ� ��ġ���� �Ʒ��� 1 �̵�


            //bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1�� �������� �����Ѵٴ� �ǹ�

        }
    }

    void ThrowingKnife() // �ٹ� ���
    {



        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; // z���� 0���� �����Ͽ� 2D ��� ���� ��ǥ�� ����

        // �÷��̾ �ٶ󺸴� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(12).transform;//������ ������
        bullet.position = transform.position;//�߻�ü�� �÷��̾��� ��ġ�������� �߻��

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//��ǥ �������� ȸ��������.up�� ��






        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//ġ��Ÿ
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��


        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
    }

    public void SmiteDead(Transform enemy)
    {
        Vector3 direction = Vector3.zero;



        Transform bullet = GameManager.instance.pool.Get(13).transform;//������ ������
        bullet.position = enemy.position;//�߻�ü�� ���� enemy ��ġ�������� �߻��




        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//�������� ����Ƚ��, �������� init ȣ��
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//�������� ����Ƚ��, �������� init ȣ��

        //if(isCritical ==true){ �ٸ� �Ҹ� else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
        StartCoroutine(FadeOutAndDestroy(bullet));
    }
}
