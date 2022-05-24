using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SalirCasa : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		GetComponent<BehaviorTree>().GetVariable("Dormido").SetValue(false);
		GameManager.instance.deactivateGranjeroAudioTrigger();
		transform.GetChild(1).transform.gameObject.SetActive(true);
		return TaskStatus.Success;
	}
}