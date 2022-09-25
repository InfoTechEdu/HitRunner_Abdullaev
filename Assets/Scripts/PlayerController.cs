using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    IDLE,
    RUN,
    FIGHT
}
public class PlayerController : MonoBehaviour, IObserver
{
    [SerializeField] private Transform _fireTransform;

    NavMeshAgent agent;
    PlayerState state;
    Animator animController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();

        state = PlayerState.IDLE;
        animController.SetTrigger("IDLE");
    }

    void Update()
    {
        if (state == PlayerState.IDLE)
            return;

        //need some refactoring
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            animController.SetTrigger("IDLE");
            animController.ResetTrigger("RUN");
            state = PlayerState.FIGHT;
        }
        else
        {
            animController.SetTrigger("RUN");
            animController.ResetTrigger("IDLE");
            state = PlayerState.RUN;
        }

        //need some refactoring
        if (state == PlayerState.FIGHT && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                _fireTransform.LookAt(hit.point);
            }
            else
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane * 50));
                _fireTransform.LookAt(worldPoint);
                _fireTransform.rotation = Quaternion.Euler(_fireTransform.rotation.eulerAngles.x, _fireTransform.rotation.eulerAngles.y, 0);
                _fireTransform.Rotate(-Camera.main.transform.eulerAngles.x, 0, 0);
            }
            
            Fire();
        }
    }

    public void Fire()
    {
        Rigidbody shell = ShellPool.SharedInstance.GetPooledShell();
        if (shell != null)
        {
            shell.transform.position = _fireTransform.position;
            shell.transform.rotation = _fireTransform.rotation;
            shell.gameObject.SetActive(true);
            shell.velocity = GameController.Instance.ShellSpeed * _fireTransform.forward;
        }
    }

    public void ToNextWaypoint(Vector3 targetPos)
    {
        agent.destination = targetPos;
        state = PlayerState.RUN;
        animController.SetTrigger("RUN");
    }

    public void Update(ISubject subject, object data)
    {
        WaypointData wd = data as WaypointData;
        ToNextWaypoint(wd.PlayerPosition);
    }
}
