using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunTimeOfDay : MonoBehaviour
{
    [SerializeField] float sunLightAngle = 30;
    [SerializeField] float timeOfDay = 0f;
    public float TimeOfDay { get => timeOfDay; }
    [SerializeField] float intensetyMultiplyer = 4.0f;
    private Coroutine runningClock;
    private Coroutine animClock;

    [SerializeField] EnviromentFX eFX;

    //TODO : Niceify
    DisableLightAtDay[] nightLights;
    bool nightLightsON;
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
        nightLights = FindObjectsOfType<DisableLightAtDay>();
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
        AudioManager.instance.PlayOneShot(SoundType.MorningBell, gameObject);
        runningClock = null;

        MoveTimeOfDayTo(clockStopTime, sunRiseAnimTime);
    }

    // this should be called once dracula is full 
    public void MoveTimeOfDayTo(float toTimeOfDay, float sunAnimTime)
    {
        if (animClock != null)
        {
            // print("STOPP : " + runningClock);
            StopCoroutine(animClock);
        }
        Debug.Log("Sun is rising");
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
        float dot = Vector3.Dot(Vector3.down, transform.forward);
        if (dot < 0)
        { //Sun is down
            GetComponent<Light>().enabled = false;
            GetComponent<Light>().intensity = 0;
            SetAllNightLights(true);
        }
        else
        { //Sun is up
            GetComponent<Light>().enabled = true;
            GetComponent<Light>().intensity = dot * intensetyMultiplyer;
            SetAllNightLights(false);
        }

        if (eFX)
        {
            eFX.UpdateEnviroment(dot * intensetyMultiplyer);
        }
    }

    void SetAllNightLights(bool to)
    {
        foreach (DisableLightAtDay l in nightLights)
        {
            l.EnableLight(to);
            nightLightsON = to;
        }
    }
}
