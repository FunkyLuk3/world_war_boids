using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private float cooldown_time;
    private float active_time;
    
    enum ability_state
    {
        ready,
        active,
        cooldown
    }

    ability_state state = ability_state.ready;
    public KeyCode key;

    void Update()
    {
        switch (state)
        {
            case ability_state.ready:
                if (Input.GetKeyDown(key))
                {
                    ability.Activate(gameObject);
                    state = ability_state.active;
                    active_time = ability.active_time;
                }
                break;
            case ability_state.active:
                if (active_time > 0)
                {
                    active_time -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject);
                    state = ability_state.cooldown;
                    cooldown_time = ability.cooldown_time;
                }
                break;
            case ability_state.cooldown:
                if (cooldown_time > 0)
                {
                    cooldown_time -= Time.deltaTime;
                }
                else
                {
                    state = ability_state.ready;
                }
                break;
        }
    }
}
