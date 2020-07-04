using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    [Range(0f, 10f)]
    public float radius = 0.5f;
    
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        if (context.Count == 0)
            return Vector2.zero;

        context = FilterContext(agent, context);
        
        Vector2 avoidanceMove = Vector2.zero;
        var filteredContext = filter == null ? context : filter.Filter(agent, context);
        foreach (var item in filteredContext) {
            avoidanceMove += (Vector2) (agent.transform.position - item.position);
        }

        if (context.Any())
            avoidanceMove /= context.Count();

        return avoidanceMove;
    }

    private List<Transform> FilterContext(FlockAgent agent, List<Transform> context) {
        return context.Where(item =>
            Vector2.SqrMagnitude(item.position - agent.transform.position) < radius
        ).ToList();
    }
}
