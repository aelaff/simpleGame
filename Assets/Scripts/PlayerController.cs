using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraFollowSpeed = 10f;

    private CharacterController controller;
    private Animator animator;

    private bool isGrounded = false;
    private bool isDead = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        // Check if the player is dead
        if (isDead)
        {
            animator.SetTrigger("Die");
            return;
        }

        // Move the player forward/backward and sideways based on input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.TransformDirection(new Vector3(horizontal, 0, vertical)) * moveSpeed;

        // Set the animation state based on the player's movement
        if (movement.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger("Speed", 2); // Running
        }
        else if (movement.magnitude > 0)
        {
            animator.SetInteger("Speed", 1); // Walking
        }
        else
        {
            animator.SetInteger("Speed", 0); // Idle
        }

        // Jump when the player presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            movement.y = Mathf.Sqrt(2f * jumpHeight * -Physics.gravity.y);
        }

        // Apply movement to the player
        controller.Move(movement * Time.deltaTime);

        // Rotate the player left/right based on input
        float rotate = Input.GetAxis("Mouse X") * rotateSpeed;
        transform.Rotate(0, rotate, 0);

        // Move the camera to follow the player
        Vector3 targetPosition = transform.position + transform.forward * -5f + Vector3.up ;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
        cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    public void Die()
    {
        isDead = true;
    }
}
