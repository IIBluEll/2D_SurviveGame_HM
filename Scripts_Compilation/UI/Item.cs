using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // ������ ���� ������ 
    public ItemData data;   // ������ ������ ����
    public Weapon weapon;   // ���� ����
    public Gear gear;       // ��� ����

    public int level;       // ������ ����

    Image icon;             // ������ �̹���
    Text textLevel;         // ���� �ؽ�Ʈ

    private void Awake()
    {
        // ������ �̹��� ����
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        // ���� �ؽ�Ʈ ����
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        // ���� �ؽ�Ʈ ������Ʈ
        textLevel.text = "LV." + (level);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:   // ���� ����
            case ItemData.ItemType.Range:   // ���Ÿ� ����

                if (level == 0)             // ���� ������ 0�� ���
                {
                    GameObject newWeapon = new GameObject();    // ���� ���ӿ�����Ʈ ����
                    weapon = newWeapon.AddComponent<Weapon>();  // ���� ���� ������Ʈ �߰�

                    weapon.Init(data);      // ���� ���� �ʱ�ȭ
                }
                else // ���� ������ 0�� �ƴ� ���
                {
                    float nextDamage = data.baseDamge;
                    int nextCount = 0;

                    nextDamage += data.baseDamge * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUP(nextDamage, nextCount);  // ���� ���� ��
                }
                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:

                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear= newGear.AddComponent<Gear>();

                    gear.Init(data);            // ��� ���� �ʱ�ȭ
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);     // ��� ���� ��
                }
                level++;
                break;


            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        // ������ ������ �ִ� ������ ��� ������ ��� �Ұ���
        if (level  == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }

    }
}
