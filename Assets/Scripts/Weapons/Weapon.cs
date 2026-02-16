using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public WeaponManager.WeaponType weaponType;
    public string weaponName;
    public bool IsEquipped { get; private set; }
    
    [Header("Components")]
    private Collider2D weaponCollider;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public Animator anim;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Pickup(Transform owner, Transform holdPoint)
    {
        IsEquipped = true;
        
        if (rb != null)
        {
            rb.simulated = false;
            rb.velocity = Vector2.zero;
        }
        
        if (weaponCollider != null)
            weaponCollider.enabled = false;
        
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        anim.enabled = true;
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sortingLayerName = "Weapon";
        
        Highlight(false);
    }

    public void Drop(Vector2 dropPosition)
    {
        IsEquipped = false;
        
        transform.SetParent(null);
        
        if (rb != null)
        {
            rb.simulated = true;
            rb.position = dropPosition;
        }
        
        if (weaponCollider != null)
            weaponCollider.enabled = true;
        
        Highlight(false);
    }

    public void Highlight(bool enable)
    {
        //if (spriteRenderer != null)
        //{
            //if (enable)
                //spriteRenderer.color = Color.yellow;
            //else
                //spriteRenderer.color = Color.white;
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsEquipped && other.CompareTag("Player"))
        {
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.PickupWeapon(this);
            }
        }
    }

    void Update()
    {
        if(IsEquipped == false) anim.SetBool("isDropped", true);
        else if(IsEquipped == true) anim.SetBool("isDropped", false);
    }
}