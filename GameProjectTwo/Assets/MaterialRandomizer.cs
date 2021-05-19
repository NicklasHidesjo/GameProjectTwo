using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizer : MonoBehaviour
{

    [SerializeField] private Material[] _skinMaterials;
    [SerializeField] private Material[] _hairMaterials;
    [SerializeField] private Material[] _jacketMaterials;
    [SerializeField] private Material[] _pantMaterials;
    private Material[] mats= new Material[4];
    [SerializeField] private Renderer _renderer;


    public void Start()
    {
        _renderer = GetComponent<Renderer>();
      
        mats[0] = _skinMaterials[Random.Range(0, _skinMaterials.Length)];
        //the cloth needs to match numbers, so the arrays must stay the same length.
        int i = Random.Range(0, _jacketMaterials.Length);
        mats[1] = _jacketMaterials[i];
        mats[2] = _pantMaterials[i];

        mats[3] = _hairMaterials[Random.Range(0, _hairMaterials.Length)];
        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
     _renderer.materials = mats;
    }

}