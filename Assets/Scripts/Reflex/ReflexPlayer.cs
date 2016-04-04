using UnityEngine;
using System.Collections;

public class ReflexPlayer : MonoBehaviour {

    public int score;
    public int controllerNumber;
    public Sprite playerSprite;

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

    private float timePressed = 0;
    public float TimePressed { get { return timePressed; } }

    private string target;
    public string Target
    {
        get { return target; }
        set { target = value; }
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
                SetButtonPressed("X");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                SetButtonPressed("Y");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SetButtonPressed("A");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SetButtonPressed("B");
            }
        }
	}

    public void ResetButtons()
    {
        wasButtonPressed = false;
        buttonPressedString = "";
        timePressed = 9999;
    }

    private void SetButtonPressed(string key)
    {
        wasButtonPressed = true;
        buttonPressedString = key;
        timePressed = Time.time;
        Debug.Log("Pressed");
    }
}
