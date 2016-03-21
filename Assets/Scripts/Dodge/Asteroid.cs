using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.name == "Wall")
            {
                ObjectPool.instance.PoolObject(gameObject);
            }
        }
    }
}
