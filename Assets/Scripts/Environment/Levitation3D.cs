using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation3D : MonoBehaviour
{
    [SerializeField] float minRotationDelta = 3f;
    [SerializeField] float maxRotationDelta = 3f;
    private int fixedXRotation;
    [SerializeField] float ChangeDirectionTimer = 2f;
    private float timeLeft;
    [SerializeField] bool changeDirAbruptly = true;

    float rotationX;
    float rotationY;
    float rotationZ;

    private void Start()
    {
        // Rotation along x axis will be fixed (the object looks to be either coming or going)
        fixedXRotation = (int)Mathf.Sign(Random.Range(-1f, 1f));
        timeLeft = ChangeDirectionTimer;
        rotationX = Random.Range(0f, maxRotationDelta);
        rotationY = Random.Range(-minRotationDelta, maxRotationDelta);
        rotationZ = Random.Range(-minRotationDelta, maxRotationDelta);
    }

    void Update()
    {
        if (changeDirAbruptly)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                //rotationX = Random.Range(0f, maxRotationDelta);
                rotationY = Random.Range(-minRotationDelta, maxRotationDelta);
                rotationZ = Random.Range(-minRotationDelta, maxRotationDelta);
                timeLeft = 2f;
            }
        }

        transform.Rotate(fixedXRotation * rotationX, rotationY, rotationZ);
    }
}
