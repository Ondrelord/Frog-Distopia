using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBarEvent : EventArgs
{
    float percentage;

    public GUIBarEvent(float percentage)
    {
        this.percentage = percentage;
    }

    public float GetPercentage() => percentage;
}


public class GUIBar : MonoBehaviour
{
    [SerializeField] Image Filler = null;

    public EventHandler Get_OnValueChanged() => OnValueChanged;

    private void OnValueChanged(object sender, EventArgs e)
    {
        SetFillerBar(((GUIBarEvent)e).GetPercentage());
    }

    public void SetFillerBar(float current, float max) => SetFillerBar(current / max);
    public void SetFillerBar(float percerntage) => Filler.fillAmount = Mathf.Clamp(percerntage, 0f, 1f);
    
}
