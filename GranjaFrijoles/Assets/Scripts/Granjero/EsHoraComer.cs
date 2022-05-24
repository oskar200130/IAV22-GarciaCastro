using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EsHoraComer : Conditional
{
	private GameManager g;
	public override void OnStart()
    {
		g = GameManager.instance;
    }
	public override TaskStatus OnUpdate()
	{
		if (g.getMeet())
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}
}