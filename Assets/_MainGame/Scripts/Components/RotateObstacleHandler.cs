using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacleHandler : MonoBehaviour
{
    public enum TYPE_ROTATION
    {
        AXIS_X,

        AXIS_Y,

        AXIS_Z
    }
    public float speed = 120f;
    public TYPE_ROTATION typeRotation = TYPE_ROTATION.AXIS_Y;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (typeRotation)
        {
            case TYPE_ROTATION.AXIS_X:
                transform.Rotate(speed * Time.fixedDeltaTime, 0, 0, Space.Self); //rotate X-axis
                break;
            case TYPE_ROTATION.AXIS_Y:
                transform.Rotate(0, speed * Time.fixedDeltaTime, 0, Space.Self); //rotate Y-axis
                break;
            case TYPE_ROTATION.AXIS_Z:
                transform.Rotate(0, 0, speed * Time.fixedDeltaTime, Space.Self); //rotate Z-axis
                break;
            
        }
    }
}
