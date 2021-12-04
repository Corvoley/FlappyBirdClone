using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PipeCoupleSpawner pipeSpawnerPrefab;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject groundRight;
    [SerializeField] private GameObject groundLeft;
    private SpriteRenderer groundLeftSprite;
    private SpriteRenderer groundRightSprite;

    private Vector3 groundRightPos;
    private Vector3 groundLeftPos;


    [SerializeField] private int numberOfPipesSpawn = 2;
    [SerializeField] private float distBetweenPipes = 5;
    [SerializeField] private float initialSpawnPosX = 10;
    [SerializeField] private float minPlayerDistFromLastSpawn = 20;
    [SerializeField] private float minDistFromPlayerToDestroyPipe = 10;
    
    private Vector3 currentSpawnPos;

    private List<PipeCoupleSpawner> pipeSpawnersList = new List<PipeCoupleSpawner>();
    private void Awake()
    {
        groundLeftSprite = groundLeft.gameObject.GetComponent<SpriteRenderer>();
        groundRightSprite = groundRight.gameObject.GetComponent<SpriteRenderer>();
        groundRightPos = groundRight.transform.position;
        groundLeftPos = groundLeft.transform.position;
        currentSpawnPos = new Vector3(initialSpawnPosX, 0, 0);
        PipeSpawn(numberOfPipesSpawn);
    }
    private void Update()
    {
        PipeSpawnController();
        GroundController();

    }

    private void GroundController()
    {
        if (player.transform.position.x >= groundRightPos.x)
        {
            groundLeftPos.x = groundRightPos.x + groundRightSprite.bounds.size.x;
            groundLeft.transform.position = groundLeftPos;
        }
        if (player.transform.position.x >= groundLeftPos.x)
        {
            groundRightPos.x = groundLeftPos.x + groundLeftSprite.bounds.size.x;
            groundRight.transform.position = groundRightPos;
        }
    }

    private void PipeSpawnController()
    {
        if (player.transform.position.x + minPlayerDistFromLastSpawn >= pipeSpawnersList[pipeSpawnersList.Count - 1].transform.position.x)
        {
            PipeSpawn(numberOfPipesSpawn);
        }
        if (player.transform.position.x - minDistFromPlayerToDestroyPipe >= pipeSpawnersList[0].transform.position.x)
        {
            Destroy(pipeSpawnersList[0].gameObject);
            pipeSpawnersList.RemoveRange(0, 1);
        }        
    }
    private void PipeSpawn(int numberOfPipes)
    {       
        for (int i = 0; i < numberOfPipes; i++)
        {
            PipeCoupleSpawner pipeSpawner = Instantiate(pipeSpawnerPrefab, currentSpawnPos, Quaternion.identity, transform);
            currentSpawnPos.x += distBetweenPipes;
            pipeSpawnersList.Add(pipeSpawner);
        }        

    }

    
}
