using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoopUI : MonoBehaviour
{
    public bool isPlay;
    public float speed;
    public Vector3 direction;

    RectTransform rectTransform;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isPlay)
        {
            rectTransform.localEulerAngles += direction * speed * Time.deltaTime;
        }
    }
}
