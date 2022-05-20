using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GoSleep : Action
{
    NavMeshAgent agent;
    GameObject house;
    
    public override void OnStart()
	{
        agent = GetComponent<NavMeshAgent>();
        house = GameManager.instance.getPoints().GetComponent<Places>().house;
    }

	public override TaskStatus OnUpdate()
	{
        if(agent.enabled)
            agent.SetDestination(house.transform.position);
        if (Vector3.SqrMagnitude(transform.position - house.transform.position) < 1.0f)
        {
            agent.SetDestination(transform.position);            
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Running;
    }
}