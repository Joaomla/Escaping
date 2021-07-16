using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Starts timeline when triggered.
/// Can be deactivated so that it's not retriggered.
/// </summary>

public class StartTimelineOnTrigger : MonoBehaviour
{
    public PlayableDirector timeline; // Timeline that is activated
    [SerializeField] bool deactivateTrigger = true; // flag that doesn't let the trigger reactivate
    [SerializeField] string agentTag = "Player"; // tag of the object that should trigger the timeline

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.tag == agentTag)
        {
            timeline.Play();

            if (deactivateTrigger)
                gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
