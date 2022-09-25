using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemies;

    private void Start()
    {
        SetupEnemies();
    }

    void SetupEnemies()
    {
        foreach (var e in enemies)
        {
            e.Waypoint = this;
        }
    }

    public void OnEnemyKilled(EnemyController killed)
    {
        enemies.Remove(killed);

        if(enemies.Count == 0)
            GameController.Instance.OnWayPointCompleted();
    }

    public Vector3 PlayerPosition
    {
        get => transform.Find("PlayerPos").position;
    }
}
