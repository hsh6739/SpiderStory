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
        // �÷��̾� �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // ȭ�� ��� üũ�Ͽ� �÷��̾� �̵� ����
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
