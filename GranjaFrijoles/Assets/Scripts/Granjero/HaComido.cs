using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HaComido : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return (bool)GetComponent<BehaviorTree>().GetVariable("Comido").GetValue() ? TaskStatus.Success : TaskStatus.Failure;
	}
}