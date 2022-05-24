using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PuertaAbierta : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().doorOpen && 
			!GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().horaDePastar ? TaskStatus.Success : TaskStatus.Failure;
	}
}