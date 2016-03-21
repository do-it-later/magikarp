using UnityEngine;
using System.Collections;

public class AsteroidDispenser : Dispenser
{
    protected override IEnumerator Dispense()
    {
        while(true)
        {
            Vector2 velocity = new Vector2(Random.Range(topLeft.x / 2, bottomRight.x / 2), -5);
            velocity.Normalize();

            float scale = Random.Range(0.5f, 2.0f);

            GameObject dispensedObject = ObjectPool.instance.GetObject(objectName, false);
            dispensedObject.transform.position = new Vector2(Random.Range(bottomRight.x, topLeft.x), Random.Range(bottomRight.y, topLeft.y));
            dispensedObject.transform.localScale = new Vector3(scale, scale, 1);
            dispensedObject.GetComponent<Rigidbody2D>().mass = scale * 100;
            dispensedObject.GetComponent<Rigidbody2D>().velocity = velocity * (2 - scale) * 5;

            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
