using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionBehavior : MonoBehaviour
{
    
    [SerializeField] GameObject companion;
    [SerializeField] GameObject player;
    LineRenderer lineRenderer;

    Vector3 companionPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent(typeof(LineRenderer)) as LineRenderer;
        companionPos = companion.transform.position;
        playerPos = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetPositions();
        lineRenderer.SetPosition(0, new Vector3(playerPos.x, playerPos.y, playerPos.z));
        lineRenderer.SetPosition(1, new Vector3(companionPos.x, companionPos.y, companionPos.z));
    }


    public void GetPositions()
    {
        companionPos = companion.transform.position;
        playerPos = player.transform.position;
    }
}
