using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRandomizer : MonoBehaviour
{

    [SerializeField] private Material[] _materials;
    [SerializeField] private Renderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
        _renderer.material = _materials[Random.Range(0, _materials.Length)];
    }

}