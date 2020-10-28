using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class POI
{
    public GameObject myObject;
    public float myDistance;
    public POI(GameObject POI, float distance)
    {
        myObject = POI;
        myDistance = distance;
    }

}
