using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics layer filter")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;
    
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original) {
        return original.Where(item =>
            mask == (mask | (1 << item.gameObject.layer))
            ).ToList();
    }
}
