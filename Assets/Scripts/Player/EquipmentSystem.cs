using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    void Start() => currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);

    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
        Debug.Log("Weapon drawn");
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
        Debug.Log("Weapon sheathed");
    }

    public void StartDealDamage() =>
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();

    public void EndDealDamage() =>
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
}