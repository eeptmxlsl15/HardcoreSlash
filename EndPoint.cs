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
            player.transform.position = Bunker.position;//��Ŀ�� �̵���Ŵ
            //GameManager.instance.MapEnd();//�� ������ �ٶ� �������
            Map.SetActive(false);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//���ʹ� �±� ���� ��Ȱ��ȭ
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
        }

    }

}
