using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public GameObject enemy1, enemy2, enemy3;
    void Start()
    {
        enemy1.SetActive(false);
        enemy3.SetActive(false);
        enemy2.SetActive(false);
        Invoke("ActivarObjetos", 120f);
    }

    void ActivarObjetos()
    {
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
    }
}
