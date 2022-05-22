using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GoTo : Action
{
    public int place = -1;
    NavMeshAgent agent;
    GameObject dest;
    
    public override void OnStart()
	{
        agent = GetComponent<NavMeshAgent>();
        dest = this.gameObject;
        switch (place)
        {
            case 0:
                dest = GameManager.instance.getPoints().GetComponent<Places>().house;
                break;
            case 1:
                dest = GameManager.instance.getPoints().GetComponent<Places>().orchard;
                break;
            case 2:
                break;
        }
    }

	public override TaskStatus OnUpdate()
	{
        if(agent.enabled)
            agent.SetDestination(dest.transform.position);
        if (Vector3.SqrMagnitude(transform.position - dest.transform.position) < 1.0f)
        {
            agent.SetDestination(transform.position);            
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Running;
    }
}