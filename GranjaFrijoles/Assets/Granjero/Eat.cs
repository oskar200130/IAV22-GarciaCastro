using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Eat : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		GetComponent<BehaviorTree>().GetVariable("Eaten").SetValue(!(bool)GetComponent<BehaviorTree>().GetVariable("Eaten").GetValue());
		return TaskStatus.Success;
	}
}