using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RuidoGallina : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return GameManager.instance.getAudiosActivosGranjero() == 3 ? TaskStatus.Success : TaskStatus.Failure;
	}
}