using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject createPallet;
    public GameObject AnimatePallet;
    public bool isPlayMode = false;

    public void EnterCreateMode()
    {
        createPallet.SetActive(true);
    }

    public void ExitCreateMode()
    {
        if (createPallet.activeInHierarchy)
        {
            createPallet.SetActive(false);
            AnimatePallet.transform.position = createPallet.transform.position;
            AnimatePallet.transform.rotation = createPallet.transform.rotation;
        }
    }

    public void EnterAnimateMode()
    {
        AnimatePallet.SetActive(true);
    }

    public void ExitAnimateMode()
    {
        AnimatePallet.SetActive(false);
    }

    public void EnterPlayMode()
    {

        createPallet.SetActive(false);
        AnimatePallet.SetActive(false);

        isPlayMode = true;
    }
}
