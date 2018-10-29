using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Note : MonoBehaviour {

    public GameObject gamePlayController;
    private Interactable interactable;

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("getting here begin");
        gamePlayController.SendMessage("NoteFound");
        Destroy(this.gameObject);
    }
}
