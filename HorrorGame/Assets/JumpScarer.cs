using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarer : MonoBehaviour {
    private int gameState;
    public GameObject target;
    public AudioSource jumpScareSound;

    float lastPlayed;
    Renderer targetRenderer;

	// Use this for initialization
	void Start () {
        gameState = 0;
        lastPlayed = -5;
        targetRenderer = target.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if( flashLightOnTarget() && Time.time - lastPlayed > 5) 
        {
            jumpScareSound.Play();
            lastPlayed = Time.time;
        }
	}

    void UpdateState(int newState) {
        gameState = newState;
    }

    bool flashLightOnTarget()
    {
        Vector3 rayDirection = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.up, rayDirection);
        if(angle < 25 && targetRenderer.isVisible)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, rayDirection, out hit, 70))
            {
                if(hit.collider.gameObject == target)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
