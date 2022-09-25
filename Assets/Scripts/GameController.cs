using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, ISubject
{
    [Header("Game parameters")]
    [SerializeField] private int shellSpeed;
    [SerializeField] private int shellDamage;

    private List<IObserver> _waypointObservers = new List<IObserver>();

    private GameObject[] _waypoints;
    private WaypointData _activeWayPoint;

    public static GameController Instance { get; private set; }
    public int ShellSpeed { get => shellSpeed; }
    public int ShellDamage { get => shellDamage; }

    int waypointIndex;

    #region Mono Behaviour
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Init();

        RegisterWaypoints();
    }
    #endregion

    public void OnStartClicked()
    {
        NextWaypoint();
    }

    void Init()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
            Debug.LogError("No player found!");
        else
            Subcsribe(player);

        //subscribe other...
    }

    void RegisterWaypoints()
    {
        _waypoints = GameObject.FindGameObjectsWithTag(GameTags.WAYPOINT);
    }

    void NextWaypoint()
    {
        waypointIndex += 1;

        if(waypointIndex > _waypoints.Length - 1)
        {
            EndGame();
            return;
        }

        _activeWayPoint = _waypoints[waypointIndex].GetComponent<WaypointData>();
        Notify();

        Debug.Log("Starting new waypoint...");
    }

    void EndGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnWayPointCompleted() => NextWaypoint();

    #region Observer Pattern
    public void Subcsribe(IObserver observer)
    {
        _waypointObservers.Add(observer);
    }

    public void Uncubscribe(IObserver observer)
    {
        _waypointObservers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _waypointObservers)
        {
            observer.Update(this, _activeWayPoint);
        }
    }
    #endregion
}
