using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public static PipeManager Instance { get; private set; }

    public List<Pipe> pipesList;
    public float pipeSpawnDelay;
    public float pipeDelayTimer;
    public bool spawnPipes;

    float prevRanNum;
    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (spawnPipes)
        {
            if (pipeDelayTimer < pipeSpawnDelay)
            {
                pipeDelayTimer += Time.deltaTime;
            }
            else
            {
                SpawnPipe();
                pipeDelayTimer = 0;
            }
        }
    }

    public void SpawnPipe()
    {
        foreach (Pipe pipe in pipesList)
        {
            if (!pipe.transform.gameObject.activeInHierarchy)
            {
                pipe.transform.position = new Vector3(11, GetRandomYPos(), 0);
                pipe.transform.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void StopPipes()
    {
        spawnPipes = false;
        foreach (Pipe pipe in pipesList)
        {
            pipe.move = false;
            
        }
    }

    private float GetRandomYPos()
    {

        float ranNum = 0;

        if (prevRanNum > 2.2 || prevRanNum < -2.2) ranNum = Random.Range(-2f, 2f);
        else ranNum = Random.Range(-2.5f, 2.5f);

        prevRanNum = ranNum;
        return ranNum;
    }

    public void StartGame()
    {
        SpawnPipe();
        spawnPipes = true;
    }
}
