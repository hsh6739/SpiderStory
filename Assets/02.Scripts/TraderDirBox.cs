using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDirBox : MonoBehaviour
{
    public Transform trader;
    GameObject traderDirObj;

    public float edgePadding = 80f; // 화면 가장자리와의 간격
    public float topEdgePadding; // 화면 위쪽 UI 간격
    public float bottomEdgePadding; // 화면 아래쪽 UI 간격

    //Vector3 canvasPlayerPos;
    Vector3 screenCenter;
    Vector3 canvasTraderrPos;
    Vector3 traderDir;

    Vector3 tempPos;
    Vector3 tempRot;

    void Start()
    {
        traderDirObj = transform.GetChild(0).gameObject;
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        tempPos = Vector3.zero;
    }

    void Update()
    {
        canvasTraderrPos = Camera.main.WorldToScreenPoint(trader.position) - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // 상인이 화면 밖에 있을때
        if (canvasTraderrPos.x < - Screen.width / 2f || canvasTraderrPos.x > Screen.width / 2f 
            || canvasTraderrPos.y < -Screen.height / 2f || canvasTraderrPos.y > Screen.height / 2f - topEdgePadding)
        {
            traderDirObj.SetActive(true);
            // 화살표 이미지 위치 설정


            // 회전
            traderDir = (trader.position - GameManager.Instance.player.transform.position).normalized;
            tempRot.z = Mathf.Atan2(traderDir.y, traderDir.x) * Mathf.Rad2Deg + 90f;
            traderDirObj.transform.localEulerAngles = tempRot;

            // 화면 가장자리에 배치할 위치 계산
            if (canvasTraderrPos.x > Screen.width / 2f - edgePadding)
                tempPos.x = Screen.width / 2f - edgePadding;
            else if (canvasTraderrPos.x < -Screen.width / 2f + edgePadding)
                tempPos.x = -Screen.width / 2f + edgePadding;
            else
                tempPos.x = canvasTraderrPos.x;

            if (canvasTraderrPos.y > Screen.height / 2f - edgePadding - topEdgePadding)
                tempPos.y = Screen.height / 2f - edgePadding - topEdgePadding;
            else if (canvasTraderrPos.y < -Screen.height / 2f + edgePadding  + bottomEdgePadding)
                tempPos.y = -Screen.height / 2f + edgePadding + bottomEdgePadding;
            else
                tempPos.y = canvasTraderrPos.y;

            traderDirObj.transform.localPosition = tempPos;

        }
        else // 상인이 화면 안에 있을때
        {
            traderDirObj.SetActive(false);
        }

    }
}
