using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Camera mainCamera;

    private SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        mainCamera = Camera.main;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 플레이어 이동 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 화면 경계 체크하여 플레이어 이동 제한
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        float playerHalfWidth = playerSpriteRenderer.bounds.size.x / 2;
        float playerHalfHeight = playerSpriteRenderer.bounds.size.y / 2;

        //float clampedX = Mathf.Clamp(newPosition.x, -screenWidth + playerHalfWidth, screenWidth - playerHalfWidth);
        //float clampedY = Mathf.Clamp(newPosition.y, -screenHeight + playerHalfHeight, screenHeight - playerHalfHeight);

        float clampedX = Mathf.Clamp(newPosition.x, -15, 15);
        float clampedY = Mathf.Clamp(newPosition.y, -15, 15);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

    }

}
