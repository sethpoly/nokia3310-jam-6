using System;
using UnityEngine;

public class TouchControls : MonoBehaviour 
{
    public event Action<bool> OnRightEvent;
    public event Action<bool> OnLeftEvent;
    public event Action<bool> OnFloatEvent;


    public void OnRight()
    {
        OnRightEvent.Invoke(true);
    }

    public void OnLeft()
    {
        OnLeftEvent.Invoke(true);
    }

    public void OnFloat()
    {
        OnFloatEvent.Invoke(true);
    }

    public void OnRightUp()
    {
        OnRightEvent.Invoke(false);
    }

    public void OnLeftUp()
    {
        OnLeftEvent.Invoke(false);
    }

    public void OnFloatUp()
    {
        OnFloatEvent.Invoke(false);
    }
}