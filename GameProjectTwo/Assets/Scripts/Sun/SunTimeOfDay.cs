using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunTimeOfDay : MonoBehaviour
{
    [SerializeField] float sunLightAngle = 30;
    [SerializeField] float timeOfDay = 0f;
    [SerializeField] float intensetyMultiplyer = 4.0f;
    private Coroutine runningClock;
    private Coroutine animClock;



    public void MoveSunRealTimeStep()
    {
        //TODO : Can be moved to start if function used
        float oneDayInMin = 1;
        float rotTimeScale = oneDayInMin / 60;
        rotTimeScale *= 360;
        float degOfDay = timeOfDay * 15;
        //

        degOfDay += rotTimeScale * Time.deltaTime;
        if (degOfDay > 360)
        {
            degOfDay -= 360;
        }
        transform.rotation = Quaternion.Euler(degOfDay - 90, sunLightAngle, 0);
        EnableDisableByAngle();
        timeOfDay = (degOfDay) / 15;
    }
    public void SetTimeOfDayTo(float time)
    {
        timeOfDay = time;
        float degOfDay = time * 15;
        transform.rotation = Quaternion.Euler(degOfDay - 90, sunLightAngle, 0);
        EnableDisableByAngle();
    }

    public void SetRiseTimer(float timeTillSunRise, float clockStopTime, float sunRiseAnimTime)
    {
        if (runningClock != null)
        {
            //print("STOPP : " + runningClock);
            StopCoroutine(runningClock);
        }
        EnableDisableByAngle();
        runningClock = StartCoroutine(SunTimer(timeTillSunRise, clockStopTime, sunRiseAnimTime));
    }

    private IEnumerator SunTimer(float timeTillSunRise, float clockStopTime, float sunRiseAnimTime)
    {
        yield return new WaitForSeconds(timeTillSunRise);
        print("Sun is rising");
        runningClock = null;

        MoveTimeOfDayTo(clockStopTime, sunRiseAnimTime);
    }

    public void MoveTimeOfDayTo(float toTimeOfDay, float sunAnimTime)
    {
        if (animClock != null)
        {
            // print("STOPP : " + runningClock);
            StopCoroutine(animClock);
        }

        animClock = StartCoroutine(MoveTimeToOverTime(toTimeOfDay, sunAnimTime));
    }

    private IEnumerator MoveTimeToOverTime(float toTimeOfDay, float sunAnimTime)
    {
        float degOfDay = timeOfDay * 15;
        float degTarget = toTimeOfDay * 15;
        float degDiff = degTarget - degOfDay;
        if (degDiff < 0)
        {
            degDiff += 360;
        }
        degDiff /= sunAnimTime;
        float t = sunAnimTime;


        while (t > 0)
        {
            degOfDay += degDiff * Time.deltaTime;

            if (degOfDay > 360)
            {
                degOfDay -= 360;
            }

            transform.rotation = Quaternion.Euler(degOfDay - 90, sunLightAngle, 0);
            timeOfDay = (degOfDay) / 15;

            t -= Time.deltaTime;
            EnableDisableByAngle();

            yield return null;
        }

        runningClock = null;
    }


    private void EnableDisableByAngle()
    {
        float d = Vector3.Dot(Vector3.down, transform.forward);
        if(d < 0)
        {
            GetComponent<Light>().enabled = false;
        }
        else
        {
            GetComponent<Light>().enabled = true;
            GetComponent<Light>().intensity = d * intensetyMultiplyer;
        }
    }
}
