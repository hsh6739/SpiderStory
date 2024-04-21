using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToNPC : MonoBehaviour
{
    public Transform player; // 플레이어의 위치
    public Transform npc; // NPC의 위치
    public GameObject arrow; // 캔버스 위의 화살표 게임 오브젝트
    public float distanceToShowArrow = 5f; // 화살표가 보이기 시작하는 거리

    void Update()
    {
        if (player != null && npc != null)
        {
            Vector3 direction = npc.position - player.position;

            if (direction.magnitude <= distanceToShowArrow) // 일정 거리 이내에 있을 때만 화살표 표시
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // 화살표를 보이도록 활성화
                arrow.SetActive(true);

                RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
                Vector3 screenPos = Camera.main.WorldToViewportPoint(npc.position);
                Vector3 targetPos = new Vector3((screenPos.x - 0.5f) * canvasRect.sizeDelta.x, (screenPos.y - 0.5f) * canvasRect.sizeDelta.y);
                arrow.GetComponent<RectTransform>().anchoredPosition = targetPos;
                arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // 일정 거리를 벗어나면 화살표 비활성화
                arrow.SetActive(false);
            }
        }
    }
}
