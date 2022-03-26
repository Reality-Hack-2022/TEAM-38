using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GrabObject : MonoBehaviour
{
    private MLInput.Controller controller;

    //Premitive Prefabs

    public static Material CurrentMateriall;

    public GameObject CubePremitivePrefab;
    public GameObject SpherePremitivePrefab;
    public GameObject CapsulePremitivePrefab;
    public GameObject CylinderPremitivePrefab;

    public TextMesh text;

    public Material collidingMaterial;
    public Material enabledMaterial;

    private bool isManupilating;
    private bool isColliding;
    private int collidingObjectID;
    private Transform manipulatingObject;
    private Vector3 diffVector;

    private bool isCreating;
    private string creatingObjectName;

    private bool gestureStarted;
    private MLInput.Controller.TouchpadGesture.GestureDirection gestureDirection;

    private GameObject newGameObject;

    public SceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);
        //MLInput.OnControllerButtonDown += MLInput_OnControllerButtonDown;
        MLInput.OnControllerButtonUp += MLInput_OnControllerButtonUp;
        MLInput.OnControllerTouchpadGestureEnd += MLInput_OnControllerTouchpadGestureEnd;
        MLInput.OnControllerTouchpadGestureContinue += MLInput_OnControllerTouchpadGestureContinue;
        MLInput.OnControllerTouchpadGestureStart += MLInput_OnControllerTouchpadGestureStart;
    }

    private void MLInput_OnControllerButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (isColliding)
        {
            var obj = manipulatingObject;
            sceneManager.ExitCreateMode();
            sceneManager.EnterAnimateMode();
        }
        else
        {
            sceneManager.ExitAnimateMode();
            sceneManager.EnterCreateMode();
        }
    }

    private void MLInput_OnControllerTouchpadGestureStart(byte controllerId, MLInput.Controller.TouchpadGesture touchpadGesture)
    {
        if (isManupilating)
        {
            Debug.Log(((int)touchpadGesture.Type).ToString());
            if (touchpadGesture.Type == MLInput.Controller.TouchpadGesture.GestureType.Tap)
            {
                Debug.Log(touchpadGesture.PosAndForce);
                if(touchpadGesture.PosAndForce.Value.x >0.5)
                    manipulatingObject.localScale += new Vector3(0.01f, 0, 0);
                else if (touchpadGesture.PosAndForce.Value.x < -0.5)
                    manipulatingObject.localScale -= new Vector3(0.01f, 0, 0);
                if (touchpadGesture.PosAndForce.Value.y > 0.5)
                    manipulatingObject.localScale += new Vector3(0f, 0.01f, 0);
                else if (touchpadGesture.PosAndForce.Value.y < -0.5)
                    manipulatingObject.localScale -= new Vector3(0f, 0.01f, 0);
            }
        }
    }

    private void MLInput_OnControllerTouchpadGestureContinue(byte controllerId, MLInput.Controller.TouchpadGesture touchpadGesture)
    {
        if (isManupilating)
        {
            
            if (touchpadGesture.Type == MLInput.Controller.TouchpadGesture.GestureType.RadialScroll)
            {
                switch (touchpadGesture.Direction)
                {
                    case MLInput.Controller.TouchpadGesture.GestureDirection.Clockwise:
                        if(manipulatingObject.localScale.z<10f)
                            manipulatingObject.localScale += new Vector3(0f, 0, 0.005f);
                        break;
                    case MLInput.Controller.TouchpadGesture.GestureDirection.CounterClockwise:
                        if (manipulatingObject.localScale.z > 0f)
                            manipulatingObject.localScale -= new Vector3(0f, 0, 0.005f);
                        break;
                }
            }
        }
    }

    private void MLInput_OnControllerTouchpadGestureEnd(byte controllerId, MLInput.Controller.TouchpadGesture touchpadGesture)
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (controller != null)
        {

            CheckTrigger();
            transform.position = controller.Position;
            transform.rotation = controller.Orientation;

            if (isManupilating)
            {
                manipulatingObject.position = transform.position + diffVector;
                manipulatingObject.rotation = transform.rotation;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding With: " + other.gameObject.tag);
        if (other.gameObject.tag == "PremNew" && !isManupilating)
        {
            manipulatingObject = other.gameObject.transform;
            diffVector = manipulatingObject.position - transform.position;
            isColliding = true;
            collidingObjectID = manipulatingObject.GetInstanceID();
        }
        else if (other.gameObject.tag == "Merged" && !isManupilating)
        {
            if (!sceneManager.isPlayMode)
            {
                manipulatingObject = other.gameObject.transform.parent.parent;
                diffVector = manipulatingObject.position - transform.position;
                isColliding = true;
                collidingObjectID = manipulatingObject.GetInstanceID();
            }
            else
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        else if (other.gameObject.tag == "Scoreboard" && !isManupilating)
        {
            manipulatingObject = other.gameObject.transform;
            diffVector = manipulatingObject.position - transform.position;
            isColliding = true;
            collidingObjectID = manipulatingObject.GetInstanceID();
        }

        if (other.gameObject.tag == "UI" && !isManupilating)
        {
            creatingObjectName = other.gameObject.name;
            isCreating = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PremNew" && collidingObjectID == other.gameObject.GetInstanceID())
        {
            isColliding = false;
        }
        if (other.gameObject.tag == "Merged" && collidingObjectID == other.gameObject.transform.parent.parent.GetInstanceID())
        {
            isColliding = false;
        }
        if (other.gameObject.tag == "Scoreboard")
        {
            isColliding = false;
        }
        if (other.gameObject.tag == "UI")
        {
            isCreating = false;
        }
    }

    void OnTriggerDown()
    {
        Debug.Log("IsColliding " + isColliding + " IsCreating " + isCreating + " IsManipulating " + isManupilating);
        if (isColliding)
        {
            isManupilating = true;
        }
        if (isCreating)
        {
            switch (creatingObjectName)
            {
                case "Cube":
                    newGameObject = Instantiate(CubePremitivePrefab, transform.position, transform.rotation);
                    break;
                case "Sphere":
                    newGameObject = Instantiate(SpherePremitivePrefab, transform.position, transform.rotation);
                    break;
                case "Capsule":
                    newGameObject = Instantiate(CapsulePremitivePrefab, transform.position, transform.rotation);
                    break;
                case "Cylinder":
                    newGameObject = Instantiate(CylinderPremitivePrefab, transform.position, transform.rotation);
                    break;
            }

            newGameObject.GetComponent<MeshRenderer>().material = CurrentMateriall;
            diffVector = new Vector3(0, 0, 0);
            manipulatingObject = newGameObject.transform;
            isManupilating = true;
        }
    }

    void OnTriggerUp()
    {
        //Can do isColliding = false; but will lead to issues
            isManupilating = false;
            isCreating = false;
    }

    bool isTriggered = false;
    void CheckTrigger()
    {
        if (controller.TriggerValue > 0.3f && !isTriggered)
        {
            isTriggered = true;
            OnTriggerDown();
        }
        else if(controller.TriggerValue <0.3f && isTriggered)
        {
            isTriggered = false;
            OnTriggerUp();
        }
    }

}
