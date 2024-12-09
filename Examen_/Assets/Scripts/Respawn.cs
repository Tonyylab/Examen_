using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out of Bounds"))
        {
            transform.position = respawnPoint.transform.position;
            transform.rotation = respawnPoint.transform.rotation;
        }
    }
}
