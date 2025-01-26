using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenBob : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(-.5f, .5f)] // Min: 0, Max: 10
    public float StartMovementXRange = 0;
    [Range(-.5f, .5f)] // Min: 0, Max: 10
    public float StartMovementYRange = 0;
    
    [Range(-.5f, .5f)] // Min: 0, Max: 10
    public float EndMovementXRange = 0;
    [Range(-.5f, .5f)] // Min: 0, Max: 10
    public float EndMovementYRange = 0;
    
    public float duration = 2.0f;
    void Start()
    {
        // Move between positionA and positionB infinitely
        Vector3 startPos = new Vector3(
            StartMovementXRange + transform.position.x, 
            StartMovementYRange + transform.position.y, 
            transform.position.z);
        
        transform.position = startPos;
        Vector3 endPos = new Vector3(
            EndMovementXRange + transform.position.x, 
            EndMovementYRange + transform.position.y, 
            transform.position.z);
        
        transform.DOMove(endPos, duration)
            .SetLoops(-1, LoopType.Yoyo) // Loop infinitely in a ping-pong manner
            .SetEase(Ease.InOutSine).SetId("TweenBob");    // Smooth easing for back-and-forth motion
    }

}
