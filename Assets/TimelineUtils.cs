using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineUtils : MonoBehaviour
{
    private PlayableDirector timeline;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    public void StopOnFinish()
    {
        timeline.Stop();
    }

    public void DeactivateOnFinish()
    {
        gameObject.SetActive(false);
    }
}
