using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemBox : MonoBehaviour
{
    [Header("# Effect")]
    public GameObject destroyEffect;

    public float health;

    void Start()
    {
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health < 0)
        {
            // 이펙트 생성
            GameObject _destroyEffect = Instantiate(destroyEffect);
            _destroyEffect.transform.position = transform.position;
            InstantiateItem();
            gameObject.SetActive(false);
        }

    }


    // 아이템 랜덤 생성
    void InstantiateItem()
    {
        int ran = Random.Range(0, 3);

        GameObject dropItem = GameManager.Instance.pool.GetProbs(ran + 2);
        dropItem.transform.position = transform.position;

    }

}
