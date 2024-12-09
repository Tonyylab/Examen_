using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Recoger {0}", item.displayName);
    }

    public void OnInteract()
    {
        AudioManager.instance.player.clip = AudioManager.instance.PickUpSFX;
        AudioManager.instance.player.Play();
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
