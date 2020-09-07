using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 _finishPosition = Vector3.zero;
    public float _speed = 0.5f;

    private Vector3 _startPos;
    private float _trackPercent = 0;
    private int _dirction = 1;


    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        _trackPercent += _dirction * _speed * Time.deltaTime;
        float x = (_finishPosition.x - _startPos.x) * _trackPercent + _startPos.x;
        float y = (_finishPosition.y - _startPos.y) * _trackPercent + _startPos.y;

        transform.position = new Vector3(x, y, _startPos.z);

        if (_dirction == 1 && _trackPercent > 0.9f || _dirction == -1 && _trackPercent < 0.1f)
        {
            _dirction *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _finishPosition);
    }
}
