using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal} // 아이템 속성 { 근접, 원거리, 장갑, 신발, 힐템 }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;                      // 아이템 ID
    public string itemName;                 // 아이템 이름
    public string itemDescripton;           // 아이템 설명
    public Sprite itemIcon;                 // 아이템 아이콘 스프라이트

    [Header("# Level Data")]

    public float baseDamge; // 기본 데미지
    public int baseCount;   // 기본 관통
    public float[] damages;
    public int[] counts;


    [Header("# Weapon")]

    public GameObject projectile;   // 투사체 프리펩

}
