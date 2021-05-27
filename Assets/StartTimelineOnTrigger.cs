using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimelineOnTrigger : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.tag == "Player")
            timeline.Play();
    }
}
