using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{
    public float speed;
    public float shieldLevel;

    public GameObject shield;

    public int playerNumber;
    private bool dead;
    private float vertExtent;
    private float horzExtent;

    void Start()
    {
        dead = false;

        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
    }

    void Update()
    {
        if(shieldLevel == 0)
        {
            shield.SetActive(false);
            GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
        }

        if(dead)
        {
            gameObject.SetActive(false);
        }

        float horizontal = InputHelper.instance.GetHorizForController(playerNumber);
        float vertical = InputHelper.instance.GetVertForController(playerNumber);
        Vector2 direction = new Vector2(horizontal, vertical);
        
        if(direction.SqrMagnitude() > 0.5f)
        {
            direction.Normalize();

            if(!dead)
            {
                Vector2 position = transform.position;

                position += direction * speed * Time.deltaTime;

                if(position.x < -horzExtent)
                    position.x = -horzExtent;
                else if(position.x > horzExtent)
                    position.x = horzExtent;

                if(position.y < -vertExtent)
                    position.y = -vertExtent;
                else if(position.y > vertExtent)
                    position.y = vertExtent;

                transform.position = position;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null)
        {
            if(collider.gameObject.name.Contains("Asteroid"))
            {
                if(shieldLevel <= 0)
                {
                    dead = true;
                }
                else
                {
                    shieldLevel--;
                }

                ObjectPool.instance.PoolObject(collider.gameObject);
            }
        }
    }

    public bool IsDead()
    {
        return dead;
    }
}
