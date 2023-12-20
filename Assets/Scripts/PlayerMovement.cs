using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float normal_acceleration;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movement_input;

    public Transform arrow;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        acceleration = normal_acceleration;
    }

    void Update()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        arrow.up = Quaternion.Euler(0, 0, 90) * (mouse_position - (Vector2)transform.position).normalized;
    }

    private void FixedUpdate()
    {
        movement_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidbody.velocity += movement_input * acceleration * Time.fixedDeltaTime;
    }
}
