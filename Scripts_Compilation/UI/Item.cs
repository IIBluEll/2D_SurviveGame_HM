using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // 아이템 관리 변수들 
    public ItemData data;   // 아이템 데이터 정보
    public Weapon weapon;   // 무기 정보
    public Gear gear;       // 장비 정보

    public int level;       // 아이템 레벨

    Image icon;             // 아이콘 이미지
    Text textLevel;         // 레벨 텍스트

    private void Awake()
    {
        // 아이콘 이미지 설정
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        // 레벨 텍스트 설정
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        // 레벨 텍스트 업데이트
        textLevel.text = "LV." + (level);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:   // 근접 무기
            case ItemData.ItemType.Range:   // 원거리 무기

                if (level == 0)             // 무기 레벨이 0인 경우
                {
                    GameObject newWeapon = new GameObject();    // 무기 게임오브젝트 생성
                    weapon = newWeapon.AddComponent<Weapon>();  // 무기 정보 컴포넌트 추가

                    weapon.Init(data);      // 무기 정보 초기화
                }
                else // 무기 레벨이 0이 아닌 경우
                {
                    float nextDamage = data.baseDamge;
                    int nextCount = 0;

                    nextDamage += data.baseDamge * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUP(nextDamage, nextCount);  // 무기 레벨 업
                }
                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:

                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear= newGear.AddComponent<Gear>();

                    gear.Init(data);            // 장비 정보 초기화
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);     // 장비 레벨 업
                }
                level++;
                break;


            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        // 아이템 레벨이 최대 레벨인 경우 아이템 사용 불가능
        if (level  == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }

    }
}
