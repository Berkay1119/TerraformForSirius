using System;
using System.Collections.Generic;
using UnityEngine;
public class RoundTracker:MonoBehaviour
{
    private Resources currentResources;
    private int roundCount;

    private void OnEnable()
    {
        EventManager.ConsumeResource += ConsumeResources;
    }

    private void OnDisable()
    {
        EventManager.ConsumeResource -= ConsumeResources;
    }

    private void ConsumeResources(Resources resources)
    {
        currentResources.Consume(resources);
    }
    
}
