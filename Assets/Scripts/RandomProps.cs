//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RandomProps : MonoBehaviour
{
    [Header("Settings")]
    public GameObject objectToSpawn;
    public Transform[] spawnPoints;
    [Tooltip("Where it should spawn")]
    public Transform parent;
    public bool canScale;
    public bool canRotate;

    private void Start()
    {
        if (spawnPoints.Length <= 0) return;

        foreach(Transform point in spawnPoints)
        {
            GameObject newGO = Instantiate(objectToSpawn, point.position, point.rotation);

            if (canScale)
            {
                float newScale = Random.Range(0.3f, 1f);
                newGO.transform.localScale -= new Vector3(newScale, newScale, newScale);
            }

            if (canRotate)
            {
                newGO.transform.Rotate(0, Random.Range(0f, 180f), 0, Space.Self);
            }

            newGO.transform.parent = parent.transform;
        }

        Destroy(this, 1f);
    }
}
