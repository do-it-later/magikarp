using UnityEngine;
using System.Collections;

public class Dispenser : MonoBehaviour
{
    public string objectName;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public float minSpeed;
    public float maxSpeed;
    public float minTime;
    public float maxTime;
    
    protected int count;

    public void StartDispenser()
    {
        StartCoroutine("Dispense");
    }

    public void StopDispenser()
    {
        StopCoroutine("Dispense");
    }

    public int GetCount()
    {
        return count;
    }

    public void ClearCount()
    {
        count = 0;
    }

    protected virtual IEnumerator Dispense()
    {
        while(true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            GameObject dispensedObject = ObjectPool.instance.GetObject(objectName, false);
            Vector2 position = new Vector2(Random.Range(bottomRight.x, topLeft.x), Random.Range(bottomRight.y, topLeft.y));
            dispensedObject.transform.position = position;
            dispensedObject.GetComponent<BaseObject>().speed = Random.Range(minSpeed, maxSpeed);
            count++;
        }
    }
}
