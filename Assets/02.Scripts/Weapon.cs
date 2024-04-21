using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ItemData itemData; // ScriptableObject
    //public ItemType weaponId;

    public int prefabId; // Ǯ �Ŵ����� �Ҵ�� id
    public float damage; // ���� ������
    public int count; // ź, ���� ����

    public float fireRate; // ���� �ӵ�
    //public float bulletSpeed; // ź��
    public float rotationSpeed; // ȸ���ӵ�

    float timer;
    Player player;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        switch (itemData.itemType)
        {
            case ItemType.WheelSaw:
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                break;
            case ItemType.Rifle:
            case ItemType.SniperRifle:
            case ItemType.BallString:
                timer += Time.deltaTime;
                if (timer > fireRate)
                {
                    Fire();
                    timer = 0;
                }
                break;
            case ItemType.Web:
                break;
            default:
                break;
        }


        // TEST CODE
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("TEST CODE");
            //LevelUp(5, 1);
        }

    }

    public void LevelUp()
    {
        if (itemData.level == itemData.maxLevel)
        {
            Debug.Log("Max Level !!");
            return;
        }

        //itemData.level++;

        switch (itemData.itemType)
        {
            case ItemType.WheelSaw:
                damage = itemData.damages[itemData.level];
                count = itemData.counts[itemData.level];
                rotationSpeed = itemData.rotationSpeeds[itemData.level];
                Batch();
                break;
            case ItemType.Rifle:
            case ItemType.SniperRifle:
            case ItemType.BallString:
                damage = itemData.damages[itemData.level];
                count = itemData.counts[itemData.level];
                fireRate = itemData.fireRates[itemData.level];
                break;
            case ItemType.Web:
                damage = itemData.damages[itemData.level];
                transform.GetChild(0).localScale = Vector3.one * itemData.scales[itemData.level];
                break;
            default:
                break;
        }

    }

    public void Init()
    {
        switch (itemData.itemType)
        {
            case ItemType.WheelSaw:
                damage = itemData.damages[0];
                count = itemData.counts[0];
                rotationSpeed = itemData.rotationSpeeds[0];
                Batch();
                break;
            case ItemType.Rifle:
            case ItemType.SniperRifle:
            case ItemType.BallString:
                damage = itemData.damages[0];
                count = itemData.counts[0];
                fireRate = itemData.fireRates[0];
                break;
            case ItemType.Web:
                damage = itemData.damages[0];
                transform.GetChild(0).localScale = Vector3.one * itemData.scales[0];
                break;
            default:
                break;
        }

    }


    // ���� ��ġ ��ġ�ϱ�
    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.GetWeaponBullets(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 3f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is Infinity Per.
        }

    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) // ��ĵ ����� ���ٸ� return
        {
            player.SetAttackAnim(false);
            return;
        }

        player.SetAttackAnim(true);


        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet= GameManager.Instance.pool.GetWeaponBullets(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }


}
