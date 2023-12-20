using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireArrow : Ability
{
    [SerializeField] private float force;
    public GameObject arrow;
    private GameObject arrow_instance;
    private Rigidbody2D rigidbody;
    
    public override void Activate(GameObject parent)
    {
        
        Transform player_pointer = parent.transform.Find("Arrow");
        arrow_instance = Instantiate(arrow, player_pointer.position, player_pointer.rotation);

        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigidbody = arrow_instance.GetComponent<Rigidbody2D>();

        Vector2 direction = mouse_position - (Vector2)rigidbody.transform.position;
        rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Debug.Log(rigidbody.velocity);
        
                
    }

    public override void BeginCooldown(GameObject parent)
    {
        Destroy(arrow_instance);
    }
}
