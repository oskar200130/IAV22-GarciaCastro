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
		GameObject[] pajas = GameManager.instance.getScenario().GetComponent<Scenario>().paja;
		pajaPrefab = GameManager.instance.pajaPre;


		float minDist = Mathf.Infinity;
		int d = -1;
		for(int i=0; i<pajas.Length; i++)
        {
			if(pajas[i].transform.childCount > 0 && Mathf.Abs(Vector3.SqrMagnitude(transform.position - pajas[i].transform.GetChild(0).position)) < minDist)
            {
				minDist = Mathf.Abs(Vector3.Magnitude(transform.position - pajas[i].transform.position));
				d = i;
			}
        }

		dest = pajas[d].gameObject.transform.GetChild(0).gameObject;
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