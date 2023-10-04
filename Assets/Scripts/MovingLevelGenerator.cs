using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingLevelGenerator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform startPart;
    [SerializeField] private Transform[] levelObject;

    [SerializeField] private Transform player;
    [SerializeField] public float objectSpeed = 10.0f;
    private Transform firstLevelToSpawn;
    private Transform lasttLevelObjectSpawned;
    [SerializeField] private List<Transform> spawnedObjects;
    
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDestroy;

    void Start()
    {
        firstLevelToSpawn = startPart;
        lasttLevelObjectSpawned = Instantiate(firstLevelToSpawn, transform.position, transform.rotation);
        spawnedObjects.Add(lasttLevelObjectSpawned);
        ExtraPartToSpawn(0);
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
                transform.position += Vector3.left * objectSpeed * Time.deltaTime;
            }
        }
    }

    // Generates random platform to spawn when player reach boundary
    private void GeneratePlatform()
    {
        int currentDifficulty = GameManager.instance.difficulty;
        List<Transform> validParts = new List<Transform>();

        // Iterate through levelObject array and add parts with matching difficulty
        foreach (Transform part in levelObject)
        {
            LevelPart levelPart = part.GetComponent<LevelPart>();
            if (levelPart != null && levelPart.difficulty == currentDifficulty)
            {
                validParts.Add(part);
            }
        }

        while (Vector2.Distance(lasttLevelObjectSpawned.position, player.transform.position) < distanceToSpawn)
        {
            if (validParts.Count > 0)
            {
                // Randomly select a valid level part of the current difficulty
                Transform selectedPart = validParts[Random.Range(0, validParts.Count)];
                Vector2 newPosition = new Vector2(lasttLevelObjectSpawned.position.x - selectedPart.Find("StartPosition").position.x, 0);
                Transform newPart = Instantiate(selectedPart, newPosition, selectedPart.rotation, transform);
                lasttLevelObjectSpawned = newPart.Find("EndPosition").transform;
                spawnedObjects.Add(newPart);
            }
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

    private void ExtraPartToSpawn(int partIndex)
    {
        Transform part = levelObject[partIndex];
        Vector2 newPosition = new Vector2(lasttLevelObjectSpawned.position.x - part.Find("StartPosition").position.x, 0);
        Transform newPart = Instantiate(part, newPosition, part.rotation, transform);
        lasttLevelObjectSpawned = newPart.Find("EndPosition").transform;
        spawnedObjects.Add(newPart);
    }
}
