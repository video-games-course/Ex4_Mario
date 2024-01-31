using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHeadTriger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player's head
        if (other.CompareTag("PlayerHead"))
        {
            // Destroy the prefab
            Destroy(other.transform.root.gameObject);
        }
    }
}
