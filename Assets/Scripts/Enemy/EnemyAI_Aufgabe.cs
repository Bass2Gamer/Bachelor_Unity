using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class EnemyAI : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1.2f;
    [SerializeField] float aggroRange = 8f;
 
    GameObject player;
    NavMeshAgent agent;
    EnemyAnimator enemyAnimator;
 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        
        // TODO: Player-Referenz finden (Tipp: GameObject.FindGameObjectWithTag("Player"))
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // TODO: Distanz zum Spieler berechnen (Tipp: Vector3.Distance)
        
        // TODO: Gegner zum Spieler schauen lassen wenn in Reichweite (Tipp: transform.LookAt)
        
        // TODO: Attack-Verhalten implementieren
        HandleAttack(0f); // Parameter durch berechnete Distanz ersetzen
        
        // TODO: Chase-Verhalten implementieren  
        HandleChase(0f); // Parameter durch berechnete Distanz ersetzen
    }

    private void HandleAttack(float distanceToPlayer)
    {
        // TODO: Attack-Logik implementieren
        // - Timer für Angriffs-Cooldown
        // - Prüfen ob Spieler in Angriffsreichweite
        // - Animation auslösen (enemyAnimator.TriggerAttack())
    }

    private void HandleChase(float distanceToPlayer)
    {
        // TODO: Chase-Logik implementieren
        // - Spieler verfolgen wenn in Aggro-Reichweite aber nicht in Angriffsreichweite
        // - NavMeshAgent verwenden (agent.SetDestination())
        // - Stoppen wenn in Angriffsreichweite
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        enemyAnimator.TriggerDamage();

        if (health <= 0)
        {
            Die();
        }
    }
 
    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
