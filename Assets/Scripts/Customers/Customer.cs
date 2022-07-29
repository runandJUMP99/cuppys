using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    // Transform currentTarget;
    // Transform playerTarget;
    // Transform exitTarget;
    // Target[] fetchedTargets;
    // Targets[] targets = {Targets.Order, Targets.Exit};
    bool hasNotOrdered = true;
    bool isOrdering = false;
    UIDialogue uiDialogue;

    Containers container;
    Flavors flavor;
    List<Toppings> toppings = new List<Toppings>();
    CustomerSpawner customerSpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner>();
        uiDialogue = FindObjectOfType<UIDialogue>();
        container = (Containers)Random.Range(0, System.Enum.GetValues(typeof(Containers)).Length - 1);
        flavor = (Flavors)Random.Range(0, System.Enum.GetValues(typeof(Flavors)).Length - 1);
        int totalToppings = System.Enum.GetValues(typeof(Toppings)).Length - 1;
        int totalToppingsChosen = Random.Range(0, totalToppings + 1);
        
        for (int i = 0; i < totalToppingsChosen; i++)
        {
            Toppings chosenTopping = (Toppings)Random.Range(0, totalToppings);
            while (toppings.Contains(chosenTopping))
            {
                chosenTopping = (Toppings)Random.Range(0, totalToppings);
            }
            toppings.Add(chosenTopping);
        }
    }

    void Start()
    {
        waveConfig = customerSpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FaceTarget();
        if (!isOrdering)
        {
            MoveToTarget();
        }
    }

    public Containers GetContainer()
    {
        return container;
    }

    public Flavors GetICFlavor()
    {
        return flavor;
    }

    public bool GetHasNotOrdered()
    {
        return hasNotOrdered;
    }

    public bool GetIsOrdering()
    {
        return isOrdering;
    }

    public List<Toppings> GetToppings()
    {
        return toppings;
    }

    void MoveToTarget()
    {
        distanceToTarget = Vector3.Distance(waypoints[waypointIndex].position, transform.position);

        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            // will use once animations have been added
            // GetComponent<Animator>().SetTrigger("move");
            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        }
        else
        {
            if (hasNotOrdered && !isOrdering)
            {
                if (waypointIndex == 1)
                {
                    isOrdering = true;
                    if (toppings.Count == 0)
                    {
                        uiDialogue.UpdateDialogue($"I would like {flavor} in a {container}, please!");
                    }
                    else if (toppings.Count == 1)
                    {
                        uiDialogue.UpdateDialogue($"I would like {flavor} with {toppings[0]} in a {container}, please!");
                    }
                    else if (toppings.Count == 2)
                    {
                        uiDialogue.UpdateDialogue($"I would like {flavor} with {toppings[0]} and {toppings[1]} in a {container}, please!");
                    }
                    else
                    {
                        uiDialogue.UpdateDialogue($"I would like {flavor} with {toppings[0]}, {toppings[1]}, and {toppings[2]} in a {container}, please!");
                    }
                }
                waypointIndex++;
            }
            else if (waypointIndex == waypoints.Count - 1 && distanceToTarget < navMeshAgent.stoppingDistance)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void FaceTarget()
    {
        Transform targetToFace;
        if (hasNotOrdered)
        {
            targetToFace = FindObjectOfType<Interact>().transform;
        }
        else
        {
            targetToFace = waypoints[waypointIndex];
        }

        Vector3 direction = (targetToFace.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    public void CompleteCustomerOrder()
    {
        hasNotOrdered = false;
        isOrdering = false;
        uiDialogue.UpdateDialogue("");
    }
}

// use this for cuppy
// [SerializeField] float chaseRange = 10f;
// bool isProvoked = false;

// void Update()
//     {
//         if (health.IsDead())
//         {
//             enabled = false;
//             navMeshAgent.enabled = false;
//         }

//         distanceToTarget = Vector3.Distance(target.position, transform.position);
        
//         if (isProvoked)
//         {
//             EngageTarget();
//         }
//         else if (distanceToTarget <= chaseRange)
//         {
//             isProvoked = true;
//         }
//     }

// use this for cuppy
//     void AttackTarget()
//     {
//         GetComponent<Animator>().SetBool("attack", true);
//     }

// use this for cuppy
//     void ChaseTarget()
//     {
//         GetComponent<Animator>().SetBool("attack", false);
//         GetComponent<Animator>().SetTrigger("move");
//         navMeshAgent.SetDestination(target.position);
//     }

// use this for cuppy
//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, chaseRange);
//     }
// }

// use this for cuppy
//     void EngageTarget()
//     {
//         FaceTarget();
//         if (distanceToTarget >= navMeshAgent.stoppingDistance)
//         {
//             ChaseTarget();
//         }
//         if (distanceToTarget <= navMeshAgent.stoppingDistance)
//         {
//             AttackTarget();
//         }
//     }

// use this for cuppy
//     void FaceTarget()
//     {
//         Vector3 direction = (target.position - transform.position).normalized;
//         Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
//         transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
//     }
