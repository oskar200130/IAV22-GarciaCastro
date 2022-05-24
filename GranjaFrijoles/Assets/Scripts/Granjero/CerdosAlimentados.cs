using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CerdosAlimentados : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return (bool)GetComponent<BehaviorTree>().GetVariable("CerdosAlimentados").GetValue() ? TaskStatus.Success : TaskStatus.Failure;
	}
}