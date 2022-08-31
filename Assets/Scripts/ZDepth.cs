using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepth : MonoBehaviour
{
    Transform t;
    public float f = 0.001f;
    [SerializeField] bool stationary = true;

    private void Start()
    {
        t = transform;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = pos.y * f;
        transform.position = pos;

        if(stationary)
        {
            Destroy(this);
        }
    }
}
