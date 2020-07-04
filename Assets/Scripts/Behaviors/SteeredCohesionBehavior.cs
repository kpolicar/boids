using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    private Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        if (context.Count == 0)
            return Vector2.zero;
        
        Vector2 cohesionMove = Vector2.zero;
        
        var filteredContext = filter == null ? context : filter.Filter(agent, context);
        foreach (var item in filteredContext) {
            cohesionMove += (Vector2) item.position;
        }
        cohesionMove /= context.Count;

        cohesionMove -= (Vector2) agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        
        return cohesionMove;
    }
}
