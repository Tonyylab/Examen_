using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;



public class PlayerStats : MonoBehaviour, IDamagable

{
    public Image FadeScreen;

    public Need health;
    public Need hunger;
    public Need thirst;

    public Image healthUiBar;
    public Image healthIcon;
    public Sprite healthIcon1,healthIcon2,healthIcon3,healthIcon4;
    public TextMeshProUGUI healthText,hungerText, thirstText;

    public float hungerHealthdecay;
    public float thirstHealthdecay;

    public UnityEvent onTakeDamage;

    public static PlayerStats instance;

    bool Dying = false;

    void Awake()
    {
        Dying = false;
        instance = this;
    }
    void Start()
    {
        health.currentValue = health.startValue;
        hunger.currentValue = hunger.startValue;
        thirst.currentValue = thirst.startValue;
    }
    void Update()
    {
        IconUpdate();
        hunger.Subtrack(hunger.decayRate * Time.deltaTime);
        thirst.Subtrack(thirst.decayRate * Time.deltaTime);

        if (hunger.currentValue <= 0.0f)
        {
            health.Subtrack(hungerHealthdecay * Time.deltaTime);
            hunger.currentValue = 0.0f;
        }

        if (thirst.currentValue <= 0.0f)
        {
            health.Subtrack(thirstHealthdecay * Time.deltaTime);
            thirst.currentValue = 0.0f;
        }

        if (health.currentValue <= 0.0f && Dying == false)
        {
            Dying = true;
            Die();
        }

        healthUiBar.fillAmount = health.GetPercentage();
        healthText.text = health.currentValue.ToString("0");
        hungerText.text = hunger.currentValue.ToString("0.0");
        thirstText.text = thirst.currentValue.ToString("0.0");
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Drink(float amount)
    {
        thirst.Add(amount);
    }
    public void TakePhysicDamage(int amount)
    {
        health.Subtrack(amount);
        AudioManager.instance.player.clip = AudioManager.instance.damageSFX;
        AudioManager.instance.player.Play();
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        AudioManager.instance.MuteAll();
        AudioManager.instance.Enviroment.mute = false;
        AudioManager.instance.Enviroment.clip = AudioManager.instance.GameOverSFX;
        AudioManager.instance.Enviroment.Play();
        FadeScreen.gameObject.SetActive(true);
        Invoke("returnToMenu",7.5f);
       
    }
    void returnToMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }

    void IconUpdate()
    {
        if(health.currentValue >= 75)
        {
            healthIcon.sprite = healthIcon1;
        }
        if (health.currentValue < 75 && health.currentValue >= 50)
        {
            healthIcon.sprite = healthIcon2;
        }
        if (health.currentValue < 50 && health.currentValue >= 25)
        {
            healthIcon.sprite = healthIcon3;
        }
        if (health.currentValue < 25)
        {
            healthIcon.sprite = healthIcon4;
        }
    }
}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float regenrate;
    public float decayRate;

    public void Add(float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Subtrack(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0);
    }
    public float GetPercentage()
    {
        return currentValue / maxValue;
    }
}

public interface IDamagable
{
    void TakePhysicDamage(int damageAmount);
}
