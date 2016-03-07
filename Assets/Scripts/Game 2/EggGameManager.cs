using UnityEngine;
using System.Collections;

public class EggGameManager : MonoBehaviour {

    public GameObject dispenserObj;
    private EggDispenser dispenser;

    private bool started;

	// Use this for initialization
	void Start () {
        dispenser = dispenserObj.GetComponent<EggDispenser>();
        started = false;

        StartCoroutine(Countdown.Instance.StartCountdown(Play));
	}

    IEnumerator Play()
    {
        if (!started)
        {
            dispenser.StartDispenser();
            started = true;
        }

        while ( dispenser.EggsRemaining > 0 )
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
