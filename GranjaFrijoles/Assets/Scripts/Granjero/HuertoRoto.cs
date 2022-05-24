using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HuertoRoto : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return GameManager.instance.getScenario().GetComponent<Scenario>().huerto.GetComponent<Huerto_Behaviour>().estaPisado ? TaskStatus.Success : TaskStatus.Failure;
	}
}