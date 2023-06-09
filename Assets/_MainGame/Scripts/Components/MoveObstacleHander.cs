using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveObstacleHander : MonoBehaviour
{
    Sequence mySequence;
    public Vector3 vecDirection = Vector3.right;
    public float distance = 10f;
    public float firstTimePush = 0.5f;
    public float secondTimePush = 2f;
    // Start is called before the first frame update
    void Start()
    {
        DoMove();
    }

    public void DoMove()
    {
        //use sequence from Dotween.
        mySequence = DOTween.Sequence();
        Vector3 newPos = vecDirection * distance;// calculate new position which object will be moved to there
        mySequence.Append(transform.DOMove(transform.position - newPos, firstTimePush).SetEase(Ease.Linear)); // add moving to new position
        mySequence.Append(transform.DOMove(transform.position, secondTimePush).SetEase(Ease.Linear)); // add moving back to original position
        mySequence.SetLoops(-1); //loop forever
    }
}
