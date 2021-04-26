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
    [SerializeField] Transform playerPont;

    private float WidthOff = 0.0f;
    private float hightOff = 0.0f;

    [SerializeField] float lightSourceDist = 100f;
    [SerializeField] float inSkin = -0.05f;
    [SerializeField] bool inSunlight;

    // Start is called before the first frame update
    void Start()
    {
        SunInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inSunlight = InSun();
    }

    public void SunInit(Transform dracula, Transform linearLightSun)
    {
        this.playerPont = dracula;
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

        if (playerPont == null)
        {
            playerPont = FindObjectOfType<CharacterController>().transform;
           // Debug.Log("<color=red> dracula is missing. Auto assigned : </color>" + dracula.name);
        }

        GetOffsettFromCollider();
    }

    void GetOffsettFromCollider()
    {
        Vector3 dBounds = playerPont.GetComponent<Collider>().bounds.extents;
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
            RaycastSunToCharacter(playerPont.position - linearLightSun.transform.forward *
                lightSourceDist - linearLightSun.transform.right *
                WidthOff + linearLightSun.transform.up * hightOff,
                linearLightSun.transform.forward)
                == true
            ||
            RaycastSunToCharacter(playerPont.position - linearLightSun.transform.forward *
                lightSourceDist + linearLightSun.transform.right *
                WidthOff + linearLightSun.transform.up * hightOff,
                linearLightSun.transform.forward)
                == true
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
            
            if (hit.collider == playerPont.gameObject.GetComponent<Collider>())
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
