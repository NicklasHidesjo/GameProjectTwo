using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for any object that is interactable
public abstract class Interactable : MonoBehaviour
{

    protected InteractableScanner iScanner;

    public virtual void SetSelected(bool isSelected, InteractableScanner playerScanner)
    {
        if (isSelected)
        {
            //GetComponent<MeshRenderer>().material = selectedMaterial;
            iScanner = playerScanner;
            SpriteRenderer sprite = transform.Find("Closest interactable sprite renderer").gameObject.GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
        else
        {
            //GetComponent<MeshRenderer>().material = standardMaterial;
            iScanner = null;
            SpriteRenderer sprite = transform.Find("Closest interactable sprite renderer").gameObject.GetComponent<SpriteRenderer>();
            sprite.enabled = false;
        }
    }

    public abstract void Interact(GameObject player);

}
