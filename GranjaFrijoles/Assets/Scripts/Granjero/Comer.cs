using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Comer : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		GetComponent<BehaviorTree>().GetVariable("Comido").SetValue(true);
		return TaskStatus.Success;
	}
}