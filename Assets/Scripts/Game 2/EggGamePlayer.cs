using UnityEngine;
using System.Collections;

public class EggGamePlayer : MonoBehaviour {

    public int score;
    private Vector2 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(initialPos.x - 1, initialPos.y);
        }
        else if( Input.GetKey(KeyCode.RightArrow) )
        {
            transform.position = new Vector2(initialPos.x + 1, initialPos.y);
        }
        else
        {
            transform.position = initialPos;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var coll = collision.gameObject;
        var objScript = coll.GetComponent<EggGameObject>();

        if ( objScript.objectName == "Egg" )
        {
            score++;
        }
        else if (objScript.objectName == "Bomb" )
        {
            StartCoroutine("BecomeInvincible");
        }

        ObjectPool.instance.PoolObject(coll);
    }

    private IEnumerator BecomeInvincible()
    {
        Renderer r = gameObject.GetComponent<Renderer>();
        BoxCollider2D c = gameObject.GetComponent<BoxCollider2D>();

        c.enabled = false;

        for (int i = 0; i < 5; ++i)
        {
            r.enabled = !r.enabled;

            yield return new WaitForSeconds(0.1f);
        }

        r.enabled = true;
        c.enabled = true;
    }
}
