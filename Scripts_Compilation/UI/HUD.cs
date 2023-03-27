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

                float curExp = GameManager.instance.exp; // ���� ����ġ
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level]; //���� �������� �ʿ� ����ġ

                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:

                myText.text = string.Format("LV.{0:F0}", GameManager.instance.level);

                break;

            case InfoType.Kill:

                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;

            case InfoType.Time:

                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;    // ���� �ð�

                int min = Mathf.FloorToInt(remainTime / 60); // �� ���ϱ�(�Ҽ��� ����)

                int sec = Mathf.FloorToInt(remainTime % 60); // �� ���ϱ�(�Ҽ��� ����)

                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.Health:

                float curHealth = GameManager.instance.health;      // ���� �÷��̾� ü��
                float maxHealth = GameManager.instance.maxHealth;   //�÷��̾� �ִ� ü��

                mySlider.value = curHealth / maxHealth;

                break;
        }
    }
}
