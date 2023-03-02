using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
   public enum InfoType { Exp, Level, Kill, Time, Health}

    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:

                float curExp = GameManager.instance.exp; // 현재 경험치
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level]; //다음 레벨까지 필요 경험치

                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:

                myText.text = string.Format("LV.{0:F0}", GameManager.instance.level);

                break;

            case InfoType.Kill:

                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;

            case InfoType.Time:

                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;    // 남은 시간

                int min = Mathf.FloorToInt(remainTime / 60); // 분 구하기(소수점 버림)

                int sec = Mathf.FloorToInt(remainTime % 60); // 초 구하기(소수점 버림)

                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.Health:

                float curHealth = GameManager.instance.health;      // 현재 플레이어 체력
                float maxHealth = GameManager.instance.maxHealth;   //플레이어 최대 체력

                mySlider.value = curHealth / maxHealth;

                break;
        }
    }
}
