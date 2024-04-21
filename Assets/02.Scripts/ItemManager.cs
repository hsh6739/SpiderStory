using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData[] itemDatas;

    float itemTimer;
    GameObject dropItemBox;

    private void Awake()
    {
        // level ���� 0���� �ʱ�ȭ
        foreach (ItemData item in itemDatas)
        {
            item.level = 0;
        }
    }
    void Update()
    {
        itemTimer += Time.deltaTime;

        if (itemTimer > 30f)
        {
            Vector3 ranPos = Vector3.zero;
            // ���� ��ġ ����
            while (ranPos.x < 15f && ranPos.x > -15f
                && ranPos.y < 15f && ranPos.y > -15f)
            {
                ranPos = new Vector3(Random.Range(-26, 26), Random.Range(-26, 26), 0);
            }


            dropItemBox = GameManager.Instance.pool.GetProbs(1);
            dropItemBox.transform.position = GameManager.Instance.player.transform.position + ranPos;
            itemTimer = 0;
        }
        
    }

}
