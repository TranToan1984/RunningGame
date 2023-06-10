using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public enum PATH_STATE
    {
        HIDE,

        SHOW
    }
    Timer showTime = new Timer();
    Timer hideTime = new Timer();
    public PathHandler pathHander;
    // Start is called before the first frame update
    void Start()
    {
        showTime.SetDuration(3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        showTime.Update(Time.deltaTime);
        if(showTime.JustFinished())
        {
            SetState(PATH_STATE.HIDE); //after showing 3s, hide the path
            hideTime.SetDuration(3.0f); //set time for hiding the path
        }
        hideTime.Update(Time.deltaTime);
        if(hideTime.JustFinished())
        {
            SetState(PATH_STATE.SHOW); //after hiding 3s, show the path
            showTime.SetDuration(3.0f); //set time for show the path
        }
    }

    void SetState(PATH_STATE st)
    {
        switch (st)
        {
            case PATH_STATE.HIDE:
                pathHander.gameObject.SetActive(false);
                break;
            case PATH_STATE.SHOW:
                pathHander.gameObject.SetActive(true);
                break;
        }
    }
}
