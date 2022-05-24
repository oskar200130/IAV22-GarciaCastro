using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class LoboZonaPeligro : Conditional
{
    public override TaskStatus OnUpdate()
    {
        NavMeshHit navHit;
        Vector3 position = GameManager.instance.getLobo().transform.position;
        NavMesh.SamplePosition(position, out navHit, 0, NavMesh.AllAreas);

        if ((((1 << NavMesh.GetAreaFromName("Campo") & navHit.mask) != 0) || ((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) != 0) 
            || ((1 << NavMesh.GetAreaFromName("Granja") & navHit.mask) != 0)) || GameManager.instance.getLobo().GetComponent<Lobo>().TieneOveja())
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}