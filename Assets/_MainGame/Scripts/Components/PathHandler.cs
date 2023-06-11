using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    float scaleZ = 0;
    private void Awake()
    {
        scaleZ = transform.localScale.z; //get original localScale.z, we will use this value to scale localScale.z from 0 -> scaleZ
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);//set localScale.z = 0 so that we can scale localScale.z value little by little in next function
        StartCoroutine(IEShow());
    }

    IEnumerator IEShow()
    {
        while(transform.localScale.z < scaleZ)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 0.01f); //render the path little by little
            yield return null;
        }
        yield return null;
    }
}
