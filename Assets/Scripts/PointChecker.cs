using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public bool trig = false;
    public Vector3 targetPos;

    
    //criar dicionario targets - positions. Calcular o mais perto (maybe another script). Return o mais perto. 
    
    
    
    //collisions check
    public bool CheckTrig()
    {
        return trig;
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "PointOfInterest")
        {
            trig = true;
            targetPos = other.transform.position;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "PointOfInterest")
        {
            trig = true;
            targetPos = other.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "PointOfInterest")
        {
            trig = false;
        }
    }
}
