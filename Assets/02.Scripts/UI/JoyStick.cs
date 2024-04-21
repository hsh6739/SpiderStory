using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    RectTransform rectTransform; // 조이스틱 RectTransform

    Vector2 joyStickPos;

    void Start()
    {
        rectTransform = transform.parent.GetComponent<RectTransform>();
        joyStickPos = Vector2.zero;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // 조이스틱을 클릭했을 때 호출되는 함수
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
        // 조이스틱을 드래그할 때 호출되는 함수
        // 여기에 조이스틱을 드래그할 때의 동작을 추가
        // 예: 플레이어 이동, 카메라 회전 등

    }

}
