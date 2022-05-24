using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetVariables : Action
{
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		GameManager.instance.resetPajas();
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday = false;
		GetComponent<BehaviorTree>().GetVariable("Comido").SetValue(false);
		GetComponent<BehaviorTree>().GetVariable("Regado").SetValue(false);
		GetComponent<BehaviorTree>().GetVariable("CerdosAlimentados").SetValue(false);
		return TaskStatus.Success;
	}
}