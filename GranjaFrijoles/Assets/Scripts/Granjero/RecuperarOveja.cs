using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class RecuperarOveja : Action
{
    NavMeshAgent agent;
    GameObject dest;
    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        dest = GameManager.instance.getPoints().GetComponent<Places>().dentroEstablo;
    }

    public override TaskStatus OnUpdate()
    {
        if (!GameManager.instance.getLobo().GetComponent<Lobo>().TieneOveja())
            return TaskStatus.Failure;

        if (agent.enabled)
            agent.SetDestination(dest.transform.position);
        if (Vector3.SqrMagnitude(transform.position - dest.transform.position) < 1.0f)
        {
            agent.SetDestination(transform.position);
            if (transform.childCount > 2)
            {
                transform.GetChild(2).SetParent(null);
                dest = GameManager.instance.getPoints().GetComponent<Places>().puertaValla;
                agent.SetDestination(dest.transform.position);
            }
            else
            {
                dest.GetComponent<Animator>().SetBool("Open", false);
                GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().doorOpen = false;
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Running;
    }
}