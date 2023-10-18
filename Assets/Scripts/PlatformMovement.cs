using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float movementRange = 2f;
    private float _startPosition;
    public float speed = 2f;
    
    void Start()
    {
        _startPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        var position = transform.position;
        position.x = _startPosition + Mathf.PingPong(Time.time * speed, movementRange);
        transform.position = position;
    }
}
