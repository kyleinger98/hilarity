//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using StateStuff;

//public class ChasingState : State<AI>
//{

//    public Transform target = GameObject.FindGameObjectWithTag("Player").transform;

//    #region Background Stuff
//    // This static variable will only be declared once
//    private static ChasingState _instance;


//    // Private constructor means that this state can only be constructed within the state class itself
//    // The following means that the first and only time this gets constructed, we set _instance to one instance of the state
//    private ChasingState()
//    {
//        if (_instance != null)
//        {
//            return;
//        }

//        _instance = this;
//    }


//    // Declaring the constructor in this format enables us to do it once rather than every time
//    public static ChasingState Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                // Call the constructor
//                new ChasingState();
//            }

//            return _instance;
//        }
//    }
//    #endregion

//    #region State Changing and Effects
//    // The EnterState function is where the AI gains their first state
//    public override void EnterState(AI _owner)
//    {
//        Debug.Log("Entering Chasing State");
//    }

//    // The ExitState function is where the AI exits their state, this also occurs before updating state
//    public override void ExitState(AI _owner)
//    {
//        Debug.Log("Exiting Chasing State");

//    }

//    // The UpdateState function switches the AI's state
//    public override void UpdateState(AI _owner)
//    {
//        // Switch back to the idle state when the bool is false
//        if (!_owner.isChasing)
//        {
//            _owner.stateMachine.ChangeState(IdleState.Instance);
//        }

//        // Chasing code

//    }

    
//    #endregion





//}