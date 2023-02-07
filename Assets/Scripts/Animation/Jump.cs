// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float force;

    //State Variables
    [SerializeField]
    string floorTag = "Floor"; //Tag
    public int maxJump = 2;
    public int jumpCount = 0;

    bool jumpButtonEnabled = true;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void onJump(InputAction.CallbackContext ctx)
    {
        // Mixed Controls
        if (jumpButtonEnabled)
        {
            //If button pressed down, start jump
            if (ctx.performed && jumpCount <= maxJump)
            {
                PerformJump();
            }
        }
    }

    //If collision with ground occurs call Ground().
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(floorTag))
        {
            Ground();
        }
    }

    void PerformJump()
    {
        // Here for BCI stuff.
        if (jumpCount <= maxJump)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse); //Make character jump using physics.
            jumpCount++;
        }
    }

    void Ground()
    {
        jumpCount = 0; //Reset state.
    }

    // Mixed Controls
    void DisableJumpBTN()
    {
        jumpButtonEnabled = false;
    }

    // Mixed Controls
    void EnableJumpBTN()
    {
        jumpButtonEnabled = true;
    }

    private void OnEnable()
    {
        BCIManager.jumpReceived += PerformJump;
        BCIManager.disableJumpButton += DisableJumpBTN;
        BCIManager.enableJumpButton += EnableJumpBTN;
    }

    private void OnDisable()
    {
        BCIManager.jumpReceived -= PerformJump;
        BCIManager.disableJumpButton -= DisableJumpBTN;
        BCIManager.enableJumpButton -= EnableJumpBTN;
    }
}
