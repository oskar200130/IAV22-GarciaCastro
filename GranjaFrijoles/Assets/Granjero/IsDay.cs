using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDay : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}

	public bool isDay()
    {
		return true;
    }
}