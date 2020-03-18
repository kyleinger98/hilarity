//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// Remember to use this namespace in other scripts related to AI
//namespace StateStuff
//{


//    // Declare 'T' as 'StateMachine's prevelent type
//    // This means that anything other than AI can also use this state machine
//    public class StateMachine<T>
//    {
//        // Declare the starting state for the AI
//        public State<T> currentState { get; private set; }
//        // 'Owner' is the object using these states (the ai)
//        public T Owner;

//        // The following code will turn AI into the owner of the state when the constructor is called
//        // We pass into the parameter of _o 
//        public StateMachine(T _o)
//        {
//            Owner = _o;
//            currentState = null;
//        }

//        // This function exits the current state, and enters the new one
//        public void ChangeState(State<T> _newstate)
//        {
//            // This checks there is a state to exit first
//            if (currentState != null)
//                currentState.ExitState(Owner);
//            currentState = _newstate;
//            currentState.EnterState(Owner);
//        }

//        // The 'Update' function checks that the AI has a currentState at all time
//        public void Update()
//        {
//            if (currentState != null)
//                currentState.UpdateState(Owner);
//        }

//    }
        
//        // All of our state files are going to be based on the following class:
//        // This means that the state files will know to reference the 'AI' script
//        public abstract class State<T>
//        {
//            // This function allows us to trigger something when the AI enters state
//            public abstract void EnterState(T _owner);
//            // This function allows us to trigger something when the AI exits a state
//            public abstract void ExitState(T _owner);
//            // This function allows us to trigger something when the AI enters a certain state
//            public abstract void UpdateState(T _owner);
//        }


//    }

