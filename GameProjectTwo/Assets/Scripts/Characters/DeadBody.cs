using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//used to make any object carriable by the player
public class DeadBody : Interactable
{

    private bool isGrabbed;
    private bool isHidden;
    public bool IsHidden => isHidden;

    private GameObject objToFollow;
    private bool moveComplete;
    private Quaternion offset;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * -100, ForceMode.Impulse);

    }

    private void Update()
    {
        if (!isGrabbed)
        {
            return;
        }

        if (objToFollow != null)
        {

            transform.position = objToFollow.transform.position + objToFollow.transform.up * 2f;           
            transform.rotation = objToFollow.transform.rotation * offset;
            return;


        }


    }

    public override void Interact(GameObject player)
    {
        if (isGrabbed)
        {
            // transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            isGrabbed = false;
            objToFollow = null;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //  transform.SetParent(player.transform, true);
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());        
            isGrabbed = true;
            StartCoroutine(MoveAboveHead(player.transform));
        }

    }

    public void SetHidden(bool status)
    {
        isHidden = status;
        if (isHidden)
        {
            //GetComponent<Collider>().enabled = false;
        }
        else
        {
            //GetComponent<Collider>().enabled = true;

        }
    }

    IEnumerator MoveAboveHead(Transform player)
    {

        yield return new WaitForSeconds(0.5f); //sync with anim
        float startTime = Time.time;
        float journeyTime = 0.7f;
        Vector3 targetPos = player.position + player.up * 2f;
        Vector3 startPos = transform.position;
        Vector3 center = player.position - Vector3.up;
        Vector3 startRelCenter = startPos - center;
        Vector3 endRelCenter = targetPos - center;


        while (transform.position != targetPos)
        {
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
            transform.position += center;
            yield return null;
        }

        objToFollow = player.gameObject;
        offset = transform.rotation * Quaternion.Inverse(objToFollow.transform.rotation);

    }

}
