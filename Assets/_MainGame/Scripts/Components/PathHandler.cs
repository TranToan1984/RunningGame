using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    float scaleZ = 0;
    private void Awake()
    {
        scaleZ = transform.localScale.z;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        StartCoroutine(IEShow());
    }

    IEnumerator IEShow()
    {
        while(transform.localScale.z < scaleZ)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 0.01f); //render the path litte by litte
            yield return null;
        }
        yield return null;
    }
}
