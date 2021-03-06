﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // player
    private Player player;

    // the side this dude is facing. 1: right, -1: left
    int companionFacing = 1;

    // comportamento do monstro será programado através de state machine (try)
    public Vector3 myOriginalPosition;
    
    public StateMachine stateMachine = new StateMachine();

    [SerializeField] PointChecker pointChecker = null;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        stateMachine.ChangeState(new Searching(this));
        myOriginalPosition = this.transform.localPosition;
        //pointChecker = FindObjectOfType<PointChecker>();
    }

    void Update()
    {
        stateMachine.Update();

        FacingPlayer();
    }


    //from this point onwards this is collision checks for points of interest. Need to change to accomodate several points
    public bool CheckTrig()
    {
        return pointChecker.trig;
    }

    public Vector3 CheckTargetPos()
    {
        return pointChecker.targetPos;
    }

    private void FacingPlayer()
    {
        // companion should be facing the player
        if(Mathf.Sign(player.transform.position.x - transform.position.x) != companionFacing)
        {
            companionFacing = -1 * companionFacing;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Checks if the player can teleport (the companion is focused on a teleportation point)
    public void CheckIfCanTeleport(out bool canTP, out Vector2 destination)
    {
        // the companion is focused on a POI
        if( pointChecker.activePOI != null)
        {
            TeleportPOI teleportPOI = pointChecker.activePOI.GetComponent<TeleportPOI>();
            // If the POI is a teleportation point
            if (teleportPOI)
            {
                // The player can teleport
                canTP = true;
                destination = teleportPOI.destination;
                return;
            }
        }

        // the player cannot teleport
        canTP = false;
        destination = Vector2.zero;
    }
    
}
