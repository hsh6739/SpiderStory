using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 5 * 60f;
    public int goldCount;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 4;
    public int level;
    public int maxLevel = 9;
    public int[] nextExp;
    public int kill;
    public int exp;

    [Header("# GameObject")]
    public UIManager uiManager;
    public PoolManager pool;
    public GameObject player;
    //public Transform uiJoyStick;

    [Header("# Prefab")]
    public GameObject damageText;

    void Awake()
    {
        // 60프레임으로 고정
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

    }

    public void GameStart()
    {
        isLive = true;
        uiManager.GameStart();
        player.SetActive(true);
        Resume();

    }
    public void GameClear()
    {
        StartCoroutine(GameClearRoutine());
    }
    IEnumerator GameClearRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(2f);
        uiManager.GameClear();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiManager.GameOver();
    }

    // 게임 다시 하기
    public void GameRetry()
    {
        // 씬 재 업로드
        SceneManager.LoadScene(0);
    }

    // 단순 정지
    public void Stop()
    {
        //isLive = false;
        Time.timeScale = 0;
        uiManager.ActiveJoyStick(false);
    }
    // 일시정지 버튼 클릭시
    public void Pause()
    {
        Stop();
        uiManager.Pause();
    }

    // 게임 재게
    public void Resume()
    {
        //isLive = true;
        Time.timeScale = 1;
        uiManager.ActiveJoyStick(true);
        uiManager.Resume();
    }

    // 경험치 얻기
    public void GetExp()
    {
        exp++;
        if (level < maxLevel)
        {
            if (exp == nextExp[level])
            {
                level++;
                exp = 0;
            }
        }
    }

    // 상인 아이템 선택
    public void ItemSelect()
    {
        uiManager.ItemSelect();
        Stop();
    }

    // 게임 종료
    public void GameQuit()
    {
        Application.Quit();
    }


}
