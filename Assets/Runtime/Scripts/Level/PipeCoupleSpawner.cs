using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCoupleSpawner : MonoBehaviour
{
    [SerializeField] private Pipe bottomPipePrefab;
    [SerializeField] private Pipe topPipePrefab;

    [SerializeField] private float maxGapSize = 1;
    [SerializeField] private float minGapSize = 1;
    [SerializeField] private float maxGapCenter = 2;
    [SerializeField] private float minGapCenter = 1;

    private Pipe bottomPipe;
    private Pipe topPipe;


    private void Awake()
    {
        SpawnPipes();
    }
    public void SpawnPipes()
    {   
        bottomPipe = Instantiate(bottomPipePrefab, transform.position, Quaternion.identity, transform);
        topPipe = Instantiate(topPipePrefab, transform.position, Quaternion.identity, transform);
        SetPipePosition();       

    }
    public void SetPipePosition()
    {
        float gapPosY = transform.position.y + Random.Range(-minGapCenter, maxGapCenter);
        float gapSize = Random.Range(minGapSize, maxGapSize);

        Vector3 bottomPipePos = bottomPipe.transform.position;
        bottomPipePos.y = (gapPosY - gapSize * 0.5f) - (bottomPipe.Head.y - bottomPipe.transform.position.y);
        bottomPipe.transform.position = bottomPipePos;

        Vector3 topPipePos = topPipe.transform.position;

        topPipePos.y = (gapPosY + gapSize * 0.5f) + (topPipe.transform.position.y - topPipe.Head.y);
        topPipe.transform.position = topPipePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        DrawGap(transform.position + Vector3.up * maxGapCenter);
        DrawGap(transform.position - Vector3.up * minGapCenter);

    }
    private void DrawGap(Vector3 position)
    {
        Gizmos.DrawCube(position, Vector3.one * 0.5f);
        Gizmos.DrawLine(position - Vector3.up * maxGapSize * 0.5f, position + Vector3.up * maxGapSize * 0.5f);
    }


}
