using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // ������
    public int per; // �����
    public float bulletSpeed; // ź��

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
        }
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir * bulletSpeed;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���Ϳ͸� �浹, ���Ÿ� ����ü�϶���(�Ѿ�)
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
    
        per--;
    
        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false); // ������Ʈ Ǯ�� ������ destroy X
        }
    }

    // �Ѿ� �ָ����� ������� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerArea") || per == -100)
            return;

        gameObject.SetActive(false);

    }

}
