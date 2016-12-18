using UnityEngine;
using System.Collections;

public class BirdControl : MonoBehaviour {

    public float m_jump_speed = 0.3f;
    public float m_rotation_ratio = 1f;

    private Animator _bird_control_animator;
    private Rigidbody2D _rigid_body;
    private Vector2 _born_pos;

    private bool _is_dead = false;
    public bool IsDead
    {
        get
        {
            return _is_dead;
        }
    }

    // Use this for initialization
    void Start () {
        _bird_control_animator = GetComponent<Animator>();
        _rigid_body = GetComponent<Rigidbody2D>();
        _born_pos = transform.localPosition;
        Reset();
    }
	
	// Update is called once per frame
	void Update () {


        if (IsDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        var v2 = _rigid_body.velocity;
        var v3 = transform.localEulerAngles;
        v3.z = v2.y * m_rotation_ratio;
        transform.localEulerAngles = v3;

    }

    // bird jump high
    public void Jump()
    {
        Rigidbody2D rigid_body = GetComponent<Rigidbody2D>();
        var v2 = rigid_body.velocity;
        v2.y = m_jump_speed;
        rigid_body.velocity = v2;
    }

    // bird die
    public void GoDie()
    {
        _is_dead = true;
        _bird_control_animator.SetBool("m_is_alive", false);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "pipe" || collisionInfo.gameObject.name == "floor")
        {
            GoDie();
        }
    }

    public void StartGame()
    {
        _rigid_body.gravityScale = 1;
    }

    public void Reset()
    {
        _bird_control_animator.SetBool("m_is_alive", true);
        _is_dead = false;
        transform.localPosition = _born_pos;
        _rigid_body.gravityScale = 0;
    }
}
