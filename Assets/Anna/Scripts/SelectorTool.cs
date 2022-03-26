using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class SelectorTool : MonoBehaviour
{
    public Material regularMaterial;
    public Material recordingMaterial;
    //public Palette palette;

    private MLInput.Controller controller;

    // Pos & rot relative to palette
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public bool touchingController = false;
    private bool isRecording = false;
    private float timer = 0.0f;
    private GameObject objectToAnimate;
    private Transform objectToAnimateOriginalTransform;
    private Transform objectToAnimateOriginalParent;
    private GameObject stick;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);   
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
        GetComponent<Renderer>().material = regularMaterial;
        stick = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //objectToAnimate.transform.position = transform.position + transform.up * 0.
        if (isRecording) {
            if (timer == 0) {
                AddDot();
            } else if (timer > 0.5) {
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }

        CheckTrigger();
    }

    void CheckTrigger() {
        if (touchingController && controller.TriggerValue == 1.0f) {
            Debug.Log("trigger down");
            stick.transform.localRotation = Quaternion.Euler(0, 0, 0);
            stick.transform.position = controller.Position;
            stick.transform.rotation = controller.Orientation * Quaternion.Euler(45, 0, 0);
            stick.GetComponent<Collider>().isTrigger = true;
        } else {
            Reset();
            if (isRecording) {
                StopRecording();
            }
        }
    }

    private bool IsController(Collider other) {
        return other.tag == "Controller";
    }

    private bool IsObject(Collider other) {
        return other.gameObject.transform.parent.GetComponent<Thingamabob>() != null;
    }

    void StartRecording(GameObject obj) {
        GetComponent<Renderer>().material = recordingMaterial;
        objectToAnimate = obj;
        objectToAnimateOriginalParent = objectToAnimate.transform.parent;
        objectToAnimateOriginalTransform = objectToAnimate.transform;
        objectToAnimate.transform.parent = gameObject.transform;
        isRecording = true;
    }

    void StopRecording() {
        isRecording = false;
        GetComponent<Renderer>().material = regularMaterial;
        objectToAnimate.transform.parent = objectToAnimateOriginalParent;
        objectToAnimate.transform.position = objectToAnimateOriginalTransform.position;
        objectToAnimate.transform.rotation = objectToAnimateOriginalTransform.rotation;
    }

    void Reset() {
        stick.GetComponent<Collider>().isTrigger = false;
        stick.transform.localPosition = startingPosition;
        stick.transform.localRotation = startingRotation;
    }

    void AddDot() {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = objectToAnimate.transform.position;
        sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        sphere.transform.rotation = objectToAnimate.transform.rotation;
    }

    public void TriggerEnter(Collider other) {
        if (IsController(other)) {
            Debug.Log("touching controller");
            touchingController = true;
        } else if (IsObject(other)) {
            StartRecording(other.gameObject);
        }
    }

    public void TriggerExit(Collider other) {
        if (IsController(other)) {
            touchingController = false;
        }
    }
}