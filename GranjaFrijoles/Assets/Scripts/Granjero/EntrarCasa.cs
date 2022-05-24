using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EntrarCasa : Action
{
	public override TaskStatus OnUpdate()
	{
		GetComponent<BehaviorTree>().GetVariable("Dormido").SetValue(true);
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday = false;
		transform.GetChild(1).transform.gameObject.SetActive(false);
		return TaskStatus.Success;
	}
}