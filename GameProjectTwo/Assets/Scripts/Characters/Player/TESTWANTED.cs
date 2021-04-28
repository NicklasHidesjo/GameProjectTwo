using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTWANTED : MonoBehaviour
{
    public PlayerNotoriousLevels pN;
    // Start is called before the first frame update
    void Start()
    {
        pN =
        GetComponent<PlayerNotoriousLevels>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, Vector3.up * pN.GetPlayerNotoriousLevel() * 10, Color.red);
    }
}
