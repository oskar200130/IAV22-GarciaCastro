using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Despierto : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return (bool)GetComponent<BehaviorTree>().GetVariable("Dormido").GetValue() ? TaskStatus.Failure : TaskStatus.Success;
	}
}