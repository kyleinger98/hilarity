//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using StateStuff;

//public class IdleState : State<AI>
//{


//    #region Background Stuff
//    // This static variable will only be declared once
//    private static IdleState _instance;

//    // Private constructor means that this state can only be constructed within the state class itself
//    // The following means that the first and only time this gets constructed, we set _instance to one instance of the state
//    private IdleState()
//    {
//        if(_instance != null)
//        {
//            return;
//        }

//        _instance = this;
//    }

//    // Declaring the constructor in this format enables us to do it once rather than every time
//    public static IdleState Instance
//    {
//        get
//        {
//            if(_instance == null)
//            {
//                // Call the constructor
//                new IdleState();
//            }

//            return _instance;
//        }
//    }
//    #endregion

    

//    #region State Changing and Effects
//    // The EnterState function is where the AI gains their first state
//    public override void EnterState(AI _owner)
//    {
//        Debug.Log("Entering Idle State");
//    }

//    // The ExitState function is where the AI exits their state, this also occurs before updating state
//    public override void ExitState(AI _owner)
//    {
//        Debug.Log("Exiting Idle State");

//    }

//    // The UpdateState function switches the AI's state
//    public override void UpdateState(AI _owner)
//    {
//        // Wander around
        

//        // If the owner's chasing bool is true
//        if (_owner.isChasing)
//        {
//            // Change state to chasing 
//            _owner.stateMachine.ChangeState(ChasingState.Instance);
//        }

        

//        // ADD CODE FOR STATE HERE

//    }
//    #endregion
//}
