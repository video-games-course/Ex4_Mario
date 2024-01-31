using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToLevel_2 : MonoBehaviour
{
    // The name of the next level
    public string nextLevelName ;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Load the next level
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
