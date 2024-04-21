using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // 데미지
    public int per; // 관통력
    public float bulletSpeed; // 탄속

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
        // 몬스터와만 충돌, 원거리 투사체일때만(총알)
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
    
        per--;
    
        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false); // 오브젝트 풀링 때문에 destroy X
        }
    }

    // 총알 멀리가면 사라지게 해줌
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerArea") || per == -100)
            return;

        gameObject.SetActive(false);

    }

}
