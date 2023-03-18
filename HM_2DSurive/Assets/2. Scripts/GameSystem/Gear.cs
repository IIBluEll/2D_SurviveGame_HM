using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
   public ItemData.ItemType type;
    public float rate;  // 공속

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

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

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

    void RateUp()
    {
        Weapon[] weapons = this.transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
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

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
