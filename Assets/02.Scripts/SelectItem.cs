using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    private void OnEnable()
    {
        // 1. 자식 전부 비활성화
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
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

            // 3. 만렙 아이템의 경우는 소비 아이템으로 대체
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
