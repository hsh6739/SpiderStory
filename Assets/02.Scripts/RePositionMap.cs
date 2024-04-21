using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePositionMap : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 할당
    public float gridSize; // 각 배경 이미지의 크기

    public int mapNum;

    void Start()
    {
        player = GameManager.Instance.player.transform;

        // 몇번째 맵인지 확인
        mapNum = 0;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                mapNum = i;
                break;
            }
        }

    }

    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            Debug.Log("Enter  " + this.gameObject.name);
            PositionUpdate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            Debug.Log("Exit  " + this.gameObject.name);
            PositionUpdate();
        }
    }

    private void PositionUpdate()
    {
        //Debug.Log("PositionUpdate !!");

        // 플레이어의 위치에 따라 배경 이미지의 위치를 동적으로 조정
        Vector3 playerPosition = player.position;

        float offsetX = 0;
        float offsetY = 0;

        switch (mapNum)
        {
            case 0:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) - 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) + 1) * gridSize;
                break;
            case 1:
                offsetX = (Mathf.Round(playerPosition.x / gridSize)) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) + 1) * gridSize;
                break;
            case 2:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) + 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) + 1) * gridSize;
                break;
            case 3:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) - 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize)) * gridSize;
                break;
            case 4:
                offsetX = (Mathf.Round(playerPosition.x / gridSize)) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize)) * gridSize;
                break;
            case 5:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) + 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize)) * gridSize;
                break;
            case 6:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) - 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) - 1) * gridSize;
                break;
            case 7:
                offsetX = (Mathf.Round(playerPosition.x / gridSize)) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) - 1) * gridSize;
                break;
            case 8:
                offsetX = (Mathf.Round(playerPosition.x / gridSize) + 1) * gridSize;
                offsetY = (Mathf.Round(playerPosition.y / gridSize) - 1) * gridSize;
                break;


            default:
                break;
        }


        transform.position = new Vector3(offsetX, offsetY, transform.position.z);

    }
}