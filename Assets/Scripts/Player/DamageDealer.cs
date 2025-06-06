using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    void Update()
    {
        if (!canDealDamage) return;

        int layerMask = 1 << 9;
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, weaponLength, layerMask))
        {
            var obj = hit.transform.gameObject;
            if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(obj))
            {
                enemy.TakeDamage(weaponDamage);
                hasDealtDamage.Add(obj);
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage() => canDealDamage = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}