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
    private ObjPool<PipeCoupleSpawner> pipePool;
    PipeCoupleSpawner pipeSpawner;

    public Vector3 GroundLeftPos => groundLeftPos;
    public Vector3 GroundRightPos => groundRightPos;

    private Vector3 groundRightPos;
    private Vector3 groundLeftPos;


    [SerializeField] private int numberOfPipesSpawn = 2;
    [SerializeField] private float distBetweenPipes = 5;
    [SerializeField] private float initialDistanceToStartSpawn = 10;
    [SerializeField] private float minPlayerDistFromLastSpawn = 20;
    [SerializeField] private float minDistFromPlayerToDestroyPipe = 10;
    
    private Vector3 currentSpawnPos;

    [SerializeField]private List<PipeCoupleSpawner> pipeSpawnersList = new List<PipeCoupleSpawner>();
    private void Awake()
    {
        pipePool = GetComponent<PipePool>();
        groundLeftSprite = groundLeft.gameObject.GetComponent<SpriteRenderer>();
        groundRightSprite = groundRight.gameObject.GetComponent<SpriteRenderer>();
        groundRightPos = groundRight.transform.position;
        groundLeftPos = groundLeft.transform.position;     
        
    }
    private void Update()
    {
        PipeSpawnController();
        GroundController();

    }

    public void StartPipeSpawn()
    {
        currentSpawnPos = new Vector3(player.transform.position.x + initialDistanceToStartSpawn, 0, 0);
        PipeSpawn(numberOfPipesSpawn);
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
        if (pipeSpawnersList.Count > 0)
        {
            if (player.transform.position.x + minPlayerDistFromLastSpawn >= pipeSpawnersList[pipeSpawnersList.Count - 1].transform.position.x)
            {
                PipeSpawn(numberOfPipesSpawn);
            }
            if (player.transform.position.x - minDistFromPlayerToDestroyPipe >= pipeSpawnersList[0].transform.position.x)
            {
                pipePool.ReturnToPool(pipeSpawnersList[0]);
                pipeSpawnersList.RemoveRange(0, 1);
            }
        }
              
    }
    private void PipeSpawn(int numberOfPipes)
    {       
        for (int i = 0; i < numberOfPipes; i++)
        {
            pipeSpawner = pipePool.GetOrCreateFromPool(pipeSpawnerPrefab, currentSpawnPos, transform);
            pipeSpawner.transform.position = currentSpawnPos;
            pipeSpawner.SetPipePosition();
            currentSpawnPos.x += distBetweenPipes;
            pipeSpawnersList.Add(pipeSpawner);
        }        

    }

    
}