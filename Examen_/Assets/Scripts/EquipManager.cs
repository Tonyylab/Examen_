using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class EquipManager : MonoBehaviour
{
    public Equip currentEquip;
    public Transform equipParent;
    public Animation attack;
    public PlayerController controller;
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerController>();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && currentEquip != null)
        {
            Debug.Log("Atacando");
            currentEquip.OnAttackInput();
            attack.Play();
            Debug.Log("ANIM Atacando");
            controller.DealDamage(currentEquip.gameObject.GetComponent<ItemObject>().item.weaponDamage);
            Debug.Log("Infligiendo Daño");
        }
    }

    public void OnAltAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && currentEquip != null)
        {
            currentEquip.OnAltAttackInput();
        }
    }

    public void EquipNewItem(ItemData item)
    {
        UnEquipItem();
        currentEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquipItem()
    {
        if (currentEquip != null)
        {
            Destroy(currentEquip.gameObject);
            currentEquip = null;
        }
    }


}
