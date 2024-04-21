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
    public ItemType itemType; // ������ Ÿ��
    public int itemId; // ������ID
    public string itemName; // ������ �̸�

    [TextArea]
    public string itemDesc; // ������ ����
    public Sprite itemIcon; // ������ ������ �̹���

    [Header("# Level Data")]
    //public float baseDamage; // �⺻ ������
    //public int baseCount; // �⺻ ź, ���� ����
    public int level;
    public int maxLevel;
    public float[] damages; // ������ ���� ������
    public int[] counts; // ������ ���� ź, ���� ����
    public float[] fireRates; // ���� �ӵ�
    public float[] rotationSpeeds; // ȸ���ӵ�
    public float[] scales; // ũ��

    [Header("# Weapon ")]
    public GameObject projectilePrefab; // ����ü ������
    public Sprite projectileImage; // ����ü �̹���

}
