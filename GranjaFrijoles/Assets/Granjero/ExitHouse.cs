using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ExitHouse : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		transform.GetChild(1).transform.gameObject.SetActive(true);
		return TaskStatus.Success;
	}
}