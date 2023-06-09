using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacleHandler : MonoBehaviour
{
    public float speed = 120f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, speed * Time.fixedDeltaTime, 0, Space.Self); //rotate Y-axis
    }
}
