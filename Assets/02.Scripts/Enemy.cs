using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("# Effect")]
    public GameObject DieEffect;

    public bool isLive;
    public float health;
    public float maxHealth;

    public float speed;
    public RuntimeAnimatorController[] animCon;

    public Rigidbody2D target;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator animator;
    WaitForFixedUpdate wait;

    public float fireTimer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();

    }
    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }



    private void FixedUpdate()
    {
        if (!isLive)
            return;

        // Hit �ִϸ��̼� �����϶� �˹��� �ϱ����� ����
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }


        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        if (target.position.x < rigid.position.x)
        {
            transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.GetChild(0).localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Enemy_0:
                break;
            case EnemyType.Enemy_1:
                break;
            case EnemyType.Enemy_2:
                fireTimer += Time.deltaTime;
                if (fireTimer > 5f)
                {
                    Fire();
                    fireTimer = 0;
                }
                break;
            case EnemyType.Enemy_3:
                break;
            case EnemyType.Enemy_4:
                break;
            default:
                break;
        }
    }
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        // ������ Text ����
        GameObject damageText = Instantiate(GameManager.Instance.damageText);
        damageText.transform.SetParent(transform);// �ڽ����� ����
        //damageText.transform.parent = transform; 
        damageText.transform.localScale = Vector3.one;
        damageText.transform.position = this.transform.position + new Vector3(0, 0.5f, 0); // ǥ�õ� ��ġ
        damageText.GetComponent<DamageText>().damage = collision.GetComponent<Bullet>().damage; // ������ ����


        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // .. Live, Hit Action
            animator.SetTrigger("Hit");
        }
        else
        {
            // .. Die
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            animator.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();

            // �״� ����Ʈ ����
            GameObject _dieEffect = Instantiate(DieEffect);
            _dieEffect.transform.position = transform.position;

            StartCoroutine(DeadCoroutine());
            //Dead();
        }
    }


    IEnumerator KnockBack()
    {
        yield return wait; // ���� �ϳ��� ���� ������ ������

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // 3 �˹�Ÿ�
    }



    IEnumerator DeadCoroutine()
    {
        animator.SetBool("Dead", true);

        // ���� ����
        Transform coin = GameManager.Instance.pool.GetProbs(0).transform;
        coin.position = transform.position;

        yield return new WaitForSeconds(1);
        Dead();
    }

    void Fire()
    {
        GameObject bullet = GameManager.Instance.pool.GetWeaponBullets(4);
        bullet.transform.position = transform.position;
        //bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<EnemyBullet>().Init(1, 4f); // ź�� 4f
        fireTimer = 0;
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
