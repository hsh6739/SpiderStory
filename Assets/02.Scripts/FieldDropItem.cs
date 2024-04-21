using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDropItem : MonoBehaviour
{
    public FieldDropItemType itemType;

    [Header("# Effect")]
    public GameObject getEffect;

    protected Vector3 playerDir;
    protected float moveSpeed;
    protected CapsuleCollider2D coll;

    void OnEnable()
    {
        coll = GetComponent<CapsuleCollider2D>();
        coll.enabled = true;
        moveSpeed = 5f;


        switch (itemType)
        {
            case FieldDropItemType.BombItem:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case FieldDropItemType.MagnetItem:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerGoldCollider")
        {
            playerDir = (GameManager.Instance.player.transform.position - transform.position).normalized;
            StartCoroutine(MoveBetweenTargets());
        }
    }


    protected IEnumerator MoveBetweenTargets()
    {
        coll.enabled = false;


        // �÷��̾�ٱ� �������� �̵�
        yield return StartCoroutine(MoveToTarget(transform.position + playerDir * -1.5f));

        // �÷��̾�� �̵�
        yield return StartCoroutine(MoveToPlayer());

        GameObject _getEffect;

        switch (itemType)
        {
            case FieldDropItemType.Coin_A:
                GameManager.Instance.goldCount += 1;
                break;
            case FieldDropItemType.Coin_B:
                GameManager.Instance.goldCount += 5;
                break;
            case FieldDropItemType.Coin_C:
                GameManager.Instance.goldCount += 10;
                break;
            case FieldDropItemType.HealPackItem:
                // ����Ʈ ����
                _getEffect = Instantiate(getEffect);
                _getEffect.transform.position = transform.position;
                GameManager.Instance.health += 2;
                if (GameManager.Instance.health > GameManager.Instance.maxHealth)
                    GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.player.GetComponent<Player>().UpdatePlayerEyes();
                break;

            case FieldDropItemType.BombItem:
                _getEffect = Instantiate(getEffect);
                _getEffect.transform.position = transform.position;
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case FieldDropItemType.MagnetItem:
                _getEffect = Instantiate(getEffect);
                _getEffect.transform.position = transform.position;
                transform.GetChild(0).gameObject.SetActive(true);
                break;

        }

        yield return null;
        gameObject.SetActive(false);
    }


    protected IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        moveSpeed = 5f;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    protected IEnumerator MoveToPlayer()
    {
        moveSpeed = 12f;
        while (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > 0.1f)
        {
            // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
