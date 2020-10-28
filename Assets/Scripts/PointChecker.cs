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

    
    //criar dicionario targets - positions. Calcular o mais perto (maybe another script). Return o mais perto. 
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
        if(other.gameObject.tag == "PointOfInterest")
        {
            //Use this to update the distances
            trig = true;
            targetPos = other.transform.position;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "PointOfInterest")
        {
            POIToAdd = new POI(other.gameObject, CalculateDistance(other.gameObject));
            Debug.Log(POIs);
            POIs.Add(POIToAdd);
            Debug.Log(POIs);
            trig = true;
            targetPos = other.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Check if list is bigger than 1
        //If it is check if other.gameObject == POI.gameObject
        //If it is take THAT POI out of the list
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
}
