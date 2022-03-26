using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thingamabob : MonoBehaviour
{
    public enum AnimationType { NoLoop, Loop, Boomerang };
    public enum SpawnType { Bumper, Interval };

    // Basic
    public GameObject modelParent;
    public static GameObject palette;
    public static GameObject score;

    // Animation
    public GameObject animationParent;
    public AnimationType animationType = AnimationType.NoLoop;

    // Spawn
    public GameObject prefabToSpawn;
    public int spawnInterval = 1;

    // Touching
    public GameObject touchTargetPrefab;
    public bool destroyOnTouch = false;
    public int scoreChangeOnTouch = 0;

    public void AddAnimation(GameObject animationParent) {
        this.animationParent = animationParent;
        animationParent.transform.parent = transform.parent;
    }

    public void RemoveAnimation() {
        GameObject.Destroy(animationParent);
    }

    public void AddPrefabToSpawn(GameObject objectToSpawn) {
        prefabToSpawn = GameObject.Instantiate(objectToSpawn);
        prefabToSpawn.transform.position = transform.position;
        prefabToSpawn.transform.rotation = transform.rotation;
        prefabToSpawn.transform.parent = transform.parent;
        prefabToSpawn.SetActive(false);
    }

    public void RemovePrefabToSpawn() {
        GameObject.Destroy(prefabToSpawn);
    }
}
