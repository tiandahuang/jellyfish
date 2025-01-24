using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    [SerializeField] int length;
    [SerializeField] LineRenderer lineRend;
    [SerializeField] Vector3[] segmentPoses;
    private Vector3[] segmentV;

    [SerializeField] Transform targetDir;
    [SerializeField] float targetDist;
    [SerializeField] float smoothSpeed;

    void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    void Update()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
        }
        lineRend.SetPositions(segmentPoses);
    }
}
