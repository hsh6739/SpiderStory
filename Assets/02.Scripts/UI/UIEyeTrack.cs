using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEyeTrack : MonoBehaviour
{
    [Header("Face UI Obj")]
    public GameObject Eye_1;
    public GameObject Eye_2;
    public GameObject Eye_3;
    public GameObject Eye_4;
    public GameObject Eyebrow;
    public GameObject Mouth;

    RectTransform eyeRT1;
    RectTransform eyeRT2;
    RectTransform eyeRT3;
    RectTransform eyeRT4;
    RectTransform eyebrowRT;
    RectTransform mouthRT;

    Vector2 eyePos1;
    Vector2 eyePos2;
    Vector2 eyePos3;
    Vector2 eyePos4;
    Vector2 eyebrowPos;
    Vector2 mouthPos;

    Vector2 playerPos;

    int health;

    void Start()
    {
        eyeRT1 = Eye_1.GetComponent<RectTransform>();
        eyeRT2 = Eye_2.GetComponent<RectTransform>();
        eyeRT3 = Eye_3.GetComponent<RectTransform>();
        eyeRT4 = Eye_4.GetComponent<RectTransform>();
        eyebrowRT = Eyebrow.GetComponent<RectTransform>();
        mouthRT = Mouth.GetComponent<RectTransform>();

        eyePos1 = eyeRT1.anchoredPosition;
        eyePos2 = eyeRT2.anchoredPosition;
        eyePos3 = eyeRT3.anchoredPosition;
        eyePos4 = eyeRT4.anchoredPosition;
        eyebrowPos = eyebrowRT.anchoredPosition;
        mouthPos = mouthRT.anchoredPosition;

        health = GameManager.Instance.health;
    }

    void Update()
    {
        playerPos = GameManager.Instance.player.GetComponent<Player>().inputVec;

        // 플레이어 눈알 개수 UI 바꾸기
        if (health != GameManager.Instance.health)
        {
            switch (GameManager.Instance.health)
            {
                case 0:
                    Alloff();
                    Eye_1.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_2.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_3.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_4.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 1:
                    Alloff();
                    Eye_1.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_2.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_3.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_4.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 2:
                    Alloff();
                    Eye_1.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_2.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_3.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_4.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 3:
                    Alloff();
                    Eye_1.transform.GetChild(1).gameObject.SetActive(true);
                    Eye_2.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_3.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_4.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 4:
                    Alloff();
                    Eye_1.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_2.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_3.transform.GetChild(0).gameObject.SetActive(true);
                    Eye_4.transform.GetChild(0).gameObject.SetActive(true);
                    break;

                default:
                    break;
            }
        }

        // 눈 위치 움직임
        eyeRT1.anchoredPosition = eyePos1 + playerPos * 4;
        eyeRT2.anchoredPosition = eyePos2 + playerPos * 4;
        eyeRT3.anchoredPosition = eyePos3 + playerPos * 8;
        eyeRT4.anchoredPosition = eyePos4 + playerPos * 8;
        eyebrowRT.anchoredPosition = eyebrowPos + playerPos * 4;
        mouthRT.anchoredPosition = mouthPos + playerPos * 4;


        health = GameManager.Instance.health;
    }


    void Alloff()
    {
        Eye_1.transform.GetChild(0).gameObject.SetActive(false);
        Eye_1.transform.GetChild(1).gameObject.SetActive(false);
        Eye_2.transform.GetChild(0).gameObject.SetActive(false);
        Eye_2.transform.GetChild(1).gameObject.SetActive(false);
        Eye_3.transform.GetChild(0).gameObject.SetActive(false);
        Eye_3.transform.GetChild(1).gameObject.SetActive(false);
        Eye_4.transform.GetChild(0).gameObject.SetActive(false);
        Eye_4.transform.GetChild(1).gameObject.SetActive(false);
    }

}
