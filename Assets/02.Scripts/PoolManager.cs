using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] Enemies;
    public GameObject[] WeaponBullets;
    public GameObject[] Probs;

    // 프리팹들을 보관할 변수
    //public GameObject[] prefabs;

    // 풀 담담을 하는 리스트들
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

        // 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근
        foreach (GameObject item in enemiesPools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 못찾았으면?
        if (!select)
        {
            // ... 새롭게 생성하고 select 변수에 할당
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
