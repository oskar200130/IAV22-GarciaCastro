using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HaRegado : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return (bool)GetComponent<BehaviorTree>().GetVariable("Regado").GetValue() ? TaskStatus.Success : TaskStatus.Failure;
	}
}