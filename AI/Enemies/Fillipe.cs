using UnityEngine;
using UnityEngine.AI;

public class Fillipe : AI
{
    #region Variables
    // Variables

    // Attacks
    public float AIThrowRange;
    [HideInInspector]
    public float AINextThrow;
    public float AIThrowCoolDown;
    public float AIThrowDamage;
    public GameObject fillipeHand;
    public GameObject food;

    public float AIChargeRange;
    [HideInInspector]
    public float AINextCharge;
    public float AIChargeCoolDown;
    public float AIChargeDamage;

    // States
    public bool isThrowing;
    public bool isCharging;
    #endregion

    override public void Start()
    {
        // Setup
        target = PlayerTracker.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // States
        isIdle = true;
        isChasing = false;
        isAttacking = false;
        isThrowing = false;
        isCharging = false;

        SetKinematic(true);

    }


    override public void Update()
    {
        // Set animator depending on state
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isChasing", isChasing);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isThrowing", isThrowing);
        animator.SetBool("isCharging", isCharging);

        // Distance between Player and AI
        float distFromPlayer = Vector3.Distance(target.transform.position, transform.position);

        // Look at player unless idle
        if (!isIdle && !isDead)
        {
            // Face the player
            FaceTarget();
        }

        // If ready to chase
        if (((distFromPlayer < AIChaseRange || takenDamage) && distFromPlayer > AIAttackRange) && !isDead)
        {
            // Chase player
            isIdle = false;
            isAttacking = false;
            isChasing = true;
            agent.SetDestination(target.position);
        }

        // If ready to attack
        if (Time.time > AINextAttack && distFromPlayer <= AIAttackRange && !isDead)
        {

            isChasing = false;
            isAttacking = true;

            // Attack the player
            EnemyAttack();

            // Reset the nextAttack time to a new point in the future
            AINextAttack = Time.time + AIAttackSpeed;
        }

        #region Cooldown Abilities
        // If ready to throw
        if (Time.time > AINextThrow && distFromPlayer <= AIThrowRange && !isDead)
        {
            // Throw attack the player via animation event
            animator.SetTrigger("isThrowing");
            // Reset the nextThrow time to a new point in the future
            AINextThrow = Time.time + AIThrowCoolDown;
        }

        //// If ready to charge
        //if (Time.time > AINextCharge && distFromPlayer <= AIChargeRange && !isDead)
        //{
        //    animator.SetTrigger("isCharging");
        //    // Charge attack the player
        //    ChargeAttack();
        //    // Reset the nextCharge time to a new point in the future
        //    AINextCharge = Time.time + AIChargeCoolDown;
        //}

        // Stop movement when in certain animations (e.g. throwing)
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("stopMovement"))
        {
            agent.isStopped = true;
        }
        else agent.isStopped = false;
        #endregion
    }

    override public void EnemyAttack()
        {
            // Used for raycast
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 origin = transform.position;

            if (Physics.Raycast(origin, forward, out RaycastHit hit, AIAttackRange))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                hit.transform.gameObject.SendMessage("playerTakeDamage", AIAttackDamage);
                }
            }
        }

        public void ThrowAttack()
        {
        // Instantiate food
        Instantiate(food, fillipeHand.transform.position, Quaternion.identity);
        }

        public void ChargeAttack()
        {
        Debug.Log("fillipe charges");
        }
        

        // Range indicators
        override public void OnDrawGizmosSelected()
        {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AIChaseRange);
        Gizmos.DrawWireSphere(transform.position, AIAttackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AIThrowRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AIChargeRange);
        }

}
