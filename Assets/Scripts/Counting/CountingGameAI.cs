using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountingGameAI : CountingGamePlayer
{
    private List<Dispenser> dispensersToCount;
    private List<int> lastCount;

    private int percentToCount;

    public CountingGameAI()
    {
        dispensersToCount = new List<Dispenser>();
        lastCount = new List<int>();

        percentToCount = 90;
    }

    public override void Update()
    {
        for(int i = 0; i < dispensersToCount.Count; ++i)
        {
            int count = dispensersToCount[i].GetCount();
            if(count != lastCount[i])
            {
                CountObject();
                lastCount[i] = count;
            }
        }
    }

    public override void StartNewRound()
    {
        counter = 0;

        dispensersToCount.Clear();
        foreach(int i in CountingGameManager.instance.objectsToCount)
        {
            dispensersToCount.Add(CountingGameManager.instance.dispenserList[i].GetComponent<Dispenser>());
        }

        lastCount.Clear();
        for(int i = 0; i < dispensersToCount.Count; ++i)
        {
            lastCount.Add(0);
        }
    }

    private void CountObject()
    {
        int chance = Random.Range(1, 100);

        if(chance <= percentToCount)
        {
            counter++;
        }
    }
}
