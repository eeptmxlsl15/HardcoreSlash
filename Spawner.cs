using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ��ȯ 
/// </summary>
public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    public int level; // �ð��� ���� ���̵�@@


    float timer;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;

        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 5f); //�ݹ������� ��Ʈ������ �������
        //�ð��� ���� ���̵�@@
        if (level > 4)
        {
            level = 4;
        }
        



        if(timer >(level == 0 ? 0.5f : 0.2f))//�ð��� ���� ������ �����ϸ� 0.2�ʸ���
        {
            Spawn();
            timer = 0;
        }
        
    }

    void Spawn()//���� ��ȯ �Լ�
    {
        GameObject enemy = GameManager.instance.pool.Get(0);//���� ��ȯ

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //���� �������� 1���� ������ ������ � ������Ʈ�� transform�� ������ �ֱ⶧���� awake���� �ڱ��ڽ��� ���Ե�.
        //���� 1���� �ϴ� ��. �ڽ� ������Ʈ������ ���õǵ���
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
       
    }
}

[System.Serializable]//����ȭ�ؼ� �ν����Ϳ� ���� �� �ְ� ����
public class SpawnData
{
    public float spawnTime;//���� ��ȯ ����

    public int spriteType;//���� ��������Ʈ
    public int health;
    public float speed;


}