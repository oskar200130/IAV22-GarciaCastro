using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CerrarPuerta : Action
{
	private GameObject fenceDoor;
	public override void OnStart()
	{
		fenceDoor = GameManager.instance.getScenario().GetComponent<Scenario>().puertaValla;
	}

	public override TaskStatus OnUpdate()
	{
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().doorOpen = false;
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().horaDePastar = false;
		fenceDoor.GetComponent<Animator>().SetBool("Open", false);
		return TaskStatus.Success;
	}
}