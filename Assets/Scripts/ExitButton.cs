using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    public SceneManager sceneManager;

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            sceneManager.EnterPlayMode();

        }
    }
}
