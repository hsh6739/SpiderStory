using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    RectTransform rectTransform; // ���̽�ƽ RectTransform

    Vector2 joyStickPos;

    void Start()
    {
        rectTransform = transform.parent.GetComponent<RectTransform>();
        joyStickPos = Vector2.zero;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // ���̽�ƽ�� Ŭ������ �� ȣ��Ǵ� �Լ�
        //joyStickPos.x = Input.mousePosition.x - Screen.width / 2;
        //joyStickPos.y = Input.mousePosition.y - rectTransform.sizeDelta.y / 2;

        transform.localPosition = Vector2.zero;

        joyStickPos.x = Input.mousePosition.x - Screen.width / 2;
        joyStickPos.y = Input.mousePosition.y - Screen.height / 2;
        rectTransform.anchoredPosition = joyStickPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickPos.x = 0;
        joyStickPos.y = -734;
        rectTransform.anchoredPosition = joyStickPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���̽�ƽ�� �巡���� �� ȣ��Ǵ� �Լ�
        // ���⿡ ���̽�ƽ�� �巡���� ���� ������ �߰�
        // ��: �÷��̾� �̵�, ī�޶� ȸ�� ��

    }

}
