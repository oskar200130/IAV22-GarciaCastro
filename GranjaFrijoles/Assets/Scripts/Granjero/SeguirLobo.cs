using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class SeguirLobo : Action
{
    NavMeshAgent agent;
    Animator anim;
    GameObject dest;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        dest = GameManager.instance.getLobo();
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.enabled)
            agent.SetDestination(dest.transform.position);

        NavMeshHit navHit;
        Vector3 position = dest.transform.position;
        NavMesh.SamplePosition(position, out navHit, 0, NavMesh.AllAreas);

        if (Vector3.SqrMagnitude(transform.position - dest.transform.position) < 1.0f)
        {
            agent.SetDestination(transform.position);
            dest.transform.GetChild(2).SetParent(transform);
            anim.SetBool("walking", false);
            return TaskStatus.Success;
        }
        else if ((1 << NavMesh.GetAreaFromName("CampoEscape") & navHit.mask) != 0)
        {
            agent.SetDestination(transform.position);
            dest.GetComponent<Lobo>().visto = false;
            anim.SetBool("walking", false);
            return TaskStatus.Failure;
        }
        else
        {
            dest.GetComponent<Lobo>().visto = true;
            anim.SetBool("walking", true);
            return TaskStatus.Running;
        }
    }
}