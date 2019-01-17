using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGR;

public class GGR_GameLoopController : MonoBehaviour
{
    public GGR_StateMachine machine;
    private GGR_StateMachine myMachine;

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
