using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointConfig : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    private void Start() {
        Instantiate(enemyPrefab);
    }

    public List<Transform> GetWaypoints()
    {
        var trajectoriePoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            trajectoriePoints.Add(child);
            Debug.Log(trajectoriePoints.Count);
        }
        return trajectoriePoints;
    }

    //public float GetMoveSpeed(){return moveSpeed;}
    public GameObject GetMyEnemy(){return enemyPrefab;}
}
