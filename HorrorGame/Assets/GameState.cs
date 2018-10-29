using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public Text stateText;
    public Text slenderText;

    public Text notesText;

    public AudioSource noteSound;
    public AudioSource stage1Music;

    public int gameState;
    public int notesCollected;

    public GameObject slender;
    private Renderer slenderRenderer;
    private bool isSlenderVisible;
    private float lastSlenderVisibleTime;
    private float foundNoteTime;

	// Use this for initialization
	void Start () {
        gameState = 0;
        stateText.text = "State: " + gameState;
        slenderText.text = "Slenderman not visible";
        lastSlenderVisibleTime = 0.0f;
        isSlenderVisible = false;
        slenderRenderer = slender.GetComponent<Renderer>();
        foundNoteTime = -10;
	}
	
	// Update is called once per frame
	void Update () {
        checkSlenderVisible();
        HandleUI();
	}

    void advanceState()
    {
        if(gameState < 5)
        {
            gameState++;
            if (gameState == 1)
            {
                stage1Music.Play();
            }
            stateText.text = "State: " + gameState;
        }
    }

    void checkSlenderVisible()
    {
        if(!isSlenderVisible && slenderRenderer.isVisible)
        {
            slenderText.text = "Slenderman visible";
            isSlenderVisible = true;
            lastSlenderVisibleTime = Time.time;
        } else if(isSlenderVisible && !slenderRenderer.isVisible)
        {
            isSlenderVisible = false;
            slenderText.text = "Slenderman not visible";
        }
    }

    void NoteFound()
    {
        noteSound.Play();
        notesCollected++;
        notesText.text = "Notes found: " + notesCollected + "/8";
        notesText.enabled = true;
        foundNoteTime = Time.time;

        if(notesCollected == 1 || notesCollected == 3 || notesCollected == 5 || notesCollected == 7 || notesCollected == 8)
        {
            advanceState();
        }
    }

    void HandleUI()
    {
        if(Time.time - foundNoteTime > 5)
        {
            notesText.enabled = false;
        }
    }
}
