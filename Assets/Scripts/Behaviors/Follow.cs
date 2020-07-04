using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Follow")]
public class Follow : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - agent.transform.position;
    }
}
