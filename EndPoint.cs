using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public Transform Bunker;
    public Player player;
    public GameObject Map;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = Bunker.position;//벙커로 이동시킴
            //GameManager.instance.MapEnd();//맵 보상을 줄때 사용하자
            Map.SetActive(false);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//에너미 태그 전부 비활성화
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
        }

    }

}
