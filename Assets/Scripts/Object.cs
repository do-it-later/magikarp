﻿using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Vector3 position = transform.position;

        position.x -= speed * Time.deltaTime;

        transform.position = position;

        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(viewPosition.x < -0.5f)
            ObjectPool.instance.PoolObject(gameObject);
    }
}
