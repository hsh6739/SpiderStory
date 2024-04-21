using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    Animator uiAnimator;

    [Header("# UI GameObject")]
    public GameObject gameStart;
    public GameObject HUD;
    public GameObject pause;
    public GameObject itemSelect;
    public GameObject gameResult;
    public GameObject gameClear;

    [Header("# HUD Text")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI GoldText;


    [Header("# Select Item Text")]
    public TextMeshProUGUI SelectItemGoldText;


    [Header("# GameResult UI")]
    public TextMeshProUGUI resultTimeText;
    public TextMeshProUGUI resultCoinScoreText;

    [Header("# GameClear UI")]
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI ClearCoinScoreText;

    [Header("# JoyStick")]
    public Transform JoyStick;

    [Header("# TraderArrow")]
    public GameObject TraderArrow;

    float gameTime;

    void Start()
    {
        uiAnimator = canvas.GetComponent<Animator>();

        gameStart.SetActive(true);
        HUD.SetActive(false);
        pause.SetActive(false);
        itemSelect.SetActive(false);
        gameResult.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isLive)
        {
            gameTime = GameManager.Instance.gameTime;

            // HUD 화면
            timeText.text = Mathf.Floor(gameTime / 60).ToString("00") + ":" + Mathf.Floor(gameTime % 60).ToString("00");
            LevelText.text = "Lv. " + GameManager.Instance.level.ToString("N0");
            killText.text = "<color=#FF4747> Kill </color> : " + GameManager.Instance.kill.ToString("N0");
            GoldText.text = "<color=#FBFF4A> Gold </color> : " + GameManager.Instance.goldCount.ToString("N0");

            // 아이템 선택화면 Gold
            SelectItemGoldText.text = "<color=#FBFF4A>" + GameManager.Instance.goldCount.ToString("N0") + "</color>";
        }

    }

    public void GameStart()
    {
        AllOff();
        HUD.SetActive(true);
        ActiveJoyStick(true);
        TraderArrow.SetActive(true);
        uiAnimator.SetTrigger("Start");
    }
    public void ItemSelect()
    {
        itemSelect.SetActive(true);
        ActiveJoyStick(false);
    }

    // 일시정지
    public void Pause()
    {
        ActiveJoyStick(false);
        pause.SetActive(true);
    }
    // 재개
    public void Resume()
    {
        ActiveJoyStick(true);
        itemSelect.SetActive(false);
        pause.SetActive(false);
    }

    public void GameOver()
    {
        AllOff();
        gameResult.SetActive(true);
        ActiveJoyStick(false);

        resultTimeText.text = "진행 시간\n" + (gameTime / 60).ToString("00") + ":" + (gameTime % 60).ToString("00");
        resultCoinScoreText.text = "골드\n" + GameManager.Instance.goldCount.ToString("N0");
    }

    public void GameClear()
    {
        AllOff();
        gameClear.SetActive(true);
        ActiveJoyStick(false);
        TraderArrow.SetActive(false);

        ClearTimeText.text = "진행 시간\n" + (gameTime / 60).ToString("00") + ":" + (gameTime % 60).ToString("00");
        ClearCoinScoreText.text = "골드\n" + GameManager.Instance.goldCount.ToString("N0");
    }

    public void AllOff()
    {
        gameStart.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        itemSelect.SetActive(false);
        gameResult.SetActive(false);
        gameClear.SetActive(false);
    }

    public void ActiveJoyStick(bool val)
    {
        if (val)
            JoyStick.localScale = Vector3.one;
        else
            JoyStick.localScale = Vector3.zero;
    }


}
