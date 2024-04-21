using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.ItemSelect();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*
            int signX = Random.Range(1, 3); // 1은 + 2는 -
            int signY = Random.Range(1, 3); // 1은 + 2는 -
            if (signX == 1)
                signX = 1;
            else
                signX = -1;

            if (signY == 1)
                signY = 1;
            else
                signY = -1;

            // 상인 랜덤 위치 생성
            Vector3 ranPos = new Vector3(signX * Random.Range(15, 26),
                signY * Random.Range(15, 26), 0);
            */

            Vector3 ranPos = Vector3.zero;
            // 랜덤 위치 생성
            while (ranPos.x < 15f && ranPos.x > -15f
                && ranPos.y < 15f && ranPos.y > -15f)
            {
                ranPos = new Vector3(Random.Range(-26, 26), Random.Range(-26, 26), 0);
            }

            transform.position += ranPos;

            //Debug.Log("상인 위치 변경 : " + ranPos);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }

}
