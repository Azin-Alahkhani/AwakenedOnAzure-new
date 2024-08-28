using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followSubject;

    Vector3 positioOffsets;
    public float smoothSpeed = 0.125f;
    void Start()
    {
        positioOffsets =   followSubject.transform.position - transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followSubject.transform.position - positioOffsets;

        Quaternion desiredRotation = Quaternion.Euler(0, followSubject.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);

    }
}
