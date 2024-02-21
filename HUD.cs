using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType//열거형
    {
        Exp,Level,Kill,Time,Health , Damage,
    }

    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();

    }
    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;//현재 경험치 나누기 다음 레벨 경험치
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level+1);//포맷을 쓸 타입{}안에 순서의 인자값이 들어간다는 뜻 : 형식 F0은 소수점이 없다는 뜻, 적용되는 데이터, 
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                /*
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec); //D는 자릿수를 고정하는 역할
                break;
                */
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;

                break;
            case InfoType.Damage:
                myText.text = string.Format("Damage:{0:F1}", GameManager.instance.nowDamage);//포맷을 쓸 타입{}안에 순서의 인자값이 들어간다는 뜻 : 형식 F0은 소수점이 없다는 뜻, 적용되는 데이터, 
                break;

        }
    }
}
