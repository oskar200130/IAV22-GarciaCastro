using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HanPastado : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday ? TaskStatus.Success : TaskStatus.Failure;
	}
}