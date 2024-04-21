using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    public bool isLive;
    public float health;
    public float maxHealth;
    public float moveSpeed;

    public Rigidbody2D target;
    [Header("# Prefab")]
    public GameObject DangerTrail;
    public GameObject DieEffect;
    public GameObject HpBarPrefab;
    GameObject hpBar;


    Rigidbody2D rigid;
    Collider2D coll;
    Animator animator;
       
    float fireTimer;

    bool fire_A;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

    }

    private void OnEnable()
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        animator.SetBool("Dead", false);
        health = maxHealth;


        // ���� ü�¹� ����
        hpBar = Instantiate(HpBarPrefab, GameManager.Instance.uiManager.canvas.transform);
        hpBar.transform.SetAsFirstSibling();
        hpBar.GetComponent<RectTransform>().localPosition =
            Camera.main.WorldToScreenPoint(transform.position) - new Vector3(Screen.width / 2f, Screen.height / 2f + 140, 0);

    }

    void Start()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > 1.5f)
        {
            Fire_A();
            Fire_B();
            fireTimer = 0;
        }

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

        // ü�¹� ��ġ ������Ʈ
        hpBar.GetComponent<RectTransform>().localPosition =
            Camera.main.WorldToScreenPoint(transform.position) - new Vector3(Screen.width / 2f, Screen.height / 2f + 140, 0);
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        // ������ Text ����
        GameObject damageText = Instantiate(GameManager.Instance.damageText);
        damageText.transform.SetParent(transform);// �ڽ����� ����
        //damageText.transform.parent = transform; 
        damageText.transform.localScale = Vector3.one;
        damageText.transform.position = this.transform.position + new Vector3(0, 0.5f, 0); // ǥ�õ� ��ġ
        damageText.GetComponent<DamageText>().damage = collision.GetComponent<Bullet>().damage; // ������ ����

        health -= collision.GetComponent<Bullet>().damage;

        // ü�� Bar ������Ʈ
        hpBar.transform.GetChild(0).GetComponent<Image>().fillAmount = health / maxHealth;

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

            // ü�¹� ����
            Destroy(hpBar);

            StartCoroutine(DeadCoroutine());
            //Dead();
        }
    }

    void Fire_A()
    {
        int addDeg;
        if (fire_A)
        {
            addDeg = 0;
            fire_A = false;
        }
        else
        {
            addDeg = 18;
            fire_A = true;
        }

        // �������� �߻�
        for (int i = 0 + addDeg; i < 360; i += 36)
        {
            //�Ѿ� ����
            GameObject bullet = GameManager.Instance.pool.GetWeaponBullets(4);

            bullet.transform.position = transform.position;

            float angleInRadians = i * Mathf.Deg2Rad; // ������ �������� ��ȯ
            Vector3 pointOnUnitCircle =
                new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0) + transform.position;

            bullet.GetComponent<EnemyBullet>().Init(1, 5f, (pointOnUnitCircle - transform.position).normalized); // ź�� 5f
        }

    }
    void Fire_B()
    {
        GameObject trail = Instantiate(DangerTrail);
        trail.transform.position = transform.position;
        trail.GetComponent<Rigidbody2D>().velocity = (target.position - rigid.position).normalized * 30f; // trail �߻� �ӵ�
        Destroy(trail, 1f);
        StartCoroutine(Fire_B_Coroutine((target.position - rigid.position).normalized));
    }


    IEnumerator Fire_B_Coroutine(Vector2 _dir)
    {
        yield return new WaitForSeconds(1);

        //�Ѿ� ����
        GameObject bullet = GameManager.Instance.pool.GetWeaponBullets(5);
        bullet.transform.position = transform.position;
        bullet.transform.localRotation = Quaternion.LookRotation(Vector3.forward, _dir);
        bullet.GetComponent<EnemyBullet>().Init(1, 15f, _dir); // ź�� 15f
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
    void Dead()
    {
        GameManager.Instance.GameClear();
        gameObject.SetActive(false);
    }

}
