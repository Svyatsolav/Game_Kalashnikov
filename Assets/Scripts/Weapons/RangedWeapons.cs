using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedWeapons : MonoBehaviour
{
    [SerializeField] public enum WeaponType {Pistol, Cannon};
    [SerializeField] public WeaponType type;
    [SerializeField] private int magSize;
    [SerializeField] private int totalSize;
    [SerializeField] private int reloadTime;
    private Text ammoText;
    public static int currentMagAmmo = 14;
    public static int currentTotalAmmo = 28;
    [SerializeField] private float offset;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    public static bool isReloading = false;
    public static RangedWeapons rw;

    void Start()
    {
        rw = this;

        GameObject ammoTextObj = GameObject.Find("Ammo_text").gameObject;
        ammoText = ammoTextObj.GetComponent<Text>();
    }
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(currentTotalAmmo > totalSize) currentTotalAmmo = totalSize;

        ammoText.text = $"Ammo: {currentMagAmmo}/{currentTotalAmmo}";

        if(isReloading) return;

        if(timeBtwShots <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                if(currentMagAmmo >= 1) Shoot();
            }
        }
        else timeBtwShots -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R)) StartCoroutine(Reload());

        if(currentMagAmmo <= 0) StartCoroutine(Reload());
    }
    private void Shoot()
    {
        currentMagAmmo--;
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots;

        if(currentMagAmmo <= 0) StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        if (isReloading || currentTotalAmmo <= 0 || currentMagAmmo == magSize)
            yield break;

        isReloading = true;
        
        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = magSize - currentMagAmmo;
        int ammoToLoad = Mathf.Min(ammoNeeded, currentTotalAmmo);

        currentMagAmmo += ammoToLoad;
        currentTotalAmmo -= ammoToLoad;

        isReloading = false;
    }

    public void AddAmmo(int amount)
    {
        int ammoNeeded = totalSize - currentTotalAmmo;
        if(currentTotalAmmo - ammoNeeded <= amount) currentTotalAmmo += amount;
        else if(currentTotalAmmo - ammoNeeded > amount) currentTotalAmmo += ammoNeeded;
    }
}
