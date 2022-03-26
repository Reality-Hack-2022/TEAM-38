using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMark : MonoBehaviour
{
    public bool startEnabled = false;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        isOn = startEnabled;
        GetComponent<Renderer>().enabled = startEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Toggle()
    {
        Debug.Log("Toggle check mark");
        isOn = !isOn;
        GetComponent<Renderer>().enabled = isOn;
    }
}
