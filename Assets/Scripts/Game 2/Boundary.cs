using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectPool.instance.PoolObject(collision.gameObject);
    }
}
