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
    public bool isLive; // �ð��� �帣�� �ִ���? 
    public float gameTime;// ������ �帣�� ���� Ÿ��
    public float maxGameTime = 5 * 10f;
    public int playerId;//ĳ���� ���̵�

    [Header("# Player Info")]
    public float health;
    public float maxHealth=100;
    public int level;
    public int kill;
    public int exp;
    public int criticalChance=0;
    public int criticalMultiple=2;
    public int Range=1;//���� ����
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    public List<int> MuinSkills = new List<int> { 0, 1, 2 };
    public List<int> WarrorSkills = new List<int> { 3,4,5 };
    public List<int> ArcherSkills = new List<int> { 6,7,8 };
    public List<int> AssassinSkills = new List<int> { 9,10};
    public List<int> AllSkills = new List<int> { 12,13,14,15 };



    void Awake()
    {
        instance = this;//�ڱ��ڽ��� ����
        Application.targetFrameRate = 60;//60������
    }



    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;//���� �� ü�� ����ȭ
        

        uiLevelUp.Show();
        player.gameObject.SetActive(true);//ĳ���� Ȱ��ȭ
        //ĳ���� ������ �����ϰ� �ϴ� ���� ¥��
        /*
        if(id == 0)
            uiLevelUp.Select(7);//�����۱׷쿡�� ����
        else
            uiLevelUp.Select(playerId % 2   );//�÷��̾�� ù��° ���� ����� // ���� ���� ������ 2���� �ϴ� ����
        */
       // Resume();
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);//�Ҹ�

    }


    public void GameOver()
    {

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()// ����ҋ� �����̸� �ֱ� ���� �ڷ�ƾ
    {
        isLive = false;//�ð�  ����
        yield return new WaitForSeconds(0.5f); // 0.5�� ���� �÷��̾ �״� ���
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();//Ÿ�� �������� 0���� ����� �Լ�

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);//�Ҹ�
    }


    public void GameVictory() // ���̰� ���� ���� ���� ���̴� �Լ�
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()// ����ҋ� �����̸� �ֱ� ���� �ڷ�ƾ
    {
        isLive = false;//�ð�  ����
        enemyCleaner.SetActive(true);//��� ���� �̶� ����ġ ��°� �����ؾ���
        yield return new WaitForSeconds(0.5f); // 0.5�� ���� ���Ͱ� �״� ���
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);//�Ҹ�
    }




    public void GameRetry()//���� ���� ���� �ٽ� ������ ��
    {
        SceneManager.LoadScene(0);//���� - ���� ���ÿ� �ε����� �̸����� ���� �ҷ��´�
    }


    public void GameQuit()//���� ���� ���� �ٽ� ������ ��
    {
        Application.Quit(); ;//���� - ���� ���ÿ� �ε����� �̸����� ���� �ҷ��´�
    }


    void Update()
    {
        if (!isLive)//�ð��� ����ٸ� ������ �Ȱ��� �ð��� �帣�� ���� ��� ������Ʈ���� �ؾ���
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
            return;//�̰ܼ� ������ �� ���� �� ����ġ ��� ���� ����
        exp++;
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
            //���� ������ �ִ뷹��-1 ������ ������ �����µ� 
            //���� ������ �ִ뷹�� ���� ������� �ִ� ����-1�� ����ġ�� ������
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop() //���� ����
    {
        isLive = false;
        Time.timeScale = 0;// �ð��� �帣�� �ӵ��� ũ��. �⺻ 1. 0 �� ����
        
    }

    public void Resume()//�����簳
    {
        isLive = true;
        Time.timeScale = 1;
        
    }

}
