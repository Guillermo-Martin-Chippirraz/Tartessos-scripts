using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Alert, Aggro, Flee }
    public EnemyState state = EnemyState.Idle;

    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float fleeThreshold = 20f; // huye si vida < 20%

    public float moveSpeed = 3f;
    public float damage = 10f;
    public float attackCooldown = 1.5f;

    private float lastAttack;
    private HealthSystem health;
    private AgroSystem agro;

    private void Start()
    {
        health = GetComponent<HealthSystem>();
        agro = GetComponent<AgroSystem>();

        health.OnDamage += (_) => agro.TriggerAgro();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // Cambios de estado
        if (health.currentHealth < fleeThreshold)
            state = EnemyState.Flee;
        else if (agro.hasAgro)
            state = EnemyState.Aggro;
        else if (dist < detectionRange)
            state = EnemyState.Alert;
        else
            state = EnemyState.Idle;

        // Comportamientos
        switch (state)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Alert:
                LookAtPlayer();
                break;

            case EnemyState.Aggro:
                AggroBehaviour(dist);
                break;

            case EnemyState.Flee:
                FleeBehaviour();
                break;
        }
    }

    private void AggroBehaviour(float dist)
    {
        LookAtPlayer();

        if (dist > attackRange)
            MoveTowards(player.position);
        else
            TryAttack();
    }

    private void FleeBehaviour()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        MoveTowards(transform.position + dir * 5f);
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );
    }

    private void LookAtPlayer()
    {
        Vector3 look = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(look);
    }

    private void TryAttack()
    {
        if (Time.time - lastAttack < attackCooldown) return;

        lastAttack = Time.time;
        player.GetComponent<HealthSystem>().TakeDamage(damage);
    }
}
