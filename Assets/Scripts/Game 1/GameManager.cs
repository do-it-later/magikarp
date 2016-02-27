using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float gameLength;
    public List<GameObject> dispenserList;

    void Start()
    {
        for(int i = 0; i < dispenserList.Count; ++i)
        {
            Dispenser dispenser = dispenserList[i].GetComponent<Dispenser>();

            dispenser.objectName = "Object " + (i + 1).ToString();
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
        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().StartDispenser();
        }

        yield return new WaitForSeconds(gameLength);

        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().StopDispenser();
        }

        Debug.Log(dispenserList[0].GetComponent<Dispenser>().GetCount());
    }
}
