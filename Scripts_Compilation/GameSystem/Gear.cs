using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
   public ItemData.ItemType type;   // ��� Ÿ��
    public float rate;  // ����

    // ������ �����͸� ������� �ʱ�ȭ�ϴ� �Լ�
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

    // ������ �� �ӵ� ����
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    // ��� �Ӽ��� ���� ��� ����
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

    // ���� �ӵ� ����
    void RateUp()
    {
        Weapon[] weapons = this.transform.parent.GetComponentsInChildren<Weapon>();

        // ���⺰ �ӵ� ����
        foreach (Weapon weapon in weapons)
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

    // �̵� �ӵ� ����
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
