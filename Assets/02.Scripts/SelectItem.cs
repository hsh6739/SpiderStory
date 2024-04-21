using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    private void OnEnable()
    {
        // 1. �ڽ� ���� ��Ȱ��ȭ
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, transform.childCount);
            ran[1] = Random.Range(0, transform.childCount);
            ran[2] = Random.Range(0, transform.childCount);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            ItemData itemdata = transform.GetChild(ran[index]).GetComponent<SelectWeapon>().itemData;

            // 3. ���� �������� ���� �Һ� ���������� ��ü
            if (itemdata.level == itemdata.maxLevel)
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(ran[index]).gameObject.SetActive(true);
                transform.GetChild(ran[index]).GetComponent<SelectWeapon>().init();
            }

        }
    }
}
