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
    public float Density;
    public float levelTime;

    public int level; // �ð��� ���� ���̵�@@


    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        
    }

    private void OnEnable()
    {
        for (int i = 0; i < Density; i++)
        {
            Spawn();
        }
    }

    void Update()
    {
        if (!GameManager.instance.isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ����
            return;
        
        //timer += Time.deltaTime;
       // level = Mathf.FloorToInt(GameManager.instance.gameTime / 5f); //�ݹ������� ��Ʈ������ �������
        
        



        
    }

    void Spawn()//���� ��ȯ �Լ�
    {
        float spawnRadius = 15f; // ��ȯ �ݰ� ����

        Vector3 RandomPosition = Random.insideUnitCircle * spawnRadius;

        GameObject enemy = GameManager.instance.pool.Get(0);//���� ��ȯ

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position + RandomPosition;
        //���� �������� 1���� ������ ������ � ������Ʈ�� transform�� ������ �ֱ⶧���� awake���� �ڱ��ڽ��� ���Ե�.
        //���� 1���� �ϴ� ��. �ڽ� ������Ʈ������ ���õǵ���
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
       
    }
}

[System.Serializable]//����ȭ�ؼ� �ν����Ϳ� ���� �� �ְ� ����
public class SpawnData
{
    

    public int spriteType;//���� ��������Ʈ
    public int health;
    public float speed;


}