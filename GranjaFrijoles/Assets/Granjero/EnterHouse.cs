using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnterHouse : Action
{
	public override TaskStatus OnUpdate()
	{
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday = false;
		transform.GetChild(1).transform.gameObject.SetActive(false);
		return TaskStatus.Success;
	}
}