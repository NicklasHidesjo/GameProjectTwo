using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLairFinder : MonoBehaviour
{
    //Activates Lairfinder Gameobject if it's a child, slower but no need for index.
    //faster/cheaper with index I presume, but this doesn't run often and we might mod the player more.

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
