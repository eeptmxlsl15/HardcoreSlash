using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // ��𿡼��� �� �� �ְ� ���� �޸𸮿� ����

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;//���� �ٹ������� ���� ���带 ���� ����
    AudioSource[] sfxPlayers;
    int channelIndex;//���� ������� ä�� �ε���

    public enum Sfx //�������� 3���� �����ϸ� ������ ������ lose�� 4�� �ȴ�. ������ ����� ���� ���� -1 �̸� ����

    {
        Dead , Hit,LevelUp=3,Lose, Melee , Range=7 , Select, Win
    }

    void Awake()
    {
        instance = this;// ������ �ν��Ͻ�ȭ
        Init();

    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");//�ڵ�ȿ��� ������Ʈ ����� ����. ����ǥ ���� ������Ʈ �̸�
        bgmObject.transform.parent = transform;// ���ٿ��� ���� �÷��̾ ����� �Ŵ��� ������Ʈ�� �ڽ����� ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;//���ӽ��۽� �ٷ� ������� �ȳ�������. ĳ�� ���� �� ������ �ϱ� ����.
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();//����� �����н� ������Ʈ�� ���� ī�޶� ������Ʈ�� �ִµ� Camera.main ���� �ڵ� ����


        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");//�ڵ�ȿ��� ������Ʈ ����� ����. ����ǥ ���� ������Ʈ �̸�
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];//ä�� ������ŭ ����� �ҽ��� ���� ����
        for (int index=0;index< sfxPlayers.Length; index++)//�������� ä�ο� �������� Ŭ�� ���� �ֱ�
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;//����������͸� �ϴ°� �н���. 
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay) //������� �÷��� ���� // ���� ���� , �� ��ƾ
    {
        if (isPlay)
        {
            bgmPlayer.Play();

        }
        else
        {
            bgmPlayer.Stop();
        }

    }

    public void EffectBgm(bool isPlay) // ����� ���� // ������ ����
    {
        bgmEffect.enabled = isPlay;

    }



    public void PlaySfx(Sfx sfx)
    {
        for (int index =0; index  < sfxPlayers.Length;index++)
        {
            //ä�� �ε����� �������� �÷��̵� Ŭ���̴�
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;//�Ѿ���ʰ��ϱ����� ��ⷯ ���

            //���� �Ҹ��� ��ø�Ǵ� ��� �������� ���߿� �ϳ� ����
            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee) 
                ranIndex = Random.Range(0, 2);

            if (sfxPlayers[loopIndex].isPlaying)//��� �Ǵ� ȿ������ �ִٸ� �Ѿ
                continue;
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx+ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }
}
