using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
   public ItemData.ItemType type;   // 장비 타입
    public float rate;  // 공속

    // 아이템 데이터를 기반으로 초기화하는 함수
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition= Vector3.zero;
        
        // Property Set
        type = data.itemType;
        rate = data.damages[0];

        ApplyGear();
    }

    // 레벨업 시 속도 증가
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    // 장비 속성에 따른 기능 적용
    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;

            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // 공격 속도 증가
    void RateUp()
    {
        Weapon[] weapons = this.transform.parent.GetComponentsInChildren<Weapon>();

        // 무기별 속도 조절
        foreach (Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);      // 근접무기의 speed는 회전하는 속도 => 값이 커질수록 회전 속도 빨라짐
                    break;

                default:
                    weapon.speed = .5f * (1f- rate);        // 원거리 무기의 speed는 발사속도 => 값이 작을수록 발사속도 빨라짐
                    break;
            }
        }
    }

    // 이동 속도 증가
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
