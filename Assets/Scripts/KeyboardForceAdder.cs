using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TouchDetector))]
public class KeyboardForceAdder : MonoBehaviour
{
    [Tooltip("The force that the player's feet use for horizontal movement, in newtons.")]
    [SerializeField] float horizontalMoveForce = 5f;

    [Tooltip("The force that the player's feet use for vertical movement, in newtons.")]
    [SerializeField] float verticalMoveForce = 5f;

    [Tooltip("The force that the player's feet use for jumping, in newtons.")]
    [SerializeField] float jumpForce = 5f;

    [Tooltip("The factor by which to slow down horizontal movement during a jump.")]
    [Range(0, 1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [Tooltip("The force that the player's feet use for a temporary increased jump, in newtons.")]
    [SerializeField] float increasedJumpForce = 15f;

    [Tooltip("The duration of the increased jump force in seconds.")]
    [SerializeField] float increasedJumpDuration = 10f;
    private Vector3 startingPoint;  // Remember the starting point

    private Rigidbody rb;
    private TouchDetector td;
    private InputAction moveHorizontal;
    private InputAction moveVertical;
    private InputAction jump;

    private bool playerWantsToJump = false;

    void OnValidate()
    {
        if (jump == null)
        {
            jump = new InputAction(type: InputActionType.Button);
            jump.AddBinding("<Keyboard>/space");
        }

        if (moveHorizontal == null)
            moveHorizontal = new InputAction(type: InputActionType.Button);

        if (moveHorizontal.bindings.Count == 0)
            moveHorizontal.AddCompositeBinding("1DAxis")
                .With("Positive", "<Keyboard>/downArrow")
                .With("Negative", "<Keyboard>/upArrow"); 

        if (moveVertical == null)
            moveVertical = new InputAction(type: InputActionType.Button);

        if (moveVertical.bindings.Count == 0)
            moveVertical.AddCompositeBinding("1DAxis")
                .With("Positive", "<Keyboard>/rightArrow")
                .With("Negative", "<Keyboard>/leftArrow");
                
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        td = GetComponent<TouchDetector>();
        startingPoint = transform.position;
        moveHorizontal.Enable();
        moveVertical.Enable();
        jump.Enable();
    }

    void OnEnable()
    {
        moveHorizontal.Enable();
        moveVertical.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        moveHorizontal.Disable();
        moveVertical.Disable();
        jump.Disable();
    }

    void Update()
    {
        // Check for jump input
        if (jump.WasPressedThisFrame() && td.IsTouching())
            playerWantsToJump = true;
    }

    void FixedUpdate()
    {
        if (td.IsTouching())
        {
            float horizontalInput = moveHorizontal.ReadValue<float>();
            float verticalInput = moveVertical.ReadValue<float>();

            Vector3 movement = new Vector3(horizontalInput * horizontalMoveForce, 0, verticalInput * verticalMoveForce);

            // Apply movement force
            rb.AddForce(movement, ForceMode.Force);

            // Check for jump
            if (playerWantsToJump)
            {
                // Slow down horizontal movement during jump
                rb.velocity = new Vector3(rb.velocity.x * slowDownAtJump, rb.velocity.y, rb.velocity.z);

                // Apply jump force
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerWantsToJump = false;
            }
        }
        else if (transform.position.y < 3)
        {
            // Respawn the player at the starting point
            Respawn();
        }
    }
    public void Respawn()
    {
        // Reset the player's position to the starting point
        rb.velocity = Vector3.zero;
        transform.position = startingPoint;
    }
    public void ActivateSuperJump()
    {
        StartCoroutine(IncreaseJumpForceTemporarily());
    }
    // increaseing the JumpForce for 10 sec when the player touches the gi
    IEnumerator IncreaseJumpForceTemporarily()
    {
        float originalJumpForce = jumpForce;
        jumpForce = increasedJumpForce;
        yield return new WaitForSeconds(increasedJumpDuration);
        jumpForce = originalJumpForce;
    }
}
