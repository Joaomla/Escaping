using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Camera camera;
    public Transform agent;

    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromAgent => transform.position.z - agent.position.z;

    float clippingPlane => (camera.transform.position.z + (distanceFromAgent > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromAgent) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
