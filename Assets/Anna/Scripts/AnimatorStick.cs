using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStick : MonoBehaviour
{
    private AnimatorTool parent;
    
    void Start() {
        parent = transform.parent.gameObject.GetComponent<AnimatorTool>();
    }

    private void OnTriggerEnter(Collider other) {
        parent.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other) {
        parent.TriggerExit(other);
    } 
}
