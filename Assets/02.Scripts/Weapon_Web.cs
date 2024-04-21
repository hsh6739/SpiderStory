using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Web : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag != "Enemy" || collision.gameObject.GetComponent<Enemy>() == null) 
            return;

        collision.gameObject.GetComponent<Enemy>().speed -= Time.deltaTime * 10f;
        if (collision.gameObject.GetComponent<Enemy>().speed < 1f)
        {
            collision.gameObject.GetComponent<Enemy>().speed = 1;
        }
        
    }

}
