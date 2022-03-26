using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorStick : MonoBehaviour
{
    private SelectorTool parent;
    
    private void OnTriggerEnter(Collider other) {
        parent.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other) {
        parent.TriggerExit(other);
    } 
}

