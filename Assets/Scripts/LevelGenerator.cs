using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelObject;
    [SerializeField] private Vector2 nextObjectRespawnPosition;
    [SerializeField] private Transform player;

    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDestroy;



    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlatform();
        GeneratePlatform();

    }

    // Generates random platform to spawn when player reach boundary
    private void GeneratePlatform()
    {
        while (Vector2.Distance(player.transform.position, nextObjectRespawnPosition) < distanceToSpawn )
        {
            Transform part = levelObject[Random.Range(0, levelObject.Length)];

            Vector2 newPosition = new Vector2(nextObjectRespawnPosition.x - part.Find("StartPosition").position.x, 0);

            Transform newPart = Instantiate(part, newPosition, transform.rotation, transform);

            nextObjectRespawnPosition = newPart.Find("EndPosition").position;
        }
    }

    // Destroys platforms generated after they are off screen
    private void DestroyPlatform()
    {
        if (transform.childCount > 0) 
        {
            Transform objectToDestroy = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, objectToDestroy.transform.position) > distanceToDestroy ) 
            {
                Destroy(objectToDestroy.gameObject);                
            }
        }
    }
}
