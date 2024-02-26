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
                bullet.GetComponent<Bullet>().Init(1, -2, direction);//�������� ����Ƚ��, �������� init ȣ��
            }
            else
                bullet.GetComponent<Bullet>().Init(1, -2, direction);//�������� ����Ƚ��, �������� init ȣ��

            //if(isCritical ==true){ �ٸ� �Ҹ� else)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);//�Ҹ�
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
            // ũ��Ƽ�� ������ ���
        }
        else
        {
            isCritical = false;

        }
    }


    IEnumerator FadeOutAndDestroy(Transform bullet)
    {
        // ���Ⱑ �߻�� �� 1�� ���� ������ �����ϰ� ����ϴ�.
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

    // ���⸦ �����մϴ�.

    

