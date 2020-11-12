using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletActiveArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            // Destroy(collision.gameObject);
            // collision.GetComponent<EnemyBullet>().IsAlive.Value = false;
            collision.GetComponent<EnemyBullet>().Dead();
        }
    }
}
