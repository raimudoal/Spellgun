using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveAxes = Vector2.zero;
    public float Distance = 3f;
    private Vector3 OrigPos = Vector3.zero;
    void Start()
    {
        OrigPos = transform.position;
    }
    void Update()
    {
        transform.position = OrigPos + MoveAxes * Mathf.PingPong(Time.time, Distance);
    }


}
