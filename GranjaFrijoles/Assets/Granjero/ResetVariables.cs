using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetVariables : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		GetComponent<BehaviorTree>().GetVariable("Comido").SetValue(false);
		GetComponent<BehaviorTree>().GetVariable("Regado").SetValue(false);
		return TaskStatus.Success;
	}
}