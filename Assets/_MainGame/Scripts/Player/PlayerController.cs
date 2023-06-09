using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;
    Vector3 moveDir;
    float horizontalInput = 0;
    float verticalInput = 0;
    public float rotateSpeed = 25f; //Speed the player rotate
    public float speed = 7f; //Speed the player rotate
    public float maxVelocityChange = 10.0f;
    GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateInputPlaying();
        UpdateMovement();
    }

    void UpdateInputPlaying()
    {
        moveDir = GetMoveDirection();
    }

    public Vector3 GetMoveDirection()
    {
        float deltaX = 0;
        float deltaY = 0;
        deltaX += Input.GetAxisRaw("Horizontal");
        deltaY += Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(deltaX) >= 0.2f || Mathf.Abs(deltaY) >= 0.2f)
        {
            if (!mainCamera)
            {
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            Vector3 v2 = deltaY * mainCamera.transform.forward; //Vertical axis to which I want to move with respect to the camera
            Vector3 h2 = deltaX * mainCamera.transform.right; //Horizontal axis to which I want to move with respect to the camera
            float dist = mainCamera.transform.position.y - this.transform.position.y;
            v2.y = 0;
            h2.y = 0;
            v2 += v2 * dist;
            h2 += h2 * dist;
            horizontalInput = deltaX;
            verticalInput = deltaY;

            return (v2 + h2).normalized; //Global position to which I want to move in magnitude 1
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
            return Vector3.zero;
        }
    }

    protected virtual void UpdateFaceDir()
    {
        Vector3 targetDir = moveDir; //Direction of the character

        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
        transform.rotation = targetRotation;
    }

    protected void UpdateMovement()
    {
        UpdateFaceDir();

        // Calculate how fast we should be moving
        Vector3 targetVelocity = moveDir;
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = m_rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        if (Mathf.Abs(m_rb.velocity.magnitude) < speed * 2.0f)
            m_rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
