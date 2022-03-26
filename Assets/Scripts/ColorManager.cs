using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public GameObject[] premitives;
    public Material defaultMaterial; 
    public Material defaultSolidMaterial;

    public void Start()
    {
        OnColorClick(defaultMaterial);
    }

    public void OnColorClick(Material material)
    {
        GrabObject.CurrentMateriall = material;
    }

    public void OnColorClickMenuItems(Material material)
    {
        foreach (var premitive in premitives)
        {
            premitive.GetComponent<MeshRenderer>().material = material;
        }
    }
}
