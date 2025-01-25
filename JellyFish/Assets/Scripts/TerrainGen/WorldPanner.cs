using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldPanner : MonoBehaviour
{
    public GameObject[] ChunkTypes;
    
    // max chunks t once
    public int MaxChunks; 
    
    // map of chunk ID to chunks 
    GameObject[] Chunks;

    
    private void Start()
    {
        // previous node... 

        // this instantiates a bunch of chunks one after each other... 
        // some game object (center position) 
        Transform ChunkSpawnNode = gameObject.transform; 
        for (int i = 0; i < MaxChunks; i++)
        {
            GameObject ChunkPrefab = ChunkTypes[Random.Range(0, ChunkTypes.Length - 1)];
            GameObject ChunkGameObject = Instantiate(ChunkPrefab, transform.position, Quaternion.identity);
            ChunkGameObject.transform.parent = ChunkSpawnNode;
            Chunk Chunk = ChunkGameObject.GetComponent<Chunk>();
            ChunkSpawnNode = Chunk.Node_Rear;
        }
    }

    void Update()
    {
        // for each chunk... 
            // is this chunk beyond player distance? 
            // if so move it to the rear. 
            // if its not, move it forward by some position += delta time * offset 
                // the offset here is measured by the input modality. 
    }
}
