using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSlot
    {
        public string slotName;
        public Transform weaponHoldPoint;
        public GameObject currentWeapon;
        public WeaponType allowedWeaponType;
    }

    public enum WeaponType
    {
        Melee,
        Ranged,
        Any
    }

    [Header("Weapon Slots")]
    public List<WeaponSlot> weaponSlots = new List<WeaponSlot>();
    
    [Header("Pickup Settings")]
    public KeyCode pickupKey = KeyCode.E;
    public float pickupRange;
    public LayerMask weaponLayer;
    
    private Weapon currentHighlightedWeapon;
    private GameObject highlightedWeaponObject;

    void Update()
    {
        HandleWeaponPickup();
        HandleWeaponSwitch();
    }

    void HandleWeaponPickup()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange, weaponLayer);
        Weapon nearestWeapon = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            Weapon weapon = collider.GetComponent<Weapon>();
            if (weapon != null && !weapon.IsEquipped)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestWeapon = weapon;
                }
            }
        }

        if (nearestWeapon != null)
        {
            if (currentHighlightedWeapon != nearestWeapon)
            {
                if (currentHighlightedWeapon != null)
                    currentHighlightedWeapon.Highlight(false);
                
                nearestWeapon.Highlight(true);
                currentHighlightedWeapon = nearestWeapon;
                highlightedWeaponObject = nearestWeapon.gameObject;
            }

            if (Input.GetKeyDown(pickupKey))
            {
                PickupWeapon(nearestWeapon);
            }
        }
        else
        {
            if (currentHighlightedWeapon != null)
            {
                currentHighlightedWeapon.Highlight(false);
                currentHighlightedWeapon = null;
                highlightedWeaponObject = null;
            }
        }
    }

    void HandleWeaponSwitch()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SwitchToSlot(i);
            }
        }
    }

    public void PickupWeapon(Weapon weapon)
    {
        if (weapon == null || weapon.IsEquipped)
            return;

        WeaponSlot targetSlot = null;
        
        foreach (WeaponSlot slot in weaponSlots)
        {
            if (slot.allowedWeaponType == weapon.weaponType || slot.allowedWeaponType == WeaponType.Any)
            {
                targetSlot = slot;
                break;
            }
        }

        if (targetSlot == null)
        {
            foreach (WeaponSlot slot in weaponSlots)
            {
                if (slot.currentWeapon == null)
                {
                    targetSlot = slot;
                    break;
                }
            }
        }

        if (targetSlot == null)
        {
            foreach (WeaponSlot slot in weaponSlots)
            {
                if (slot.allowedWeaponType == weapon.weaponType || slot.allowedWeaponType == WeaponType.Any)
                {
                    targetSlot = slot;
                    break;
                }
            }
        }

        if (targetSlot != null)
        {
            if (targetSlot.currentWeapon != null)
            {
                DropWeapon(targetSlot);
            }

            EquipWeapon(weapon, targetSlot);
        }
        else
        {
            Debug.LogWarning("Нет подходящего слота для этого оружия!");
        }
    }

    void EquipWeapon(Weapon weapon, WeaponSlot slot)
    {
        weapon.Pickup(transform, slot.weaponHoldPoint);
        slot.currentWeapon = weapon.gameObject;
        
        SwitchToSlot(weaponSlots.IndexOf(slot));
    }

    void DropWeapon(WeaponSlot slot)
    {
        if (slot.currentWeapon != null)
        {
            Weapon weapon = slot.currentWeapon.GetComponent<Weapon>();
            if (weapon != null)
            {
                Vector2 dropPosition = (Vector2)transform.position + GetDropDirection();
                weapon.Drop(dropPosition);
            }
            slot.currentWeapon = null;
        }
    }

    Vector2 GetDropDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized * 1.5f;
    }

    void SwitchToSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= weaponSlots.Count)
            return;

        WeaponSlot slot = weaponSlots[slotIndex];
        
        foreach (WeaponSlot ws in weaponSlots)
        {
            if (ws.currentWeapon != null)
                ws.currentWeapon.SetActive(false);
        }

        if (slot.currentWeapon != null)
            slot.currentWeapon.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }

    public void AddWeaponSlot(string slotName, WeaponType weaponType, Transform holdPoint)
    {
        WeaponSlot newSlot = new WeaponSlot
        {
            slotName = slotName,
            weaponHoldPoint = holdPoint,
            currentWeapon = null,
            allowedWeaponType = weaponType
        };
        weaponSlots.Add(newSlot);
    }
}