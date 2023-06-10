using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapboxHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DeadAndRespawn(); //dead when hit trapbox
        }
    }
}
