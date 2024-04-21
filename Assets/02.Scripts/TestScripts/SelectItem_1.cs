using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///  �� ��ũ��Ʈ �����
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
        // �ڽ� ���� ����
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }


        // �������� 3�� �����ϱ�

        List<int> itemNumbers = new List<int>(); // ������ ���� ��� ����Ʈ
        List<int> selectedNumbers = new List<int>(); // ���õ� ����Ʈ

        for (int i = 0; i < enumCount; i++)
        {
            itemNumbers.Add(i);
        }

        System.Random random = new System.Random(); // System.Random Ŭ������ ����ϱ� ���� �ν��Ͻ� ����

        // 3���� ������ ���ڸ� ����
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = random.Next(0, itemNumbers.Count);
            selectedNumbers.Add(itemNumbers[randomIndex]);
            itemNumbers.RemoveAt(randomIndex);
        }

        // 3�� ����
        for (int i = 0; i < 3; i++)
        {
            ItemId = (ItemType)itemNumbers[i];

            // �������� ���� �����ϱ�
            GameObject itemObject = Instantiate(ItemPrefab);
            itemObject.transform.SetParent(transform);// �ڽ����� ����





        }
    }

}
