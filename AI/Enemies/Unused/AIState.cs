//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIState : MonoBehaviour
//{

//    //Variables 
//    UnityEngine.AI.NavMeshAgent nm;
//    public Transform target;

//    public float distanceThreshold = 10f;

//    public enum AIStateE { idle, chasing };

//    int stateOfAI = 0;
    
//    //public AIState aiState;


//    void Start()
//    {
//        nm = GetComponent<UnityEngine.AI.NavMeshAgent>();
//        // Determine the AI's starting state
//        StartCoroutine(Think());
//        target = GameObject.FindGameObjectWithTag("Player").transform;
//    }


//    IEnumerator Think()
//    {
//        while (true)
//        {
//            switch (stateOfAI)
//            {
//                // If the enemy is far enough, start idle
//                case 0:
//                    {
//                        float dist = Vector3.Distance(target.position, transform.position);
//                        if (dist < distanceThreshold)
//                        {
//                            stateOfAI = 0;
//                        }
//                        nm.SetDestination(transform.position);
//                        break;
//                    }
//                // If the enemy is close enough, start chasing
//                case 1:
//                    {
//                        float dist = Vector3.Distance(target.position, transform.position);
//                        if (dist < distanceThreshold)
//                        {
//                            stateOfAI = 1;
//                        }
//                        nm.SetDestination(transform.position);
//                    }
//                    break;
//                default:
//                    break;

//            }

//            nm.SetDestination(target.position);
//            yield return new WaitForSeconds(1f);
//        }
//    }



//}
