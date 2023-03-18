using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
   public ItemData.ItemType type;
    public float rate;  // ����

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
                    weapon.speed = 150 + (150 * rate);      // ���������� speed�� ȸ���ϴ� �ӵ� => ���� Ŀ������ ȸ�� �ӵ� ������
                    break;

                default:
                    weapon.speed = .5f * (1f- rate);        // ���Ÿ� ������ speed�� �߻�ӵ� => ���� �������� �߻�ӵ� ������
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
