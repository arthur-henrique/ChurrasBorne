using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private enum State
    {
        Normal,
        Rolling,
    }
    [SerializeField]
    private LayerMask dashLayerMask;

    public float speed;
    private float rollSpeed;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Vector3 rollDirection;
    private Vector3 lastMovedDirection;
    private Vector2 moveVelocity;

    private bool isDashing;
    private State state;
    // Start is called before the first frame update

    private void Awake()
    {
        state = State.Normal;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                moveVelocity = moveInput.normalized * speed;
                direction = moveInput.normalized;
                if (moveInput.x != 0 || moveInput.y != 0)
                    lastMovedDirection = direction;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isDashing = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDirection = lastMovedDirection;
                    rollSpeed = 35f;
                    state = State.Rolling;
                }
                break;
            case State.Rolling:
                float rollSpeedMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;

                float rollSpeedMinimun = 10f;
                if (rollSpeed < rollSpeedMinimun)
                    state = State.Normal;
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb.velocity = moveVelocity;
                if (isDashing)
                {
                    float dashAmount = 10f;
                    Vector3 dashPosition = transform.position + lastMovedDirection * dashAmount;
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, lastMovedDirection, dashAmount, dashLayerMask);
                    if (raycastHit2D.collider != null)
                    {
                        dashPosition = raycastHit2D.point;
                    }
                    rb.MovePosition(dashPosition);
                    isDashing = false;
                }
                break;
            case State.Rolling:
                rb.velocity = rollDirection * rollSpeed;
                break;
        }

    }
}
