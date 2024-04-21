using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    
    public RuntimeAnimatorController[] animCon;

    [Header("# Player Face")]
    public SpriteRenderer playerEyes;
    public Sprite eyes_0;
    public Sprite eyes_1;
    public Sprite eyes_2;
    public Sprite eyes_3;
    public Sprite eyes_4;

    [Header("# Weapons")]
    public GameObject[] Weapons;

    Rigidbody2D rigid;
    Animator animator;
    Transform pivot;
    public CircleCollider2D goldCollider;

    public bool isInvincible; // ����

    [Header("# Effect")]
    public GameObject DieEffect;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        pivot = transform.GetChild(0).GetComponent<Transform>();
        goldCollider = transform.GetChild(1).GetComponent<CircleCollider2D>();
        scanner = GetComponent<Scanner>();

        //isInvincible = false;
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;        
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    }
    void Start()
    {
        Weapons[1].GetComponent<Weapon>().itemData.level = 1; // �ʱ⹫�� ���� ����
        animator.SetBool("Dead", false);

    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        //Debug.Log("inputVec : " + inputVec);

        if (!GameManager.Instance.isLive)
            return;

        animator.SetFloat("Speed", inputVec.magnitude);        
        if (inputVec.x < 0)
        {
            pivot.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            pivot.localEulerAngles = new Vector3(0, 0, 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive || collision.gameObject.tag != "Enemy" || isInvincible)
            return;

        Hit();

    }

    public void Hit()
    {

        // �浹 �����ð�
        StartCoroutine(InvincibleTime());

        // �ǰ� �ִϸ��̼�
        animator.SetTrigger("Hit");

        // ü�� ����
        GameManager.Instance.health--;

        // �÷��̾� ���� ���� UI �ٲٱ�
        UpdatePlayerEyes();

        // ���
        if (GameManager.Instance.health < 1)
        {
            SetAttackAnim(false);
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;

            // �״� ����Ʈ ����
            Instantiate(DieEffect, transform);

            GameManager.Instance.GameOver();
        }
    }

    public void SetAttackAnim(bool isAttack)
    {
        animator.SetBool("Attack", isAttack);
    }


    IEnumerator InvincibleTime()
    {
        isInvincible = true;

        yield return new WaitForSeconds(1.5f); //  �����ð�

        isInvincible = false;
    }


    public void UpdatePlayerEyes()
    {
        switch (GameManager.Instance.health)
        {
            case 0:
                playerEyes.sprite = eyes_0;
                break;
            case 1:
                playerEyes.sprite = eyes_1;
                break;
            case 2:
                playerEyes.sprite = eyes_2;
                break;
            case 3:
                playerEyes.sprite = eyes_3;
                break;
            case 4:
                playerEyes.sprite = eyes_4;
                break;
            default:
                break;
        }
    }
}
