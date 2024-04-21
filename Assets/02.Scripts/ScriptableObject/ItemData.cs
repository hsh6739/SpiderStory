using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WheelSaw,
    Rifle,
    SniperRifle,
    BallString,
    Web,
    SpeedUp,
    HealPack,
    PickupRangeUp
}

public enum FieldDropItemType
{
    Coin_A,
    Coin_B,
    Coin_C,
    HealPackItem,
    BombItem,
    MagnetItem
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptble Object/ItemData")]

public class ItemData : ScriptableObject
{
    [Header("# Main Info")]
    public ItemType itemType; // 아이템 타입
    public int itemId; // 아이템ID
    public string itemName; // 아이템 이름

    [TextArea]
    public string itemDesc; // 아이템 설명
    public Sprite itemIcon; // 아이템 아이콘 이미지

    [Header("# Level Data")]
    //public float baseDamage; // 기본 데미지
    //public int baseCount; // 기본 탄, 무기 개수
    public int level;
    public int maxLevel;
    public float[] damages; // 레벨에 따른 데미지
    public int[] counts; // 레벨에 따른 탄, 무기 개수
    public float[] fireRates; // 연사 속도
    public float[] rotationSpeeds; // 회전속도
    public float[] scales; // 크기

    [Header("# Weapon ")]
    public GameObject projectilePrefab; // 투사체 프리팹
    public Sprite projectileImage; // 투사체 이미지

}
