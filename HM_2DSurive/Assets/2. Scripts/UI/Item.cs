using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // ������ ���� ������ 
    public ItemData data;
    public Weapon weapon;
    public Gear gear;

    public int level;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "LV." + (level);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:

                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();

                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamge;
                    int nextCount = 0;

                    nextDamage += data.baseDamge * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUP(nextDamage, nextCount);
                }
                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:

                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear= newGear.AddComponent<Gear>();

                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate); 
                }
                level++;
                break;


            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

       

        if(level  == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }

    }
}
