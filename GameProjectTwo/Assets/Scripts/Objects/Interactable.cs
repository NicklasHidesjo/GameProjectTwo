using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    List<GameObject> charactersInside;
    PlayerObjectInteract playerInteract;
    [SerializeField] Material selectedMaterial;
    [SerializeField] Material standardMaterial;
    



    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            GetComponent<MeshRenderer>().material = selectedMaterial;

        }
        else
        {
            GetComponent<MeshRenderer>().material = standardMaterial;

        }

    }

 

}
