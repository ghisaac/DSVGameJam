using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGR;
using UnityEngine.AI;

public class DummyController : MonoBehaviour
{
    public GGR_StateMachine machine;
    public GGR_StateMachine myMachine;
    public Transform start, goal;

    private LineRenderer lr;

    private void Awake()
    {
        myMachine = Instantiate(machine);
    }

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(start.position, goal.position, NavMesh.AllAreas, path);
            lr.positionCount = path.corners.Length;
            lr.SetPositions(path.corners);
        }
        myMachine.Run();
    }
}
