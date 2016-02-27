using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float gameLength;
    public List<GameObject> dispensers;

    void Start()
    {
        foreach(GameObject obj in dispensers)
        {
            Dispenser dispenser = obj.GetComponent<Dispenser>();

            dispenser.objectName = "Object";
            dispenser.topLeft = new Vector2(8, -4.0f);
            dispenser.bottomRight = new Vector2(8, 0.5f);
            dispenser.minSpeed = 3.0f;
            dispenser.maxSpeed = 7.0f;
            dispenser.minTime = 0.5f;
            dispenser.maxTime = 1.5f;
        }

        StartCoroutine(Countdown.Instance.StartCountdown(Play));
    }

    IEnumerator Play()
    {
        dispensers[0].GetComponent<Dispenser>().StartDispenser();

        yield return new WaitForSeconds(gameLength);

        dispensers[0].GetComponent<Dispenser>().StopDispenser();

        Debug.Log(dispensers[0].GetComponent<Dispenser>().GetCount());
    }
}
