using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HadEaten : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return (bool)GetComponent<BehaviorTree>().GetVariable("Eaten").GetValue() ? TaskStatus.Success : TaskStatus.Failure;
	}
}