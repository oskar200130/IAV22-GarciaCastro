using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AbrirPuerta : Action
{
	private GameObject fenceDoor;
	public override void OnStart()
	{
		fenceDoor = GameManager.instance.getScenario().GetComponent<Scenario>().fenceDoor;
	}

	public override TaskStatus OnUpdate()
	{
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().doorOpen = true;
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().horaDePastar = true;
		GameManager.instance.getSheepsCtrl().GetComponent<OvejasController>().eatToday = true;
		fenceDoor.GetComponent<Animator>().SetBool("Open", true);
		GameManager.instance.getPerro().GetComponent<Perro>().takeOut = true;
		return TaskStatus.Success;
	}
}