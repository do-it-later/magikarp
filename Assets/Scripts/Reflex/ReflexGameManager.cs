using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflexGameManager : MonoBehaviour {

    public List<GameObject> targetObjs;
    public List<Sprite> sprites;
    public List<GameObject> reflexPlayerObjs;

    private int numRounds = 10;
    private List<string> roundTargets;
    private List<ReflexPlayer> reflexPlayers = new List<ReflexPlayer>();

    private float roundStartTime;

    // Use this for initialization
    void Start()
    {
        roundTargets = new List<string> { "B", "Y", "X", "A" };

        foreach(var player in reflexPlayerObjs)
        {
            reflexPlayers.Add( player.GetComponent<ReflexPlayer>() );
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

            var randomTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(randomTime);

            StartRound();

            yield return new WaitForSeconds(5.0f);

            Debug.Log("Round " + currentRound.ToString() + " end.");

            EndRound();

            yield return new WaitForSeconds(3.0f);

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

        foreach (var player in reflexPlayers)
        {
            player.CanPressButton = true;
        }

        roundStartTime = Time.time;
        Debug.Log(roundStartTime.ToString());
    }

    private void EndRound()
    {
        float fastestTime = 9999;
        ReflexPlayer fastestPlayer = null;

        foreach (var player in reflexPlayers)
        {
            player.CanPressButton = false;
        }

        foreach (var obj in targetObjs)
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            sr.enabled = false;
        }

        foreach (var player in reflexPlayers)
        {
            if (player.ButtonPressedString == player.Target)
            {
                var timeTaken = player.TimePressed - roundStartTime;
                if ( timeTaken < fastestTime )
                {
                    fastestPlayer = player;
                    fastestTime = timeTaken;
                }
            }    
        }

        if (fastestPlayer != null)
        {
            Debug.Log("Fastest Player: " + fastestPlayer.controllerNumber.ToString() + " time: " + fastestTime + "s");
        }
        // check for correct answer
        // check for time if correct
        // check if faster than fastest time

    }

    private void SetupNewRound()
    {
        reflexPlayers.ShuffleList();

        for(int i = 0; i < 4; ++i)
        {
            // Set the sprite at the correct place
            var obj = targetObjs[i];
            var sr = obj.GetComponent<SpriteRenderer>();
            sr.sprite = reflexPlayers[i].playerSprite;
            sr.enabled = false;

            // Set the target for the player
            reflexPlayers[i].Target = roundTargets[i];
            reflexPlayers[i].ResetButtons();
        }
    }
}
