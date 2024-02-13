using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    [Header("# Game Control")]
    public bool isLive; // 시간이 흐르고 있는지? 
    public float gameTime;// 실제로 흐르는 게임 타임
    public float maxGameTime = 5 * 10f;
    public int playerId;//캐릭터 아이디

    [Header("# Player Info")]
    public float health;
    public float maxHealth=100;
    public int level;
    public int kill;
    public int exp;
    public int criticalChance=0;
    public int criticalMultiple=2;
    public int Range=1;//공격 범위
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    public List<int> MuinSkills = new List<int> { 0, 1, 2 };
    public List<int> WarrorSkills = new List<int> { 3,4,5 };
    public List<int> ArcherSkills = new List<int> { 6,7,8 };
    public List<int> AssassinSkills = new List<int> { 9,10};
    public List<int> AllSkills = new List<int> { 12,13,14,15 };



    void Awake()
    {
        instance = this;//자기자신을 넣음
        Application.targetFrameRate = 60;//60프레임
    }



    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;//시작 시 체력 동기화
        

        uiLevelUp.Show();
        player.gameObject.SetActive(true);//캐릭터 활성화
        //캐릭터 아이템 선택하게 하는 로직 짜기
        /*
        if(id == 0)
            uiLevelUp.Select(7);//아이템그룹에서 나옴
        else
            uiLevelUp.Select(playerId % 2   );//플레이어에게 첫번째 무기 쥐어줌 // 아직 무기 개수가 2개라 일단 나눔
        */
       // Resume();
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);//소리

    }


    public void GameOver()
    {

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()// 사망할떄 딜레이를 주기 위한 코루틴
    {
        isLive = false;//시간  정지
        yield return new WaitForSeconds(0.5f); // 0.5초 동안 플레이어가 죽는 모션
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();//타임 스케일을 0으로 만드는 함수

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);//소리
    }


    public void GameVictory() // 이이고 나서 몹들 전부 죽이는 함수
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()// 사망할떄 딜레이를 주기 위한 코루틴
    {
        isLive = false;//시간  정지
        enemyCleaner.SetActive(true);//모두 죽임 이때 경험치 얻는걸 방지해야함
        yield return new WaitForSeconds(0.5f); // 0.5초 동안 몬스터가 죽는 모션
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);//소리
    }




    public void GameRetry()//게임 오버 이후 다시 시작할 때
    {
        SceneManager.LoadScene(0);//파일 - 빌드 세팅에 인덱스나 이름으로 씬을 불러온다
    }


    public void GameQuit()//게임 오버 이후 다시 시작할 때
    {
        Application.Quit(); ;//파일 - 빌드 세팅에 인덱스나 이름으로 씬을 불러온다
    }


    void Update()
    {
        if (!isLive)//시간이 멈췄다면 밑으로 안가서 시간이 흐르지 않음 모든 업데이트에서 해야함
            return;

        gameTime += Time.deltaTime;
        
        //
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }

    }


    public void GetExp()
    {
        if (!isLive)
            return;//이겨서 몹들이 다 죽을 때 경험치 얻는 것을 방지
        exp++;
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
            //실제 레벨과 최대레벨-1 사이중 작은게 나오는데 
            //실제 레벨이 최대레벨 보다 높은경우 최대 레벨-1의 경험치로 고정됨
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop() //게임 멈춤
    {
        isLive = false;
        Time.timeScale = 0;// 시간이 흐르는 속도의 크기. 기본 1. 0 은 멈춤
        
    }

    public void Resume()//게임재개
    {
        isLive = true;
        Time.timeScale = 1;
        
    }

}
