using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDir : MonoBehaviour
{
    Vector2 dir;
    Vector3 rot;

    void Start()
    {

    }

    void Update()
    {
        dir = GameManager.Instance.player.GetComponent<Player>().inputVec;
        rot.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = rot;
    }
}

