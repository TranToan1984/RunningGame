using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Vector3 hitDir; //direction when hit player
    public float force = 10f;
    public float pushTime = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            HitPlayer(playerController, -hitDir * force, pushTime);
        }
    }

    public void HitPlayer(PlayerController playerController, Vector3 velocityF, float bounceTime)
    {
        playerController.HitPlayer(velocityF, bounceTime);
    }
}
