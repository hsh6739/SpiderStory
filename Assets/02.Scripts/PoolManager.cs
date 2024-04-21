using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] Enemies;
    public GameObject[] WeaponBullets;
    public GameObject[] Probs;

    // �����յ��� ������ ����
    //public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] enemiesPools;
    List<GameObject>[] weaponBulletsPools;
    List<GameObject>[] probsPools;

    //int prefabLength;

    void Awake()
    {
        //prefabLength = Enemies.Length + WeaponBullets.Length + Probs.Length;
        //pools = new List<GameObject>[prefabLength];

        enemiesPools = new List<GameObject>[Enemies.Length];
        weaponBulletsPools = new List<GameObject>[WeaponBullets.Length];
        probsPools = new List<GameObject>[Probs.Length];

        for (int index = 0; index < Enemies.Length; index++)
        {
            enemiesPools[index] = new List<GameObject>();
        }
        for (int index = 0; index < WeaponBullets.Length; index++)
        {
            weaponBulletsPools[index] = new List<GameObject>();
        }
        for (int index = 0; index < Probs.Length; index++)
        {
            probsPools[index] = new List<GameObject>();
        }


    }

    public GameObject GetEnemies(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ����
        foreach (GameObject item in enemiesPools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ��ã������?
        if (!select)
        {
            // ... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(Enemies[index], transform);
            enemiesPools[index].Add(select);
        }

        return select;
    }

    public GameObject GetWeaponBullets(int index)
    {
        GameObject select = null;
        foreach (GameObject item in weaponBulletsPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(WeaponBullets[index], transform);
            weaponBulletsPools[index].Add(select);
        }

        return select;
    }
    public GameObject GetProbs(int index)
    {
        GameObject select = null;
        foreach (GameObject item in probsPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(Probs[index], transform);
            probsPools[index].Add(select);
        }

        return select;
    }


}
