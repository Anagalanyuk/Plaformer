using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float _sped = 250.0f;
    public float _jump = 12.0f;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private Animator _anim;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        float deltaX = Input.GetAxis("Horizontal") * _sped * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        Vector3 min = _box.bounds.min;
        Vector3 max = _box.bounds.max;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;

        if(hit != null)
        {
            grounded = true;
        }
        _body.gravityScale = grounded && deltaX == 0 ? 0 : 1;
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _body.AddForce(Vector2.up * _jump, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if(hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }

        if(platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        if(!Mathf.Approximately(deltaX,0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX),1 ,1);
        }

        Vector3 pScale = Vector3.one;
        if(platform != null)
        {
            pScale = platform.transform.localScale;
        }

        if(deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x / pScale.y ,1);
        }
    }

}