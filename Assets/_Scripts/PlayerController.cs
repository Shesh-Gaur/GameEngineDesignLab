using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player Movement
    public PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;

    float lookRotate = 0.0f;
    public float lookSpeed = 20.0f;
    private float walkSpeed = 5.0f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    //Player Jump
    Rigidbody rb;
    private float distanceToGround;
    private bool isGrounded = false;
    float jump = 5.0f;

    //Player Animation
    Animator playerAnimator;
    private bool isWalking = false;

    //Projectile Bullets
    public GameObject bullet;
    public Transform projectilePos;

    //Player Stats
    public float hp = 10.0f;
    Vector3 lastPosOnGround;
    Vector3 startingPos;
    public Slider healthUI;

    // Start is called before the first frame update
    void Start()
    {
        //Using controller from player input controller
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.LookKeyboard.performed += cntxt => lookRotate = cntxt.ReadValue<float>();
        inputAction.Player.LookKeyboard.canceled += cntxt => lookRotate = 0.0f;

        inputAction.Player.Jump.performed += cntxt => Jump();
        inputAction.Player.Shoot.performed += cntxt => Shoot();



        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        startingPos = transform.position;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }

    }

    public void Shoot()
    {
        Rigidbody bulletRb = Instantiate(bullet, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 32.0f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

    private void Death()
    {
        if (hp <= 0.0f)
        {
            ScoreManager.instance.GameOver();
        }

        if (transform.position.y < -25.0f)
        {
            hp -= 4.0f;
            healthUI.value = hp;
            transform.position = lastPosOnGround;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Death();

        if (isGrounded)
            lastPosOnGround = transform.position;

        transform.Translate(Vector3.right * move.x * walkSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.forward * move.y * walkSpeed * Time.deltaTime, Space.Self);

        transform.Rotate(Vector3.up, lookRotate * lookSpeed * Time.deltaTime);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }
}
