using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image Filler = null;

    public void Setup(HealthSystem healthSystem)
    {
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        SetHealthBar( ((HealthSystem) sender).GetHealthPercentage());
    }

    public void SetHealthBar(float current, float max) => SetHealthBar(current / max);
    public void SetHealthBar(float percerntage) => Filler.fillAmount = Mathf.Clamp(percerntage, 0f, 1f);
    
}
