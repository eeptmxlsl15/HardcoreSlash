using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 몬스터 소환 
/// </summary>
public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    public int level; // 시간에 따른 난이도@@


    float timer;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음
            return;

        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 5f); //반버림으로 인트형으로 만들어줌
        //시간에 따른 난이도@@
        if (level > 4)
        {
            level = 4;
        }
        



        if(timer >(level == 0 ? 0.5f : 0.2f))//시간에 따라서 레벨이 증가하면 0.2초마다
        {
            Spawn();
            timer = 0;
        }
        
    }

    void Spawn()//몬스터 소환 함수
    {
        GameObject enemy = GameManager.instance.pool.Get(0);//몬스터 소환

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //랜덤 레인지가 1부터 시작인 이유는 어떤 오브젝트든 transform은 가지고 있기때문에 awake에서 자기자신이 포함됨.
        //따라서 1부터 하는 것. 자식 오브젝트에서만 선택되도록
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
       
    }
}

[System.Serializable]//직렬화해서 인스펙터에 보일 수 있게 해줌
public class SpawnData
{
    public float spawnTime;//몬스터 소환 간격

    public int spriteType;//몬스터 스프라이트
    public int health;
    public float speed;


}