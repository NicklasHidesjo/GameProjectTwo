using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsViaState : MonoBehaviour
{
    NPC npc;
    float deathTimer;
    float charmTimer;
    float originalCharmTimer;
    Vector3 originalScale;


    void Start()
    {
        npc = GetComponent<NPC>();
        charmTimer = npc.Stats.CharmedTime;
        originalCharmTimer= npc.Stats.CharmedTime;
        originalScale = transform.GetChild(3).gameObject.transform.localScale;
    }

    void FixedUpdate()
    {
        if(npc.CurrentState==0)
        {
            if(transform.GetChild(3).gameObject.activeSelf == false)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
            }
            float i = 1*(charmTimer/originalCharmTimer);
            transform.GetChild(3).gameObject.transform.localScale = i * originalScale;
            charmTimer -= Time.deltaTime;
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(false);
            charmTimer = originalCharmTimer;
            ToggleSymbols();
        }

    }

    private void ToggleSymbols()
    {
        if (npc.IsDead && deathTimer >= 5.0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
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
