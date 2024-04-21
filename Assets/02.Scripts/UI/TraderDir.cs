using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDir : MonoBehaviour
{
    public Transform playerPos;
    public Transform traderPos;

    Vector2 dir;
    Vector3 rot;

    void Start()
    {
        playerPos = GameManager.Instance.player.transform;
        rot = Vector3.zero;
    }

    void Update()
    {
        dir = (traderPos.position - playerPos.position).normalized;
        rot.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = rot;

    }
}
