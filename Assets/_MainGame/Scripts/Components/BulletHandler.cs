using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    Rigidbody rb;
    Timer lifeTimer = new Timer();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer.Update(Time.deltaTime);
        if (lifeTimer.JustFinished())
        {
            gameObject.SetActive(false);
        }
    }

    public void SetActive(Vector3 shootForce)
    {
        gameObject.SetActive(true);
        lifeTimer.SetDuration(3.0f); //life time is 3s
        if (!rb) rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-shootForce * rb.mass);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if hit player, the bullet will be inactive and player DeadAndRespawn
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().DeadAndRespawn();
        }
    }

    public bool Active()
    {
        return gameObject.activeSelf;
    }
}
