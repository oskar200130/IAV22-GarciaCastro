using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDay : Conditional
{
	private GameManager g;
	public override void OnStart()
    {
		g = GameManager.instance;
    }
	public override TaskStatus OnUpdate()
	{
		if (g.getDay())
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}
}