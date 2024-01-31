using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    // Reference to the player's KeyboardForceAdder script

    public KeyboardForceAdder playerForceAdder;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger zone
        if (other.CompareTag("Player"))
        {
            // Activate the super jump on the player's script
            playerForceAdder.ActivateSuperJump();

            // Deactivate the gift object (optional)
            gameObject.SetActive(false);
        }
    }
}
