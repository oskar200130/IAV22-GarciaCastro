using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
public class AlimentarCerdos : Action
{
	NavMeshAgent agent;
	Animator anim;
	GameObject dest;
	GameObject pajaPrefab;
	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		Transform[] pajas = GameManager.instance.getScenario().GetComponent<Scenario>().paja;
		pajaPrefab = GameManager.instance.pajaPre;


		float minDist = Mathf.Infinity;
		int d = -1;
		for(int i=0; i<pajas.Length; i++)
        {
			if(Mathf.Abs(Vector3.SqrMagnitude(transform.position - pajas[i].transform.position)) < minDist)
            {
				minDist = Mathf.Abs(Vector3.Magnitude(transform.position - pajas[i].position));
				d = i;
			}
        }

		dest = pajas[d].gameObject;
	}

	public override TaskStatus OnUpdate()
	{
		if (agent.enabled)
			agent.SetDestination(dest.transform.position);

		if (Vector3.SqrMagnitude(transform.position - dest.transform.position) < 1.0f)
		{
			if (transform.childCount < 3)
			{
				dest.transform.SetParent(transform);
				dest = GameManager.instance.getPoints().GetComponent<Places>().cerdos;
			}
            else
            {
				GameManager.instance.destroyGameObject(transform.GetChild(2).gameObject);
				agent.SetDestination(transform.position);
				anim.SetBool("walking", false);
				return TaskStatus.Success;
            }
		}
		GetComponent<BehaviorTree>().GetVariable("CerdosAlimentados").SetValue(true);
		return TaskStatus.Running;
	}
}