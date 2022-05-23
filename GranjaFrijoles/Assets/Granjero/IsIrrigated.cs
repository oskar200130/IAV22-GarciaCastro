using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsIrrigated : Conditional
{
	public override TaskStatus OnUpdate()
	{
		if (GameManager.instance.getScenario().GetComponent<Scenario>().orchard.GetComponent<Huerto_Behaviour>().estaRegado)
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}
}