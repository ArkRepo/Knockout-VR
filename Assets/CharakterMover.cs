using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharakterMover : MonoBehaviour
{
    public KeyCode goForward;
    public KeyCode goBackward;
    public KeyCode goLeft;
    public KeyCode goRight;
    public KeyCode doJump;

    //-1 für unendlich
    public int airJumpCount = 0;

    public float angularDecay = 0.9f;
    public float angularSpeed = 10;

    public float forwardMaxSpeed = 1;
    public float backwardMaxSpeed = 1;
    public float leftMaxSpeed = 1;
    public float rightMaxSpeed = 1;

    public float forwardAccell = 1;
    public float backwardAccell = 1;
    public float leftAccell = 1;
    public float rightAccell = 1;

    public float jumpImpuls = 1;

    Rigidbody rb;

    bool cursorLock = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 speedForward = Vector3.Dot(transform.forward, rb.velocity) / (transform.forward.magnitude * transform.forward.magnitude) * transform.forward;
        Vector3 speedRight = Vector3.Dot(transform.right, rb.velocity) / (transform.right.magnitude * transform.right.magnitude) * transform.right;

        if (Input.GetKey(goForward) && speedForward.magnitude < forwardMaxSpeed) rb.AddForce(transform.forward * Time.deltaTime * forwardAccell, ForceMode.Acceleration);
        if (Input.GetKey(goBackward) && -speedForward.magnitude < backwardMaxSpeed) rb.AddForce(-transform.forward * Time.deltaTime * backwardAccell, ForceMode.Acceleration);
        if (Input.GetKey(goLeft) && speedRight.magnitude < leftMaxSpeed) rb.AddForce(-transform.right * Time.deltaTime * leftAccell, ForceMode.Acceleration);
        if (Input.GetKey(goRight) && -speedRight.magnitude < rightMaxSpeed) rb.AddForce(transform.right * Time.deltaTime * rightAccell, ForceMode.Acceleration);
        if (Input.GetKeyDown(doJump)) rb.AddForce(Vector3.up * jumpImpuls, ForceMode.Impulse);

        if (cursorLock) {
            if (Input.GetKeyDown(KeyCode.Escape)) cursorLock = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape)) cursorLock = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        rb.angularVelocity *= angularDecay * Time.deltaTime;
        rb.AddTorque(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * angularSpeed, ForceMode.Acceleration);
    }
}
