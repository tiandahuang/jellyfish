using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermittentSound : MonoBehaviour
{
    public AudioSource source;

    public float minWait = 8f;
    public float maxWait = 20f;
    public float countdown = -1f;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            if (countdown < 0f)
            {
                source.Play();
                countdown = Random.Range(minWait, maxWait);
            }
            else
            {
                countdown -= Time.deltaTime;
            }
        }
    }
}
