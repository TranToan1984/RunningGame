using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;
    Vector3 moveDir;
    public float rotateSpeed = 25f; //Speed the player rotate
    public float speed = 7f; //Speed the player rotate
    public float maxVelocityChange = 10.0f;
    public float slipperyForce = 1000f;
    public float slipperyMaxSpeed = 10f;
    GameObject mainCamera;
    public CapsuleCollider capCol;
    public LayerMask groundLayer;
    public LayerMask slipperyLayer;
    Timer countdownToDieTimer = new Timer();
    Vector3 checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        UpdatePlayerDie();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateInputPlaying();

        if (CheckSlippery())
        {
            UpdateSlippery();
        }
        else
        {
            UpdateMovement();
        }
        //Debug.Log("HasGroundUnder " + HasFloorUnder());
    }

    public void UpdateInputPlaying()
    {
        moveDir = GetMoveDirection();
    }

    public Vector3 GetMoveDirection()
    {
        float deltaX = 0;
        float deltaY = 0;
        //get input from keyboard
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
            h2 += h2 * dist;;

            return (v2 + h2).normalized; //Global position to which I want to move in magnitude 1
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void UpdateFaceDir()
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

    public void UpdateMovement()
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

    public void HitPlayer(Vector3 velocityF, float bounceTime)
    {
        m_rb.velocity = velocityF; // add velocity
    }

    public bool HasFloorUnder()
    {
        return Physics.CheckCapsule(capCol.bounds.center, new Vector3(capCol.bounds.center.x, capCol.bounds.min.y - Mathf.Abs(50f), capCol.bounds.center.z), capCol.radius * 0.25f, groundLayer | slipperyLayer);
    }

    public bool CheckSlippery()
    {
        return Physics.CheckCapsule(capCol.bounds.center, new Vector3(capCol.bounds.center.x, capCol.bounds.min.y - 0.2f, capCol.bounds.center.z), capCol.radius * 0.25f, slipperyLayer);
    }

    void UpdatePlayerDie()
    {
        if(HasFloorUnder()) // check if player on ground
        {
            countdownToDieTimer.SetTimerDone(); //don't set countdown to die
        }
        else
        {
            if(countdownToDieTimer.IsDone())
            {
                countdownToDieTimer.SetDuration(1); //set duration before player die
            }
            else
            {
                countdownToDieTimer.Update(Time.deltaTime); // countdown to die
                if(countdownToDieTimer.JustFinished())
                {
                    DeadAndRespawn();
                }
            }
        }
    }

    void DeadAndRespawn()
    {
        m_rb.velocity = Vector3.zero; //remove velocity
        transform.position = checkPoint; //spawn player at latest checkpoint
    }

    private void OnTriggerEnter(Collider other)
    {
        checkPoint = other.gameObject.transform.position;
    }

    void UpdateSlippery()
    {
        UpdateFaceDir();

        Vector3 slideVal = moveDir * slipperyForce * Time.deltaTime;
        m_rb.AddForce(slideVal, ForceMode.Acceleration);
        if (m_rb.velocity.magnitude > slipperyMaxSpeed)
        {
            m_rb.velocity = m_rb.velocity.normalized * slipperyMaxSpeed;
        }
    }
}
