using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public GameObject victory;
    private PlayerController player;
    public static int enemiesSlain;

    private void Start()
    {
        enemiesSlain = 0;
        player = FindObjectOfType<PlayerController>();
    }

    public string GetInteractPrompt()
    {
        if (enemiesSlain < 3)
        {
            return "(Bloqueado) Acaba con todos los enemigos";
        }
        else
        {
            {
                return "Abrir";
            }
        }
    }

    public void OnInteract()
    {
        if (enemiesSlain == 3)
        {
            victory.SetActive(true); 
            AudioManager.instance.MuteAll();
            AudioManager.instance.player.mute = false;
            AudioManager.instance.player.clip = AudioManager.instance.VictorySFX;
            AudioManager.instance.player.Play();
        }
    }
}
