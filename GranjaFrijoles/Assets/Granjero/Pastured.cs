using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Pastured : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday ? TaskStatus.Success : TaskStatus.Failure;
	}
}