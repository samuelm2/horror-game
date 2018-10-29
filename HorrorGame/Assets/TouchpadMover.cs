using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TouchpadMover : MonoBehaviour {
    public Hand hand;
    public AudioSource[] stepSounds;
    public GameObject head;
    public float speed = 3f;

    private bool isWalking;
    private bool wasWalking;

    private SteamVR_Input_Sources inputSource;

    [SteamVR_DefaultActionSet("platformer")]
    public SteamVR_ActionSet actionSet;

    [SteamVR_DefaultAction("Move", "platformer")]
    public SteamVR_Action_Vector2 a_move;

    [SteamVR_DefaultAction("Jump", "platformer")]
    public SteamVR_Action_Boolean a_jump;

    public int index = 0;
    private float lastStepPlay;

    // Use this for initialization
    void Start () {
        actionSet.ActivatePrimary();
        lastStepPlay = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        inputSource = hand.handType;
        Vector3 moveDirection = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
        moveDirection.Normalize();

        Vector3 moveDirectionPerp = new Vector3(head.transform.right.x, 0, head.transform.right.z);
        moveDirectionPerp.Normalize();

        Vector2 move = a_move.GetAxis(inputSource);

        updateSounds(move.sqrMagnitude);

        Vector3 totalMoveDirection = (moveDirectionPerp * move.x + moveDirection * move.y) * Time.deltaTime * speed;

        transform.Translate(totalMoveDirection);
	}

    void updateSounds(float sqrMagnitude)
    {
        if(Time.time - lastStepPlay > (-.89 * sqrMagnitude + 1.553f) && sqrMagnitude > 0.01)
        {
            lastStepPlay = Time.time;
            index += 1;
            index = index % stepSounds.Length;
            stepSounds[index].Play();
        }
    }
}
