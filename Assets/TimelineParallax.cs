using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineParallax : MonoBehaviour
{
    public Camera camera;
    public Transform agent;
    public PlayableDirector director;
    public float extraVelocity = 2f;

    Vector2 startPosition;
    float startZ;
    float startY;
    Vector3 velocity;
    Vector3 currentDirectorPosition;

    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromAgent => transform.position.z - agent.position.z;

    float clippingPlane => (camera.transform.position.z + (distanceFromAgent > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromAgent) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = agent.position;
        currentDirectorPosition = director.transform.position;
    }

    private void Update()
    {
        Vector2 velocity = currentDirectorPosition - director.transform.position;
        currentDirectorPosition = director.transform.position;
        Vector2 currentDirectorPosition2D = currentDirectorPosition;
        Vector2 newPos = currentDirectorPosition2D + velocity * extraVelocity + travel * parallaxFactor;
        transform.position = new Vector2(newPos.x, currentDirectorPosition.y);
    }

    /*
    public Camera camera;
    public Transform agent;
    public PlayableDirector timeline;

    Vector2 startPosition;

    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromAgent => transform.position.z - agent.position.z;

    float clippingPlane => (camera.transform.position.z + (distanceFromAgent > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromAgent) / clippingPlane;

    private void Update()
    {
        startPosition = timeline.transform.position;
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, timeline.transform.position.y, timeline.transform.position.z);
    }
    */
}
