using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]

    [SerializeField] float healthPoints;
    [SerializeField] float wanderSpeed = 1.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWanderSpeed()
    {
        return wanderSpeed;
    }
}
