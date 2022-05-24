using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GoTo : Action
{
    public int place = -1;
    NavMeshAgent agent;
    Animator anim;
    GameObject dest;
    
    public override void OnStart()
	{
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
                dest = GameManager.instance.getPoints().GetComponent<Places>().fenceDoor;
                break;
            case 3:
                dest = GameManager.instance.getPoints().GetComponent<Places>().meetZone;
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
            anim.SetBool("walking", false);
            return TaskStatus.Success;
        }
        else
        {
            anim.SetBool("walking", true);
            return TaskStatus.Running;
        }
    }
}