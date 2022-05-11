
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    Rigidbody rb;
    public LayerMask whatIsPlayer, WhatIsGround;

    //patrolng
    public Vector3 WalkPoint;
    bool walkPointSet;
    public float walPointRange;

    //Attack
    public float timeToAttack;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerRange, playerRangeToAttack;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        playerRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerRangeToAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (GetComponent<Enemy>().enemyHP <= 0)
        {
            Dead();
            return;
        }
        else
        {
            if (!playerRange && !playerRangeToAttack) Patroling();
            if (playerRange && !playerRangeToAttack) Chaseing();
            if (playerRangeToAttack && playerRange) Attacking();
        }
    }
    public void Patroling()
    {
        if (!walkPointSet) searchwalkPoint();

        if (walkPointSet)
            agent.SetDestination(WalkPoint);

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        if (distanceToWalkPoint.magnitude < 2f)
            walkPointSet = false;

    }
public void Dead()
    {
        agent.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        return;
    }
    public void Attacking()
    {

        transform.LookAt(player);
    }
    public void Chaseing()
    {
        agent.SetDestination(player.position);
        walkPointSet = false;
    }
    
    void searchwalkPoint()
    {
        float randomZ = Random.Range(-walPointRange, walPointRange);
        float randomX = Random.Range(-walPointRange, walPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            walkPointSet = true;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(WalkPoint, 1);
    }

}
