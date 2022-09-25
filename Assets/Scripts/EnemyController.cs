using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;

    private WaypointData waypoint;

    public WaypointData Waypoint { get => waypoint; set => waypoint = value; }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (IsDead())
        {
            Waypoint.OnEnemyKilled(this);
            Destroy(gameObject);
        }
        else
        {
            UpdateHealthBar();
        }
    }

    bool IsDead()
    {
        return health <= 0;
    }

    void UpdateHealthBar()
    {
        healthBar.value = health;
    }
}
