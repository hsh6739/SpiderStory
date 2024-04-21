using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrailPivot : MonoBehaviour
{
    public float rotateSpeed;

    Vector3 rotation;


    void Start()
    {
        rotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.z += Time.deltaTime * rotateSpeed;


        transform.localEulerAngles = rotation;

    }
}
