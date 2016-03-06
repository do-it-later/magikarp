using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float roundDuration;
    public float endGameDuration;
    public int numberOfRounds;
    
    public List<GameObject> dispenserList;
    public List<bool> activeDispenser;

    private int currentRound;

    private List<int> objectsToCount;

    void Start()
    {
        currentRound = 0;

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

        objectsToCount = new List<int>();

        StartCoroutine(Countdown.Instance.StartCountdown(Play));
    }

    IEnumerator Play()
    {
        objectsToCount.Clear();
        int count = 0;
        while(count <= currentRound)
        {
            int choice = Random.Range(0, dispenserList.Count);

            if(!objectsToCount.Contains(choice))
            {
                objectsToCount.Add(choice);
                ++count;
            }
        }

        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().ClearCount();
            dispenser.GetComponent<Dispenser>().StartDispenser();
        }

        yield return new WaitForSeconds(roundDuration);

        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().StopDispenser();
        }

        foreach(int i in objectsToCount)
        {
            Debug.Log(dispenserList[i].GetComponent<Dispenser>().GetCount());
        }

        currentRound++;

        yield return new WaitForSeconds(endGameDuration);

        if(currentRound < numberOfRounds)
            StartCoroutine(Countdown.Instance.StartCountdown(Play));
    }
}
