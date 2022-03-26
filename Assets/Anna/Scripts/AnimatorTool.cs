using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class AnimatorTool : MonoBehaviour
{
    public Material regularMaterial;
    public Material recordingMaterial;
    public Palette palette;
    public GameObject stick;

    private MLInput.Controller controller;

    // Pos & rot relative to palette
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public bool touchingController = false;
    private bool isRecording = false;
    private float spawnTimer = 0.0f;
    private GameObject objectToAnimate;
    private Transform objectToAnimateOriginalTransform;
    private Transform objectToAnimateOriginalParent;
    //private GameObject stick;
    private GameObject animationParent;
    private Vector3 initialPositionOffset;
    private Quaternion initialRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);   
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
        stick.GetComponent<Renderer>().material = regularMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording) {
            //Debug.Log("spawnTimer is " + spawnTimer);
            if (Mathf.Approximately(spawnTimer, 0.0f)) {
                AddDot();
                spawnTimer += Time.deltaTime;
            } else if (spawnTimer > 0.5) {
                spawnTimer = 0.0f;
                //Debug.Log("spawnTimer is " + spawnTimer + " so was reset");
            } else {
                spawnTimer += Time.deltaTime;
            }
        }

        CheckTrigger();
    }

    void CheckTrigger() {
        if (touchingController && controller.TriggerValue == 1.0f) {
            Debug.Log("trigger down");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.position = controller.Position;
            transform.rotation = controller.Orientation * Quaternion.Euler(45, 0, 0);
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
        Debug.Log("start recording");
        objectToAnimate = obj;
        objectToAnimateOriginalTransform = objectToAnimate.transform;
        stick.GetComponent<Renderer>().material = recordingMaterial;

        animationParent = new GameObject("Animation");
        animationParent.transform.position = objectToAnimateOriginalTransform.position;
        animationParent.transform.rotation = objectToAnimateOriginalTransform.rotation;
        animationParent.transform.parent = objectToAnimate.transform.parent;

        objectToAnimateOriginalParent = objectToAnimate.transform.parent;
        objectToAnimate.transform.parent = transform;
        
        isRecording = true;
    }

    void StopRecording() {
        isRecording = false;
        stick.GetComponent<Renderer>().material = regularMaterial;
        objectToAnimate.transform.parent = objectToAnimateOriginalParent;
        objectToAnimate.transform.position = objectToAnimateOriginalTransform.position;
        objectToAnimate.transform.rotation = objectToAnimateOriginalTransform.rotation;
        palette.AddAnimation(animationParent);
    }

    void Reset() {
        stick.GetComponent<Collider>().isTrigger = false;
        transform.localPosition = startingPosition;
        transform.localRotation = startingRotation;
    }

    void AddDot() {
        Debug.Log("Add Dot");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = animationParent.transform;
        sphere.transform.position = objectToAnimate.transform.position;
        sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        sphere.transform.rotation = objectToAnimate.transform.rotation;
    }

    public void TriggerEnter(Collider other) {
        if (IsController(other)) {
            Debug.Log("touching controller");
            touchingController = true;
        } else if (IsObject(other)) {
            StartRecording(other.gameObject.transform.parent.gameObject);
            //Debug.Log(other.gameObject.transform.parent.gameObject.name);
        }
    }

    public void TriggerExit(Collider other) {
        if (IsController(other)) {
            touchingController = false;
        }
    }
}
