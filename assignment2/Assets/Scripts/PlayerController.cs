using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    private CameraController cameraController;

    private Quaternion targetRotation; // Target orientation of the player

    private bool isCollisionDetected = false;

    CharacterController characterController;
    Animator animator;

    public bool isPlayerAlive = false;
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (!isPlayerAlive) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Check for input only from "A", "D", and "W" keys
        // if (Input.GetKey(KeyCode.A))
        // {
        //     h = -1f;
        // }
        // else if (Input.GetKey(KeyCode.D))
        // {
        //     h = 1f;
        // }

        // if (Input.GetKey(KeyCode.W))
        // {
        //     v = 1f;
        // }

        // if (Input.GetKey(KeyCode.S))
        // {
        //     v = -1f;
        // }

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h)+Mathf.Abs(v));

        Vector3 moveInput = new Vector3(h, 0f, v).normalized;
        var movDir = cameraController.PlannerRotation * moveInput;

        if (moveAmount > 0)
        {
            characterController.Move(movDir*speed*Time.deltaTime);
            targetRotation = Quaternion.LookRotation(movDir);
        }

        // Apply rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        // Vector3 movement = cameraController.PlannerRotation * moveInput * speed * Time.deltaTime;
        // transform.position += movement;
        animator.SetFloat("Velocity", moveAmount);

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a coin
        if (other.CompareTag("Block"))
        {
            // Destroy the coin object
            // Move the player back
            isCollisionDetected = true;
        }
    }

    public void SetIsPlayerAlive(bool status) {
        isPlayerAlive = status;
    }
}
