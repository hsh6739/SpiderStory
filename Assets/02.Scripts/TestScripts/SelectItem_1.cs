using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///  이 스크립트 지우기
/// </summary>
/// 
public class SelectItem_1 : MonoBehaviour
{
    public GameObject ItemPrefab;

    public Image[] iconImages;


    int enumCount;
    ItemType ItemId;


    private void Start()
    {
        enumCount = Enum.GetNames(typeof(ItemType)).Length;

        Init();
    }


    void Init()
    {
        // 자식 전부 삭제
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }


        // 랜덤으로 3개 추출하기

        List<int> itemNumbers = new List<int>(); // 아이템 전부 담긴 리스트
        List<int> selectedNumbers = new List<int>(); // 선택된 리스트

        for (int i = 0; i < enumCount; i++)
        {
            itemNumbers.Add(i);
        }

        System.Random random = new System.Random(); // System.Random 클래스를 사용하기 위해 인스턴스 생성

        // 3개의 랜덤한 숫자를 선택
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = random.Next(0, itemNumbers.Count);
            selectedNumbers.Add(itemNumbers[randomIndex]);
            itemNumbers.RemoveAt(randomIndex);
        }

        // 3번 생성
        for (int i = 0; i < 3; i++)
        {
            ItemId = (ItemType)itemNumbers[i];

            // 프리팹을 통해 생성하기
            GameObject itemObject = Instantiate(ItemPrefab);
            itemObject.transform.SetParent(transform);// 자식으로 생성





        }
    }

}
