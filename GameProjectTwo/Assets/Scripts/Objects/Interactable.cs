using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for any object that is interactable
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Material selectedMaterial;
    protected Material standardMaterial;
    protected InteractableScanner iScanner;

    public virtual void SetSelected(bool isSelected, InteractableScanner playerScanner)
    {
        if (isSelected)
        {
            GetComponent<MeshRenderer>().material = selectedMaterial;
            iScanner = playerScanner;
        }
        else
        {
            GetComponent<MeshRenderer>().material = standardMaterial;
            iScanner = null;
        }
    }

    public abstract void Interact(GameObject player);

}
