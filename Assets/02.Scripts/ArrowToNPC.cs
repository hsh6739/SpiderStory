using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToNPC : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ
    public Transform npc; // NPC�� ��ġ
    public GameObject arrow; // ĵ���� ���� ȭ��ǥ ���� ������Ʈ
    public float distanceToShowArrow = 5f; // ȭ��ǥ�� ���̱� �����ϴ� �Ÿ�

    void Update()
    {
        if (player != null && npc != null)
        {
            Vector3 direction = npc.position - player.position;

            if (direction.magnitude <= distanceToShowArrow) // ���� �Ÿ� �̳��� ���� ���� ȭ��ǥ ǥ��
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // ȭ��ǥ�� ���̵��� Ȱ��ȭ
                arrow.SetActive(true);

                RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
                Vector3 screenPos = Camera.main.WorldToViewportPoint(npc.position);
                Vector3 targetPos = new Vector3((screenPos.x - 0.5f) * canvasRect.sizeDelta.x, (screenPos.y - 0.5f) * canvasRect.sizeDelta.y);
                arrow.GetComponent<RectTransform>().anchoredPosition = targetPos;
                arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // ���� �Ÿ��� ����� ȭ��ǥ ��Ȱ��ȭ
                arrow.SetActive(false);
            }
        }
    }
}
