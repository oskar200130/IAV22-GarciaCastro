using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Wander : Action
{
    private NavMeshAgent agente;
    [SerializeField]
    float distanceWander;
    public override void OnStart()
    {
        agente = GetComponent<NavMeshAgent>();
        GetComponent<Animator>().SetBool("walking", true);
    }

    public override TaskStatus OnUpdate()
	{
        wander();
		return TaskStatus.Running;
	}

    public void wander()
    {
        if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            if (Vector3.SqrMagnitude(transform.position - agente.destination) < 1.0f)
            {
                agente.SetDestination(getRandPoint());
            }
        }

    }

    private Vector3 getRandPoint()
    {
        NavMeshHit navHit;
        do
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distanceWander;
            randomDirection += gameObject.transform.position;
            NavMesh.SamplePosition(randomDirection, out navHit, distanceWander, NavMesh.AllAreas);
        }
        while ((1 << NavMesh.GetAreaFromName("Granja") & navHit.mask) == 0);
        return navHit.position;
    }
}