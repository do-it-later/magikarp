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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if(col.tag == "Asteroid")
            {
            }
        }
    }
}
