using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenuButton : MonoBehaviour
{
    public bool enabledByDefault;
    public Material disabledMaterial;
    public Material collidingMaterial;
    public Material enabledMaterial;

    private ArrayList otherButtons = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = enabledByDefault ? enabledMaterial : disabledMaterial;
        GameObject menuObject = transform.parent.parent.gameObject;
        foreach(ToggleMenuButton button in menuObject.GetComponentsInChildren<ToggleMenuButton>())
        {
            if (button != this) {
                otherButtons.Add(button);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (IsControllerTip(other)) {
            //GetComponent<Renderer>().material = collidingMaterial;
            Enable();
        }
    }

    private bool IsControllerTip(Collider other) {
        return other.tag == "ControllerTip";
    }

    void Enable() {
        GetComponent<Renderer>().material = enabledMaterial;
        foreach (ToggleMenuButton button in otherButtons) {
            button.Disable();
        }
    }

    void Disable() {
        GetComponent<Renderer>().material = disabledMaterial;
    }

    /*private void OnTriggerExit(Collider other) {
        if (IsControllerTip(other)) {
            Enable();
        }
    }*/
}
