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

    private void Awake()
    {
        myMachine = Instantiate(machine);
    }

    // Update is called once per frame
    void Update()
    {
        myMachine.Run();
    }
}
