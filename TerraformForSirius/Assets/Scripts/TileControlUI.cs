using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TileControlUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private DynamicResources dynamicResources;
    [SerializeField] private InGameCanvasScript _canvasScript;
    private int _previousSliderValue;
    private bool _forcedToMoveSlide;

    private void OnEnable()
    {
        EventManager.AdjustTileControlUI += AdjustUI;
    }

    private void OnDisable()
    {
        EventManager.AdjustTileControlUI -= AdjustUI;
    }
    

    public int GetPreviousSliderValue()
    {
        return _previousSliderValue;
    }

    private void AdjustUI(Resources currentResources)
    {
        _forcedToMoveSlide = true;
        _previousSliderValue = 0;
        if (dynamicResources == DynamicResources.Population)
        {
            slider.maxValue = currentResources.Population;
            slider.value = 0;
        }
        else if(dynamicResources == DynamicResources.Kadir)
        {
            slider.maxValue = currentResources.Kadir;
            slider.value = 0;
        }
        _forcedToMoveSlide = false;
        EventManager.OnResourceUpdated();
    }

    public void OnSliderMoved()
    {
        if (_forcedToMoveSlide)
        {
            return;
        }

        RoundTracker roundTracker = FindObjectOfType<RoundTracker>();

        if (dynamicResources==DynamicResources.Kadir)
        {
            if (roundTracker.GetAvailableKadir()<=0 && slider.value>_previousSliderValue)
            {
                _forcedToMoveSlide = true;
                slider.value = _previousSliderValue;
                _forcedToMoveSlide = false;
                EventManager.OnResourceUpdated();
                return;
            }
        }
        else if(dynamicResources == DynamicResources.Population)
        {
            if (roundTracker.GetAvailablePopulation()<=0 && slider.value>_previousSliderValue)
            {
                _forcedToMoveSlide = true;
                slider.value = _previousSliderValue;
                _forcedToMoveSlide = false;
                EventManager.OnResourceUpdated();
                return;
            }
        }
        

        if (dynamicResources==DynamicResources.Population)
        {
            EventManager.OnAvailablePopulationChanged(-((int)slider.value-_previousSliderValue));
        }
        else if(dynamicResources==DynamicResources.Kadir)
        {
            EventManager.OnAvailableKadirChanges(-((int)slider.value-_previousSliderValue));
        }

        _previousSliderValue = (int) slider.value;
        EventManager.OnResourceUpdated();
    }
}

internal enum DynamicResources
{
    Population,
    Kadir
}
