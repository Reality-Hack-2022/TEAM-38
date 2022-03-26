using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHandler : MonoBehaviour
{
    public GameObject palletToDisable;

    public void OnToggleClicked()
    {
        bool toggleState = gameObject.GetComponent<Interactable>().IsToggled;
        palletToDisable.SetActive(!toggleState);
    }
}
