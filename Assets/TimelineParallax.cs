using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineParallax : MonoBehaviour
{
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
}
