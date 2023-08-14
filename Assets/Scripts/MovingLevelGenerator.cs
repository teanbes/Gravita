using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingLevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelObject;
    [SerializeField] private Vector2 nextObjectRespawnPosition;
    [SerializeField] private Transform player;
    [SerializeField] private float objectSpeed = 10.0f;
    private Transform rndLevelObjectToSpawn;
    private Transform lasttLevelObjectSpawned;
    [SerializeField] private List<Transform> spawnedObjects;
    
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDestroy;

    void Start()
    {
        rndLevelObjectToSpawn = levelObject[Random.Range(0, levelObject.Length)];
        lasttLevelObjectSpawned = Instantiate(rndLevelObjectToSpawn, transform.position, transform.rotation);
        spawnedObjects.Add(lasttLevelObjectSpawned);
    }

    // Update is called once per frame
    void Update()
    {
 
        GeneratePlatform();
        MoveLevelPart();
        DestroyPlatform();
    }

    // Move level part
    private void MoveLevelPart()
    {
        if (spawnedObjects.Count > 0)
        {
            foreach (Transform transform in spawnedObjects)
            {
                Vector3 moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
                transform.position += moveDirection * -objectSpeed * Time.deltaTime;
            }
        }
    }

    // Generates random platform to spawn when player reach boundary
    private void GeneratePlatform()
    {
        while (Vector2.Distance(lasttLevelObjectSpawned.position, player.transform.position) < distanceToSpawn)
        {
            Transform part = levelObject[Random.Range(0, levelObject.Length)];
            Vector2 newPosition = new Vector2(lasttLevelObjectSpawned.position.x - part.Find("StartPosition").position.x, 0);
            Transform newPart = Instantiate(part, newPosition, part.rotation, transform);
            lasttLevelObjectSpawned = newPart.Find("EndPosition").transform;
            spawnedObjects.Add(newPart);
        }
    }

    // Destroys platforms generated after they are off screen
    private void DestroyPlatform()
    {
        if (spawnedObjects.Count > 0)
        {
            Transform objectToDestroy = spawnedObjects[0];

            if (Vector2.Distance(player.transform.position, objectToDestroy.transform.position) > distanceToDestroy)
            {
                Destroy(objectToDestroy.gameObject);
                spawnedObjects.Remove(objectToDestroy);
            }
        }
    }
}
