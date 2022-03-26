using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public Material collidingMaterial;
    public Material enabledMaterial;
    public Material[] objectMaterials;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material = collidingMaterial;
        
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().material = enabledMaterial;
        if (other.gameObject.tag == "Controller")
        {
            Debug.Log("Exiting");
            var prems = GameObject.FindGameObjectsWithTag("PremNew");
            if (prems.Length == 0)
                return;
            var centerOfParent = new Vector3(0f, 0f, 0f);
            foreach (var prem in prems)
            {
                centerOfParent += prem.transform.position;
                prem.tag = "Merged";
            }
            centerOfParent /= prems.Length;
            var grandParentObject = new GameObject("Grandpapa");
            grandParentObject.transform.position = centerOfParent;
            var parentObject = new GameObject("Papa");
            parentObject.AddComponent<Thingamabob>();
            parentObject.transform.SetParent(grandParentObject.transform);
            parentObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            foreach (var prem in prems)
            {
                prem.transform.SetParent(parentObject.transform);
                Material theMatWeWant = null;
                string theMatWeHave = prem.GetComponent<MeshRenderer>().material.name;
                foreach (var mat in objectMaterials) {
                    if (theMatWeHave.Contains(mat.name))
                    {
                        theMatWeWant = mat;
                        break;
                    }
                }
                prem.GetComponent<MeshRenderer>().material = theMatWeWant;
            }
        }
    }
}
