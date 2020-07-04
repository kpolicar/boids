using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FlockBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
