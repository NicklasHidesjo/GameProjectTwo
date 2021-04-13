using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSunLight : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Transform linearLightSun;
    [SerializeField]
    private Transform dracula;

    private float WidthOff = 0.0f;
    private float hightOff = 0.0f;

    public float lightSourceDist = 100f;
    public float inSkin = -0.05f;
    public bool inSunlight;

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
        this.dracula = dracula;
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

        if (dracula == null)
        {
            dracula = FindObjectOfType<CharacterController>().transform;
            Debug.Log("<color=red> dracula is missing. Auto assigned : </color>" + dracula.name);
        }

        GetOffsettFromCollider();
    }

    void GetOffsettFromCollider()
    {
        Vector3 dBounds = dracula.GetComponent<Collider>().bounds.extents;
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
            RaycastSunToCharacter(dracula.position - linearLightSun.transform.forward *
                lightSourceDist - linearLightSun.transform.right *
                WidthOff + linearLightSun.transform.up * hightOff,
                linearLightSun.transform.forward)
                == true
            ||
            RaycastSunToCharacter(dracula.position - linearLightSun.transform.forward *
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
        if (Physics.Raycast(pos, dir, out hit, lightSourceDist, layerMask))
        {
            Debug.DrawRay(pos, dir * lightSourceDist, Color.black);
            if (hit.collider == dracula.gameObject.GetComponent<Collider>())
            {
                Debug.Log("<color=red>Dracula hit self</color>");
            }
            return false;
        }

        Debug.DrawRay(pos, dir * lightSourceDist, Color.yellow);
        return true;
    }
}
