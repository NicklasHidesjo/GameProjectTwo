using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsViaState : MonoBehaviour
{
    NPC npc;
    float deathTimer;


    void Start()
    {
        npc = GetComponent<NPC>();
    }

    void FixedUpdate()
    {
        ToggleSymbols();
    }

    private void ToggleSymbols()
    {
        if (npc.IsDead && deathTimer >= 5.0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            //Vill vi disabla gameobjectet här?
            //gameObject.SetActive(false);


        }
        else if (npc.IsDead)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            deathTimer += Time.deltaTime;
        }
        else if (npc.Alertness <= npc.Stats.CautiousThreshold)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (npc.Alertness >= npc.Stats.MaxAlerted)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (!npc.IsDead && npc.Alertness > npc.Stats.CautiousThreshold)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }

    }
}
