using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSunLight : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugRay;

    [Header("Settings")]
    [SerializeField] LayerMask checkLayers;
    [SerializeField] Transform linearLightSun;
    [SerializeField] Transform playerPoint;

    private float WidthOff = 0.0f;
    private float hightOff = 0.0f;

    [SerializeField] float lightSourceDist = 100f;
    [SerializeField] float inSkin = -0.05f;
    [SerializeField] bool inSunlight;

    private PlayerStatsManager playerStats;

    private Coroutine sunDamage;
    [SerializeField] float gracePeriod;
    private float graceTimer;
    [SerializeField] float sunDamageTickRate;
    [SerializeField] int damagePerTick;


    void Start()
    {
        SunInit();
    }


    void FixedUpdate()
    {
        inSunlight = InSun();
        if (inSunlight)
        {
            
            if (sunDamage == null)
            {
                sunDamage = StartCoroutine(TakeDamageInSunLight());
                
                //activateSunIndicator(gracePeriod);
            }
            graceTimer = Mathf.Clamp(graceTimer + Time.deltaTime, 0, gracePeriod);
        }
        else
        {
            if (sunDamage != null)
            {
                //GameUiManager.instance.DeactivateSunIndicator();

                StopCoroutine(sunDamage);
                sunDamage = null;
            }

            graceTimer = Mathf.Clamp(graceTimer - Time.fixedDeltaTime, 0, gracePeriod);

        }

        GameUiManager.instance.SetSunIndicatorAlpha(graceTimer, gracePeriod);
    }

    IEnumerator TakeDamageInSunLight()
    {
        print("in sun light");

        while (graceTimer < gracePeriod)
        {
            yield return null;
        }

        while (inSunlight && !playerStats.IsDead)
        {
            print("sunDamage taken: " + damagePerTick);
            playerStats.DecreaseHealthValue(damagePerTick);
            yield return new WaitForSeconds(sunDamageTickRate);
        }

    }

    public void SunInit(Transform dracula, Transform linearLightSun)
    {
        this.playerPoint = dracula;
        this.linearLightSun = linearLightSun;
        GetOffsettFromCollider();
    }

    public void SunInit()
    {
        if (!linearLightSun)
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light l in lights)
            {
                if (l.type == LightType.Directional)
                {
                    linearLightSun = l.transform;
                    Debug.Log("<color=red> Sun is missing. Auto assigned : </color>" + l.name);
                    break;
                }
            }
        }

        if (playerPoint == null)
        {
            playerPoint = FindObjectOfType<CharacterController>().transform;
           // Debug.Log("<color=red> dracula is missing. Auto assigned : </color>" + dracula.name);
        }
        playerStats = PlayerManager.instance.GetComponent<PlayerStatsManager>();
        GetOffsettFromCollider();

    }

    void GetOffsettFromCollider()
    {
        Vector3 dBounds = playerPoint.GetComponent<Collider>().bounds.extents;
        WidthOff = dBounds.x + inSkin;
        hightOff = dBounds.y + inSkin;
    }



    //The meat
    public bool InSun()
    {
        //If sun is above horizon, 
        //Racast to see if hit dracula.
        //TODO : switch lightdir or filtermask???
        float scalarTimeOfDay = Vector3.Dot(Vector3.up, linearLightSun.transform.forward);

        if (scalarTimeOfDay < 0)
        {
            // :P
            if (
            RaycastSunToCharacter(playerPoint.position - linearLightSun.transform.forward *
                lightSourceDist - linearLightSun.transform.right *
                WidthOff + linearLightSun.transform.up * hightOff,
                linearLightSun.transform.forward)
                
            ||
            RaycastSunToCharacter(playerPoint.position - linearLightSun.transform.forward *
                lightSourceDist + linearLightSun.transform.right *
                WidthOff + linearLightSun.transform.up * hightOff,
                linearLightSun.transform.forward)
                
                )
            {

                return true;
            }
        }
        return false;
    }

    private bool RaycastSunToCharacter(Vector3 pos, Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, lightSourceDist, checkLayers))
        {

            if (debugRay)
                Debug.DrawRay(pos, dir * lightSourceDist, Color.black);
            
            if (hit.collider == playerPoint.gameObject.GetComponent<Collider>())
            {
                Debug.Log("<color=red>Dracula hit self</color>");
            }
            return false;
        }

        if (debugRay)
            Debug.DrawRay(pos, dir * lightSourceDist, Color.yellow);

        return true; //<--- AddDamage here
    }
}
