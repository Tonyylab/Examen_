using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class MenuConfiguration : MonoBehaviour
{
    public GameObject menuWindow,SConfing;
    private PlayerController controller;
    public void Toggle()
    {
        if (menuWindow.activeInHierarchy)
        {
            menuWindow.SetActive(false);
            SConfing.SetActive(false);
            controller.ToggleCursor(false);
        }
        else
        {
            menuWindow.SetActive(true);
            controller.ToggleCursor(true);
        }
    }
    private void Awake()
    {
        controller = FindAnyObjectByType<PlayerController>();
        menuWindow.SetActive(false);
    }
    public void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }
    public void CerrarJuego()
    {
        SceneManager.LoadScene("Menu");
    }
}
