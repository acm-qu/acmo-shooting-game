using UnityEngine;

public class BugChaser : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool destroyOnHit = true;

    private PlayerHealth targetHealth;

    private void Start()
    {
        if (target == null)
        {
            var found = FindFirstObjectByType<PlayerHealth>();
            if (found != null) target = found.transform;
        }
        if (target != null) targetHealth = target.GetComponentInParent<PlayerHealth>();
    }

    private void Update()
    {
        if (target == null || (targetHealth != null && targetHealth.IsDead)) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f) return;

        transform.rotation = Quaternion.LookRotation(dir);
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponentInParent<PlayerHealth>();
        if (health == null) return;

        health.TakeDamage(1);
        if (destroyOnHit) Destroy(gameObject);
    }
}