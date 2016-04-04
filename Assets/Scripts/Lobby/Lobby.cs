using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Lobby : MonoBehaviour {

    public List<Text> typeTexts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int p = 1; p <= 4; ++p)
        {
            if (Input.GetKeyDown(InputHelper.instance.DidControllerButtonGetPressed(p, InputHelper.Button.A)))
            {
                PlayerManager.instance.SetHumanPlayer(p);
            }

            if (Input.GetKeyDown(InputHelper.instance.DidControllerButtonGetPressed(p, InputHelper.Button.B)))
            {
                PlayerManager.instance.UnsetHumanPlayer(p);
            }

            if( PlayerManager.instance.FindPlayer(p).IsHuman )
            {
                typeTexts[p - 1].text = "PLAYER";
            }
            else
            {
                typeTexts[p - 1].text = "AI";
            }
        }
    }
}
