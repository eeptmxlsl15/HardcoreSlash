using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmiteDead : MonoBehaviour
{
    Player player;
    //public Weapon weapon;
    public Enemy enemy;
    public bool isCritical = false;
    int flag = 1;
    void Awake()
    {

        player = GameManager.instance.player;
        enemy = GetComponent<Enemy>();
       

    }
    void Update()
    {
        if (player.transform.Find("Weapon 4") && enemy.isLive == false && flag == 1)
        {
            flag = 0;
            Debug.Log("adad");
            Vector3 direction = Vector3.zero;

            Transform bullet = GameManager.instance.pool.Get(13).transform;
            bullet.position = transform.position;

            //CriticalCal(GameManager.instance.criticalChance);
            if (isCritical == true)
            {
                bullet.GetComponent<Bullet>().Init(1, -2, direction);//데미지와 관통횟구, 방향으로 init 호출
            }
            else
                bullet.GetComponent<Bullet>().Init(1, -2, direction);//데미지와 관통횟구, 방향으로 init 호출

            //if(isCritical ==true){ 다른 소리 else)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//소리
            StartCoroutine(FadeOutAndDestroy(bullet));
            
        }
    }

    void CriticalCal(int criticalChance)
    {
        float randomValue = Random.Range(0, 100);
        if (randomValue < criticalChance)
        {
            isCritical = true;
            //criticalDamage = weapon.damage * GameManager.instance.criticalMultiple;
            // 크리티컬 데미지 계산
        }
        else
        {
            isCritical = false;

        }
    }


    IEnumerator FadeOutAndDestroy(Transform bullet)
    {
        // 무기가 발사된 후 1초 동안 서서히 투명하게 만듭니다.
        float fadeDuration;
        float timer = 0.0f;

        SpriteRenderer renderer = bullet.GetComponent<SpriteRenderer>();


        fadeDuration = 0.1f;
        if (renderer == null)
        {
            while (timer < fadeDuration)
            {


                timer += Time.deltaTime;
                yield return null;
            }
        } else{
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
        bullet.gameObject.SetActive(false);
    }
}

    // 무기를 제거합니다.

    

