using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockBehavior behavior;
    public FlockAgent agentPrefab;
    public FlockAgent dragonPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();

    [Range(10, 500)]
    public int startingCount = 100;
    private const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 50f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;

    void Start() {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;

        for (int i = 0; i < startingCount; i++) {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * (startingCount * AgentDensity),
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

        if (dragonPrefab != null) {
            FlockAgent newAgent = Instantiate(
                dragonPrefab,
                Random.insideUnitCircle * (startingCount * AgentDensity),
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Dragon";
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    public void Add(Transform destination) {
        FlockAgent newAgent = Instantiate(
            agentPrefab,
            destination.position,
            destination.rotation,
            transform
        );
        newAgent.name = "Agent takover";
        newAgent.Initialize(this);
        agents.Add(newAgent);
    }

    void Update() {
        foreach (var agent in agents) {
            var context = GetNearbyObjects(agent);
            var move = (Vector2) agent.transform.up + behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) {
                move = move.normalized * maxSpeed;
            }

            agent.ParseContext(context);
            agent.Move(move);
            if (agent.deleted) 
                agents.Remove(agent);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent) {
        var contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        return (from collider in contextColliders
                where collider != agent.AgentCollider
                select collider.transform)
            .ToList();
    }
}
