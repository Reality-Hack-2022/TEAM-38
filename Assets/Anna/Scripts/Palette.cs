using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    // The user-created "game object" currently selected
    private GameObject selectedObject;

    // UI elements

    public GameObject animatorTool;
    public GameObject playStopButtons;
    
    public GameObject prefabToSpawnSelectorTool;
    public GameObject prefabToSpawnSlot;
    public GameObject deletePrefabToSpawnButton;

    public GameObject touchTargetPrefabSelectorTool;
    public GameObject touchTargetPrefabSlot;
    public GameObject deleteTouchTargetPrefabButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAnimation(GameObject animation) {
        selectedObject.GetComponent<Thingamabob>().AddAnimation(animation);
        RefreshAnimationViz();
    }

    public void RemoveAnimation() {
        selectedObject.GetComponent<Thingamabob>().RemoveAnimation();
        RefreshAnimationViz();
    }

    public void RefreshAnimationViz() {
        bool hasAnimation = selectedObject.GetComponent<Thingamabob>().animationParent != null;
        animatorTool.SetActive(!hasAnimation);
        playStopButtons.SetActive(hasAnimation);
    }

    public void AddPrefabToSpawn(GameObject objectToSpawn) {
        selectedObject.GetComponent<Thingamabob>().AddPrefabToSpawn(objectToSpawn);
        RefreshPrefabToSpawnViz();
    }

    public void RemovePrefabToSpawn() {
        selectedObject.GetComponent<Thingamabob>().RemovePrefabToSpawn();
        RefreshPrefabToSpawnViz();
    }

    public void RefreshPrefabToSpawnViz() {
        GameObject prefabToSpawn = selectedObject.GetComponent<Thingamabob>().prefabToSpawn;
        bool hasPrefabToSpawn = prefabToSpawn != null;

        // Clear any old data from the slot
        foreach (Transform child in prefabToSpawnSlot.transform) {
            GameObject.Destroy(child.gameObject);
        }

        if (hasPrefabToSpawn) {
            GameObject prefabToSpawnPreview = GameObject.Instantiate(prefabToSpawn);
            prefabToSpawnPreview.transform.position = prefabToSpawnSlot.transform.position;
            prefabToSpawnPreview.transform.rotation = prefabToSpawnSlot.transform.rotation;
            //prefabToSpawnPreview.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            prefabToSpawnPreview.transform.parent = prefabToSpawnSlot.transform;
        }
        prefabToSpawnSelectorTool.SetActive(!hasPrefabToSpawn);
        prefabToSpawnSlot.SetActive(hasPrefabToSpawn);
        deletePrefabToSpawnButton.SetActive(hasPrefabToSpawn);   
    }
    
/*
    public void AddTouchTargetPrefab(GameObject touchTarget) {
        GameObject touchTargetPrefab = GameObject.Instantiate(touchTarget);
        selectedObject.GetComponent<Thingamabob>().touchTargetPrefab = touchTargetPrefab;
    }

    public void RemoveTouchTargetPrefab() {;
        selectedObject.GetComponent<Thingamabob>().RemoveTouchTargetPrefab();
        foreach (Transform child in touchTargetPrefabSlot.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }*/

    public void ChangeDestroyOnTouch(bool destroyOnTouch) {
        selectedObject.GetComponent<Thingamabob>().destroyOnTouch = destroyOnTouch;
    }

    public void ChangeScoreChangeOnTouch(int scoreChangeOnTouch) {
        selectedObject.GetComponent<Thingamabob>().scoreChangeOnTouch = scoreChangeOnTouch;
    }
}
