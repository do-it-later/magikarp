using UnityEngine;
using System.Collections;

public class ReflexPlayer : MonoBehaviour {

    public int score;

    private bool canPressButton;
    public bool CanPressButton
    {
        get { return canPressButton; }
        set { canPressButton = value; }
    }

    private bool wasButtonPressed;
    private string buttonPressedString;
    public string ButtonPressedString
    {
        get { return buttonPressedString; }
    }

	// Use this for initialization
	void Start () {
        score = 0;
        ResetButtons();
	}
	
	// Update is called once per frame
	void Update () {
	    if( canPressButton && !wasButtonPressed )
        {
            //TODO: Proper mapping to controller
            if( Input.GetKeyDown(KeyCode.A) )
            {
                SetButtonPressed("A");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                SetButtonPressed("W");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SetButtonPressed("S");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SetButtonPressed("D");
            }
        }
	}

    public void ResetButtons()
    {
        wasButtonPressed = false;
        buttonPressedString = "";
    }

    private void SetButtonPressed(string key)
    {
        wasButtonPressed = true;
        buttonPressedString = key;
    }
}
