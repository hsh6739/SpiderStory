using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage; // 데미지
    public float bulletSpeed; // 탄속

    //public Vector3 targetDir;
    public Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
    }

    public void Init(int _damage, float _bulletSpeed, Vector3 _targetDir = default)
    {
        if (_targetDir == default)
            _targetDir = (GameManager.Instance.player.transform.position - transform.position).normalized;

        damage = _damage;
        bulletSpeed = _bulletSpeed;
        rigid.velocity = _targetDir * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.isLive || GameManager.Instance.player.GetComponent<Player>().isInvincible)
            return;

        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<Player>().Hit();

            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    // 총알 멀리가면 사라지게 해줌
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerArea"))
            return;

        rigid.velocity = Vector2.zero;
        gameObject.SetActive(false);

    }

}
