using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 풀매니저에서 받아온 무기들을 관리하는 스크립트
/// </summary>
public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;//풀 매니져 오브젝트에 prefabs의 순서이다.만약 두번쨰라면 1
    public float damage;
    public float criticalDamage;
    public int projectile=2;
    
    public int count; // 무기 개수
    public float speed;//한대 때리는데 걸리는 시간
    public float cooltime;
    public bool isCritical = false;
    public int adolCount=0;
    public float increasedRangeCritWF; // 공격 범위를 설정
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
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        switch (id)
        {   case 0://늑대이빨
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Wolves_Fang();
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;
            case 1://아돌
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Adol();
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;
            case 2://만월베기

                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    FullMoon();
                    
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;

            case 3://회오리바람

                
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);//업데이트에서는 꼭 델타타임을 곱해야 한다. 프레임이 소비한 시간.
                Cyclone();
                break;

            case 4: // 강타
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();

                    Smite();
                    
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;
            case 5: // 도약강타
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;
                    //Fire();
                    //if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
                        //return;
                    LeapSlam();
                    
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;
            case 6:  // 다발사격
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;

                    for(int index=0;index < projectile; index++)
                    {if (index == 0)
                            MultipleShot(0);
                        MultipleShot(index*3f);
                        MultipleShot(index *(-3f));

                    }
                    

                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;

            case 7:  // 관통사격
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;

                    PiercingShot();


                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;

            case 8:  // 사격
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;

                    SnapShot();


                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;
            case 9://회전 칼날


                transform.Rotate(Vector3.back * speed * Time.deltaTime);//업데이트에서는 꼭 델타타임을 곱해야 한다. 프레임이 소비한 시간.
                TwistingBlade();
                break;

            case 10://단검 투척
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
                if (timer > speed)
                {
                    timer = 0f;

                    


                    ThrowingKnife();
                    //Wolves_Fang_Sword(); // 칼 모션은 나중에 하자
                }
                break;

                break;
            case 11://총쏘기 나중에 도적 칼날 회수
                timer += Time.deltaTime;//기술 사이의 시간에 대한 로직
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
        if (id == 11) //도적 단검일때만
            Batch();
        */
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

    }
    public void Init(ItemData data)//무기 정보 초기화 함수
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
        // 프리팹으로 데이터를 추가해서 프리펩에 데이터가 쌓여였다. 여기에 여러 스킬들을 넣도록 하자
        // 스크립트블 오브젝트의 독립성을 위해서 인덱스 아니 프리펩으로 설정
        // 프리텝 데이터에서 불렛 숫자로 바꿀 수도 있지만 그럼 풀매니져에서도 바꿔야함
        for (int index=0; index < GameManager.instance.pool.prefabs.Length; index++){
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0://늑대이빨
                speed = 1f;
                increasedRangeCritWF = 2f * (0.3f + (GameManager.instance.Range / 100f));
                break;
            
            case 1://아돌
                speed = 2f;
                //Adol();
                break;
            case 2://만월
                speed = 1f;
                //FullMoon();
                break;

            case 3://회오리바람
                speed = 250;
                
                Cyclone();
                break;
            case 4: //강타
                speed = 2f;
                break;
            case 5://도약강타
                speed = 2f;
                break;
            case 6: //다발 사격
                speed = 1f;
                break;
            case 7: //관통 사격
                speed = 1.5f;
                break;
            case 8: //연속 사격
                speed = 0.5f;
                break;
            case 9://회전 칼날
                speed = 150;
                
                TwistingBlade();
                break;
            case 10://회전 칼날
                speed = 1f;

                break;
            case 11://원거리 무기 공속
                speed = 0.6f;
                break;

        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver); // applygear 함수를 가지고 있는 것들은 모두 시행.
        //공속이 올라간 상태에서 무기가 추가 됐을때 그 무기가 공속 영향을 안받기 때문에 하는 것.
    }

    void Cyclone()//무기 배치 함수.
    {
        
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            
            //부모 weapon 자식 muin
            if (index < transform.childCount) //자식의 개수보다 적을경우 (가지고 있는게 있을 경우) 
            {
                bullet = transform.GetChild(index);//자신에 있는 차일드를 쓴다. 풀매니져에서 안쓴다.
                
            }
            else//인덱스가 넘을 경우
            {
                bullet = GameManager.instance.pool.Get(8).transform;//Transform 인 이유는 부모로 가야해서
                    bullet.parent = transform; // 위쪽 if 문은 부모가 이미 내 자신이라 상관없음 이거먼저하니까.
               
            }

            bullet.localPosition = Vector3.zero; // 000 위치로 초기화
            bullet.localRotation = Quaternion.identity; //회전값은 쿼터니언인데 쿼터니언의 초기값은 아이덴티티이다
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 2f, Space.World);//월드에서 자기 자신 위치에서 아래로 1 이동


            CriticalCal(GameManager.instance.criticalChance);
            if (isCritical == true)
            {
                bullet.GetComponent<Bullet>().Init(criticalDamage, -1, Vector3.zero);//데미지와 관통횟구, 방향으로 init 호출
            }
            else
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//데미지와 관통횟구, 방향으로 init 호출



            //bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1은 무한으로 관통한다는 의미

            



            //bullet.Translate(bullet.up*1.5f,Space.World) ;//월드에서 자기 자신 위치에서 아래로 1 이동


            //bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1은 무한으로 관통한다는 의미

        }
    }

    


    IEnumerator FadeOutAndDestroy(Transform bullet)
    {
        // 무기가 발사된 후 1초 동안 서서히 투명하게 만듭니다.
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
        else if(id==5 )//도약강타
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

        // 무기를 제거합니다.
        bullet.gameObject.SetActive(false);
        
    }

    void Wolves_Fang()//늑대이빨
    {
        int rand= Random.Range(0, 2);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(3).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        if (rand % 2 == 1) //칼질 방향
        {
            bullet.localScale = new Vector3(-bullet.localScale.x, bullet.localScale.y, bullet.localScale.z);
        }
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축
        
        CriticalCal(GameManager.instance.criticalChance);
         if (isCritical == true)
        {
            Wolves_Fang_Critical();
        }
        
        bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        
        //if(isCritical ==true){ 다른 소리 else)

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));
        
    }
    void Wolves_Fang_Critical()//늑대이빨 크리티컬시
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(4).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        
        
        bullet.localScale = new Vector3(increasedRangeCritWF, increasedRangeCritWF, bullet.localScale.z);
        
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축
        bullet.Translate(bullet.up * (2f * (1f + increasedRangeCritWF)), Space.World);
       
        

        bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출

       
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));

    }
    /* 옆에 붙는 칼질
    void Wolves_Fang_Sword() // 칼질하는 모션
    {


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(4).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨
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
             // 크리티컬 데미지 계산
        }
        else
        {
            isCritical = false;
            
        }
    }

    void Adol()
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(5).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축
        //damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//치명타 확인

        adolCount++;
        if(adolCount == 3)
        {
            adolCount = 0;
            Adol3();
        }

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        


        //if(isCritical ==true){ 다른 소리 else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void Adol3() // 아돌 3타
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(6).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//치명타
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count+1, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count+1, direction);//데미지와 관통횟구, 방향으로 init 호출

        
        StartCoroutine(ProjectileRemove(bullet));
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
    }

    IEnumerator ProjectileRemove(Transform bullet)
    {
        
        

        // 무기가 발사된 후 1초 동안 서서히 투명하게 만듭니다.
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



        Transform bullet = GameManager.instance.pool.Get(7).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨


        

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
       
        //if(isCritical ==true){ 다른 소리 else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void Smite()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(9).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축
        

        

        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출



        //if(isCritical ==true){ 다른 소리 else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));
    }

    void LeapSlam()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;

        StartCoroutine(MovePlayerToTarget(mousePosition));
        Transform bullet = GameManager.instance.pool.Get(10).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨


        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출

        //if(isCritical ==true){ 다른 소리 else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
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
            
            // 플레이어를 마우스 커서 위치로 이동시킵니다.
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, player.moveSpeed * 2 * Time.deltaTime);
            distance = Vector3.Distance(player.transform.position, targetPosition);
            yield return null;
        }
    }

    void MultipleShot(float angle) // 다발 사격
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction1= (mousePosition - transform.position).normalized;

        // 방향 벡터를 3도만큼 회전시킵니다.
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 direction = rotation * direction1;
        direction = direction.normalized; 


         Transform bullet = GameManager.instance.pool.Get(2).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//치명타
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출

        
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
    }

    void PiercingShot() // 다발 사격
    {


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(11).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축




        

        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//치명타
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출


        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
    }
    void SnapShot()//연속 사격
    {
        if (!player.scanner.nearsetTarget)//타겟이 없다면
            return;

        Vector3 targetPos = player.scanner.randomTarget.position;//타겟 위치는 랜덤
        Vector3 dir = targetPos - transform.position;//방향
        dir = dir.normalized;//dir에 크기까지 같이 있어서 노멀라이즈드

        Transform bullet = GameManager.instance.pool.Get(2).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//목표 방향으로 회전시켜줌.up은 축

        bullet.GetComponent<Bullet>().Init(damage, count, dir);//데미지와 관통횟구, 방향으로 init 호출

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
    }

    void TwistingBlade()//무기 배치 함수.
    {

        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            //부모 weapon 자식 muin
            if (index < transform.childCount) //자식의 개수보다 적을경우 (가지고 있는게 있을 경우) 
            {
                bullet = transform.GetChild(index);//자신에 있는 차일드를 쓴다. 풀매니져에서 안쓴다.

            }
            else//인덱스가 넘을 경우
            {
                bullet = GameManager.instance.pool.Get(12).transform;//Transform 인 이유는 부모로 가야해서
                bullet.parent = transform; // 위쪽 if 문은 부모가 이미 내 자신이라 상관없음 이거먼저하니까.

            }

            bullet.localPosition = Vector3.zero; // 000 위치로 초기화
            bullet.localRotation = Quaternion.identity; //회전값은 쿼터니언인데 쿼터니언의 초기값은 아이덴티티이다
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            

            bullet.Translate(bullet.up*1.75f, Space.World);//월드에서 자기 자신 위치에서 아래로 1 이동


            CriticalCal(GameManager.instance.criticalChance);
            if (isCritical == true)
            {
                bullet.GetComponent<Bullet>().Init(criticalDamage, -1, Vector3.zero);//데미지와 관통횟구, 방향으로 init 호출
            }
            else
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//데미지와 관통횟구, 방향으로 init 호출



            //bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1은 무한으로 관통한다는 의미





            //bullet.Translate(bullet.up*1.5f,Space.World) ;//월드에서 자기 자신 위치에서 아래로 1 이동


            //bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1은 무한으로 관통한다는 의미

        }
    }

    void ThrowingKnife() // 다발 사격
    {



        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; // z축을 0으로 설정하여 2D 평면 상의 좌표로 고정

        // 플레이어를 바라보는 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;



        Transform bullet = GameManager.instance.pool.Get(12).transform;//프리펩 가져옴
        bullet.position = transform.position;//발사체는 플레이어의 위치에서부터 발사됨

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);//목표 방향으로 회전시켜줌.up은 축






        // damage = CriticalCal(damage, GameManager.instance.criticalChance, GameManager.instance.criticalMultiple);//치명타
        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출


        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
    }

    public void SmiteDead(Transform enemy)
    {
        Vector3 direction = Vector3.zero;



        Transform bullet = GameManager.instance.pool.Get(13).transform;//프리펩 가져옴
        bullet.position = enemy.position;//발사체는 죽은 enemy 위치에서부터 발사됨




        CriticalCal(GameManager.instance.criticalChance);
        if (isCritical == true)
        {
            bullet.GetComponent<Bullet>().Init(criticalDamage, count, direction);//데미지와 관통횟구, 방향으로 init 호출
        }
        else
            bullet.GetComponent<Bullet>().Init(damage, count, direction);//데미지와 관통횟구, 방향으로 init 호출

        //if(isCritical ==true){ 다른 소리 else)
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
        StartCoroutine(FadeOutAndDestroy(bullet));
    }
}
