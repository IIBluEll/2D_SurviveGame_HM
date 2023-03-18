using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal} // ������ �Ӽ� { ����, ���Ÿ�, �尩, �Ź�, ���� }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;                      // ������ ID
    public string itemName;                 // ������ �̸�
    public string itemDescripton;           // ������ ����
    public Sprite itemIcon;                 // ������ ������ ��������Ʈ

    [Header("# Level Data")]

    public float baseDamge; // �⺻ ������
    public int baseCount;   // �⺻ ����
    public float[] damages;
    public int[] counts;


    [Header("# Weapon")]

    public GameObject projectile;   // ����ü ������

}
