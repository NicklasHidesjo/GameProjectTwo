using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLairFinder : MonoBehaviour
{
    //Activates Lairfinder Gameobject if it's a child, slower but no need for index.
    //faster/cheaper with index I presume, but this doesn't run often and we might mod the player more.

    public void Activate()
    {
        transform.Find("LairFinder").gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        transform.Find("LairFinder").gameObject.SetActive(false);
    }
}
