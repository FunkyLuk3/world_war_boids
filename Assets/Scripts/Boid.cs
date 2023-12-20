using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private static List<Boid> list_boids = new List<Boid>();

    [SerializeField] private float attraction_range;
    [SerializeField] private float repulsion_range;
    [SerializeField] private float direction_range;

    [SerializeField] private bool is_player;
    [SerializeField] private float acceleration;

    private Rigidbody2D rigidbody;

    void Start()
    {
        list_boids.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!is_player)
        {
            Vector2 movement = AttractionRule();
            movement += RepulsionRule();
            movement += DirectionRule();

            rigidbody.velocity += movement.normalized * acceleration * Time.deltaTime;
        }
    }

    private Vector2 AttractionRule()
    {
        Vector2 other_position = Vector2.zero;
        int count = 0;

        foreach (Boid boid in list_boids)
        {
            if (!boid.Equals(this) && Vector3.Distance(boid.transform.position, transform.position) < attraction_range)
            {
                other_position += (Vector2)boid.transform.position;
                count++;
            }
        }

        if (count == 0)
        {
            return other_position - (Vector2)transform.position;
        }
        return other_position/count - (Vector2)transform.position;
    }

    private Vector2 RepulsionRule()
    {
        Vector2 repulsion = Vector2.zero;
        int count = 0;

        foreach (Boid boid in list_boids)
        {
            if (!boid.Equals(this) && Vector3.Distance(boid.transform.position, transform.position) < repulsion_range)
            {
                repulsion += (Vector2)(transform.position - boid.transform.position);
                count++;
            }
        }

        if (count == 0)
        {
            return repulsion;
        }
        return repulsion/count;
    }

    private Vector2 DirectionRule()
    {
        Vector2 acceleration = Vector2.zero;
        int count = 0;

        foreach (Boid boid in list_boids)
        {
            if (!boid.Equals(this) && Vector3.Distance(boid.transform.position, transform.position) < direction_range)
            {
                acceleration += (Vector2)(boid.transform.forward * boid.acceleration);
                count++;
            }
        }

        if (count == 0)
        {
            return acceleration;
        }
        return acceleration/count;
    }
}


