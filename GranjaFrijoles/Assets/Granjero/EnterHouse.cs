using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnterHouse : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		transform.GetChild(1).transform.gameObject.SetActive(false);
		return TaskStatus.Success;
	}
}