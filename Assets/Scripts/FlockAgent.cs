using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock flock { get; private set; }
    public Collider2D AgentCollider { get; private set; }
    private float time = 0;
    public bool deleted;

    void Start() {
        AgentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity) {
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }

    public void Initialize(Flock flock) {
        this.flock = flock;
    }

    public void ParseContext(List<Transform> context) {
        List<Transform> friendlies = context.Where(item => item.parent.gameObject == transform.parent.gameObject).ToList();
        List<Transform> enemies = context.Where(item => item.parent.gameObject != transform.parent.gameObject).ToList();

        if (friendlies.Count < enemies.Count) {
            time += Time.deltaTime;
        }
        else {
            time = 0;
        }

        if (time > 1) {
            Flock enemyFlock = enemies.First().GetComponentInParent<Flock>();
            gameObject.SetActive(false);
            enemyFlock.Add(transform);
            deleted = true;
        }
    }
}
