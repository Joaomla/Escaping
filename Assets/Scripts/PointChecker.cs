using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public bool trig = false;
    public Vector3 targetPos;

    POI POIToAdd;
    List<POI> POIs = new List<POI>();

    [SerializeField] GameObject player;

     
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
        if(POIs.Count > 1)
        {
            for(int i = 0; i < POIs.Count; i++)
            {
                POIs[i].myDistance = CalculateDistance(other.gameObject);
            }
        }
        if(other.gameObject.tag == "PointOfInterest")
        {
            if(POIs.Count > 1)
            {
                Debug.Log("meu target é: " + TargetGiver(POIs).myObject.name);
                trig = true;
                targetPos = TargetGiver(POIs).myObject.transform.position;
            }
            else
            {
                trig = true;
                targetPos = other.transform.position;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "PointOfInterest")
        {
            POIToAdd = new POI(other.gameObject, CalculateDistance(other.gameObject));
            POIs.Add(POIToAdd);
            if(POIs.Count > 1)
            {
                targetPos = TargetGiver(POIs).myObject.transform.position;
            }
            else
            {
                trig = true;
                targetPos = other.transform.position;
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Check if list is bigger than 1
        //If it is check if other.gameObject == POI.gameObject
        //If it is take THAT POI out of the list
        for(int i = 0; i < POIs.Count; i++)
        {
            if(GameObject.ReferenceEquals(POIs[i].myObject, other.gameObject))
            {
                POIs.Remove(POIs[i]);
                Debug.Log(POIs.Count);
            }
        }
        
        if(other.gameObject.tag == "PointOfInterest")
        {
            trig = false;
        }
    }

    float CalculateDistance(GameObject POI)
    {
        float distance = Vector3.Distance(player.transform.position, POI.transform.position);
        return distance;
    }

    POI TargetGiver(List<POI> POIs)
    {
        POI POIToReturn = POIs[0];
        for(int i = 0; i<POIs.Count - 1; i++)
        {
            if(POIs[i].myDistance < POIs[i+1].myDistance)
            {
                POIToReturn = POIs[i];
            }
            else
            {
                POIToReturn = POIs[i+1];
            }
        }
        return POIToReturn;
    }
}
