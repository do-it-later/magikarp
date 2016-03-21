using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflexGameManager : MonoBehaviour {

    private struct Target
    {
        public string letter;
        public Sprite sprite;
    }

    public List<GameObject> targetObjs;
    public List<Sprite> sprites;

    private int numRounds = 10;
    private List<Target> roundTargets = new List<Target>();

    // Use this for initialization
    void Start()
    {
        List<string> letters = new List<string> { "A", "B", "X", "Y" };
        for(int i = 0; i < letters.Count; ++i)
        {
            Target t;
            t.sprite = sprites[i];
            t.letter = letters[i];
            roundTargets.Add(t);
        }

        StartCoroutine(Countdown.instance.StartCountdown(Play));
    }

    IEnumerator Play()
    {
        int currentRound = 1;

        while (currentRound <= numRounds)
        {
            Debug.Log("Round " + currentRound.ToString());
            SetupNewRound();

            yield return new WaitForSeconds(2.0f);

            StartRound();

            yield return new WaitForSeconds(5.0f);

            currentRound++;
        }
    }

    private void StartRound()
    {
        foreach( var obj in targetObjs )
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            sr.enabled = true;
        }
    }

    private void SetupNewRound()
    {
        roundTargets.ShuffleList();

        for(int i = 0; i < roundTargets.Count; ++i)
        {
            var obj = targetObjs[i];
            var sr = obj.GetComponent<SpriteRenderer>();
            sr.sprite = roundTargets[i].sprite;
            sr.enabled = false;
        }
    }
}
