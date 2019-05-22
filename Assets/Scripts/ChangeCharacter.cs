using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour {

    public int selected = 0;
    public GameObject[] players;
    PlayerControl[] playerControls = new PlayerControl[4];
    CameraMove cameraMove;

    void Awake()
    {
        cameraMove = GetComponent<CameraMove>();
        selected = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Start ()
    {
        SetCharacter();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(playerControls[selected].EnergyCount() <= 0)
        {
            selected++;
            selected %= 4;
            SetCharacter();
        }
	}

    public void AttackState()
    {
        playerControls[selected] = players[selected].GetComponent<PlayerControl>();
        playerControls[selected].state = PlayerControl.State.Attacking;
    }

    void SetCharacter()
    {
        for (int i = 0; i < 4; i++)
        {
            playerControls[i] = players[i].GetComponent<PlayerControl>();
            if (i != selected)
            {
                playerControls[i].Controllable = false;
            }
            if (i == selected)
            {
                playerControls[i].Controllable = true;
                playerControls[i].SetEnergy();
                playerControls[selected].state = PlayerControl.State.Moving;
                cameraMove.SetTarget(players[i]);
            }
        }
    }
}
