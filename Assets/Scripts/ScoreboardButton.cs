using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardButton : MonoBehaviour
{

    public Material collidingMaterial;
    public Material enabledMaterial;
    public GameObject scoreboard;
    private bool instanciated = false;

    public void OnTriggerEnter(Collider other)
    {
        if(!instanciated)
            GetComponent<Renderer>().material = collidingMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!instanciated) {
            instanciated = true;
            GetComponent<Renderer>().material = enabledMaterial;
            if (other.gameObject.tag == "Controller")
            {
                Instantiate(scoreboard, Camera.main.transform.position + (Camera.main.transform.forward * 1f)+ new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            }
        }
            
    }
}
