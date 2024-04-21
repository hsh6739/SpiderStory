using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshPro text;
    Color alpha;
    public float damage;
    
    public float delayTime_1;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1f;
        alphaSpeed = 5f;
        destroyTime = 2f;

        text = GetComponent<TextMeshPro>();

        // 데미지에 따른 색 바꾸기
        // 흰색 225 225 225
        if (damage <= 10)
        {
            text.color = new Color(225 / 255f, 225 / 255f, 225 / 255f);
        }
        // 노랑 225 225 100
        else if (damage <= 20)
        {
            text.color = new Color(225 / 255f, 225 / 255f, 100 / 255f);
        }
        // 초록 100 225 100
        else// if (damage <= 30)
        {
            text.color = new Color(100 / 255f, 225 / 255f, 100 / 255f);
        }

        alpha = text.color;
        text.text = damage.ToString("n1");



        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        delayTime_1 += Time.deltaTime;

        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치


        if (delayTime_1 > 0.3f)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
            text.color = alpha;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}