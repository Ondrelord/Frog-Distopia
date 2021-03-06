﻿using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    float health;
    float healthMax;

    public HealthSystem(float max, GUIBar bar)
    {
        health = healthMax = max;
        OnHealthChanged += bar.Get_OnValueChanged();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0f;
        OnHealthChanged?.Invoke(this, new GUIBarEvent(GetHealthPercentage()));
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > healthMax) health = healthMax;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealth() => health;
    public float GetHealthPercentage() => health / healthMax;
}
