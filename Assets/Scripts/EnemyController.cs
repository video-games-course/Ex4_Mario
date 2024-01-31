using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxZDistance = 9f;

    private bool movingForward = true;

    void Update()
    {
        MoveSpawner();
    }

    void MoveSpawner()
    {
        float direction = movingForward ? 1f : -1f;
        transform.Translate(Vector3.forward * direction * moveSpeed * Time.deltaTime);

        // Change direction if reached the maximum distance
        if (Mathf.Abs(transform.position.z) > maxZDistance)
        {
            movingForward = !movingForward;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Reset the player to the starting point (assuming KeyboardForceAdder is attached to the player)
            KeyboardForceAdder playerController = other.GetComponent<KeyboardForceAdder>();
            if (playerController != null)
            {
                playerController.Respawn();
            }
        }
    }
}
