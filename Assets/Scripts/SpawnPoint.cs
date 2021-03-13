using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    //this is a test script for now!!!!!
    [SerializeField] Slime slimeToSpawn;

    [SerializeField] public List<Transform> path = new List<Transform>();

    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(slimeToSpawn, transform.position, Quaternion.identity).ReceivePath(path);
    }

    
}
