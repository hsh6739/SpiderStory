using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDirBox : MonoBehaviour
{
    public Transform trader;
    GameObject traderDirObj;

    public float edgePadding = 80f; // ȭ�� �����ڸ����� ����
    public float topEdgePadding; // ȭ�� ���� UI ����
    public float bottomEdgePadding; // ȭ�� �Ʒ��� UI ����

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

        // ������ ȭ�� �ۿ� ������
        if (canvasTraderrPos.x < - Screen.width / 2f || canvasTraderrPos.x > Screen.width / 2f 
            || canvasTraderrPos.y < -Screen.height / 2f || canvasTraderrPos.y > Screen.height / 2f - topEdgePadding)
        {
            traderDirObj.SetActive(true);
            // ȭ��ǥ �̹��� ��ġ ����


            // ȸ��
            traderDir = (trader.position - GameManager.Instance.player.transform.position).normalized;
            tempRot.z = Mathf.Atan2(traderDir.y, traderDir.x) * Mathf.Rad2Deg + 90f;
            traderDirObj.transform.localEulerAngles = tempRot;

            // ȭ�� �����ڸ��� ��ġ�� ��ġ ���
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
        else // ������ ȭ�� �ȿ� ������
        {
            traderDirObj.SetActive(false);
        }

    }
}
