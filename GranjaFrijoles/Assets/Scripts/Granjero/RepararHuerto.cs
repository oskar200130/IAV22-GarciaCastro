using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RepararHuerto : Action
{
	private GameObject orch;
	public override void OnStart()
	{
		orch = GameManager.instance.getScenario().GetComponent<Scenario>().huerto;
	}

	public override TaskStatus OnUpdate()
	{
		Huerto_Behaviour hb = orch.GetComponent<Huerto_Behaviour>();
		if (hb.estaPisado)
			hb.setSeco();
		return TaskStatus.Success;
	}
}