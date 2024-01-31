using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    public float windForce = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WindZone"))
        {
            ApplyWindForce();
        }
    }

    private void ApplyWindForce()
    {
        // Assuming your player has a Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();

        // Apply force in the desired direction (you can modify this based on your game)
        rb.AddForce(Vector3.right * windForce, ForceMode.Force);
    }
}