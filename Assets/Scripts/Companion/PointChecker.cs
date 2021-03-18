using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public bool trig = false;
    public Vector3 targetPos;

    POI POIToAdd;
    List<POI> POIs = new List<POI>();

    [SerializeField] GameObject player = null;

    // active point of interest in which the companion is focused on
    public POI activePOI;

     
    private void Start() 
    {
        List<POI> POIs = new List<POI>();    
    }
    
    
    //collisions check
    public bool CheckTrig()
    {
        return trig;
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        // the other object is a point of interest
        if(other.GetComponent<POI>() != null)
        {
            if(POIs.Count > 1)
            {
                // calculates the distance 
                for(int i = 0; i < POIs.Count; i++)
                {
                    POIs[i].myDistance = CalculateDistance(POIs[i]);
                    //Debug.Log(POIs[i].name + " " + POIs[i].myDistance);
                }
            }

            // updates the target POI
            trig = true;
            activePOI = TargetGiver(POIs);
            targetPos = activePOI.transform.position;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // the other gameobject has a Point of interest -> adds to the list
        if(other.GetComponent<POI>() != null)
        {
            POIToAdd = other.GetComponent<POI>();
            POIs.Add(POIToAdd);


            // updates the target POI
            trig = true;
            activePOI = TargetGiver(POIs);
            targetPos = activePOI.transform.position;     
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Check if list is bigger than 1
        //If it is check if other.gameObject == POI.gameObject
        //If it is take THAT POI out of the list
        POI otherPOI = other.GetComponent<POI>();

        if (otherPOI != null)
        {
            for(int i = 0; i < POIs.Count; i++)
            {
                if(POIs[i] == otherPOI)
                {
                    // if this is the currently active POI remove it
                    if (POIs[i] == activePOI) activePOI = null;

                    // remove from the list
                    POIs.Remove(POIs[i]);
                    trig = false;
                }
            }
        }
    }

    float CalculateDistance(POI poi)
    {
        float distance = Vector3.Distance(player.transform.position, poi.transform.position);
        return distance;
    }

    POI TargetGiver(List<POI> POIs)
    {
        // the first element of the list is the default POI
        POI POIToReturn = POIs[0];
        float distance = POIToReturn.myDistance;

        // Checks which POI has the less distance
        for(int i = 1; i < POIs.Count; i++)
        {
            if(distance > POIs[i].myDistance)
            {
                POIToReturn = POIs[i];
                distance = POIToReturn.myDistance;
            }
        }

        return POIToReturn;
    }
}
