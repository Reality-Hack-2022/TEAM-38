using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteMovement
 : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject rotationObject;
    public int lerpSpeed;
    public float yOffset;
    public float tiltTowardUser;
    public float rotationThreshold;
    public float positionThreshold;

    private Vector3 currentForward;
    private bool lerpingRotation;

    private Vector3 currentPosition;
    private bool lerpingPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        currentForward = transform.forward;
        transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y + yOffset, transform.position.z);
        currentPosition = transform.position;
        rotationObject.transform.rotation = Quaternion.Euler(0, 90, -tiltTowardUser);
    }

    // Update is called once per frame
    void Update()
    {
        // Position

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraPosition = mainCamera.transform.position;

        Vector3 currentPositionXZ = new Vector3(currentPosition.x, 0, currentPosition.z);
        Vector3 cameraPositionXZ = new Vector3(cameraPosition.x, 0, cameraPosition.z);
        Vector3 transformPositionXZ = new Vector3(transform.position.x, 0, transform.position.z);

        float distanceFromCurrent = (cameraPositionXZ - currentPositionXZ).magnitude;
        float distanceFromTransform = (cameraPositionXZ - transformPositionXZ).magnitude;

        if (!lerpingPosition && distanceFromCurrent > positionThreshold) {
            lerpingPosition = true;
            Debug.Log("start pos lerp");
        } else if (lerpingPosition && distanceFromTransform < 0.1) {
            lerpingPosition = false;
            currentPosition = transform.position;
            Debug.Log("end pos lerp");
        }

        if (lerpingPosition) {
            //Debug.Log("lerping position " + distanceFromTransform);
        }

        float targetPositionY = Mathf.Lerp(transform.position.y, mainCamera.transform.position.y + yOffset, Time.deltaTime * lerpSpeed);
        Vector3 targetPositionXZ = lerpingPosition ? cameraPositionXZ : currentPositionXZ;
        Vector3 targetPosition = new Vector3(targetPositionXZ.x, targetPositionY, targetPositionXZ.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

        // Rotation

        Vector3 cameraForwardXZ = Vector3.Normalize(new Vector3(cameraForward.x, 0, cameraForward.z));
        Vector3 currentForwardXZ = new Vector3(currentForward.x, 0, currentForward.z);
        Vector3 transformForwardXZ = new Vector3(transform.forward.x, 0, transform.forward.z);
        
        float angleFromCurrent = Vector3.Angle(currentForwardXZ, cameraForwardXZ);
        float angleFromTransform = Vector3.Angle(transformForwardXZ, cameraForwardXZ);

        if (!lerpingRotation && angleFromCurrent > rotationThreshold) {
            lerpingRotation = true;
            Debug.Log("start rot lerp");
        } else if (lerpingRotation && angleFromTransform < 1) {
            lerpingRotation = false;
            currentForward = mainCamera.transform.forward;
            Debug.Log("end rot lerp");
        }

        if (lerpingRotation) {
            //Debug.Log("lerping rotation");
        }

        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, transform.eulerAngles.z);

        if (lerpingRotation) {
            //float yRot = Mathf.Lerp(transform.eulerAngles.y, mainCamera.transform.eulerAngles.y, Time.deltaTime * lerpSpeed);
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRot, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
