using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EsDia : Conditional
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