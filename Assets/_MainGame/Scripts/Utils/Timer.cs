public class Timer
{
    float timer = 0;
    float duration = 0;
    float overhead = 0;
    bool isDone = true;
    bool isPause = false;

    public bool Pause
    {
        get { return isPause; }
        set { isPause = value; }
    }

    public void SetDuration(float time)
    {
        timer = time;
        duration = time;
        overhead = 0;
        isDone = false;
    }

    public void SetTimerDone()
    {
        timer = 0f;
        overhead = -duration;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public float GetTime()
    {
        return timer;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public float GetDuration()
    {
        return duration;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public float GetTimePercent()
    {
        if (duration == 0)
        {
            return 0;
        }

        if (timer < 0)
        {
            timer = 0;
        }
        return timer / duration;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void Reset()
    {
        timer = duration;
        overhead = 0;
        isDone = false;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public bool IsDone()
    {
        return timer == 0;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public bool JustFinished()
    {
        if (timer > 0)
        {
            return false;
        }
        if (isDone)
        {
            return false;
        }

        isDone = true;
        return true;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public float GetOverhead()
    {
        return overhead;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void Update(float deltaTime)
    {
        if (timer == 0 || isPause)
        {
            return;
        }

        timer -= deltaTime;
        if (timer < 0)
        {
            overhead = -timer;
            timer = 0;
        }
    }

    public void SetStartTime(float startTime)
    {
        timer = startTime;
    }

    public void UpdateTimeUp(float deltaTime)
    {
        timer += deltaTime;
    }

    public string GetTimeByHH_MM_SS()
    {
        System.TimeSpan interval = System.TimeSpan.FromSeconds(timer);
        return interval.ToString("hh':'mm':'ss");
    }
}