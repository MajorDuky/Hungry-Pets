using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] private int _ammoCount;
    [SerializeField] private int _maxAmmoCapacity;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _dmgPerBullet;
    [SerializeField] private MainUIHandler _mainUIHandler;
    [SerializeField] private ParticleSystem _gunshotParticles;
    [SerializeField] private Transform _bulletSpawnerTransform;
    [SerializeField] private int _bulletForce;
    public int AmmoCount
    {
        get { return _ammoCount; }
        set { if (value.GetType() == typeof(int) ) { _ammoCount = value; } }
    }
    public int MaxAmmoCapacity
    {
        get { return _maxAmmoCapacity; }
        set { if (value.GetType() == typeof(int)) { _maxAmmoCapacity = value; } }
    }
    public float FireRate
    {
        get { return _fireRate; }
        set { if (value.GetType() == typeof(float)) { _fireRate = value; } }
    }

    public float FireSpeed
    {
        get { return _fireSpeed; }
        set { if (value.GetType() == typeof(float)) { _fireSpeed = value; } }
    }

    public float ReloadTime
    {
        get { return _reloadTime; }
        set { if (value.GetType() == typeof(float)) { _reloadTime = value; } }
    }

    public float DmgPerBullet
    {
        get { return _dmgPerBullet; }
        set { if (value.GetType() == typeof(float)) { _dmgPerBullet = value; } }
    }

    public bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        _mainUIHandler = FindObjectOfType<MainUIHandler>();
        ReloadGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AmmoCount > 0)
        {
            isShooting = true;
            StartCoroutine(GunshotCoroutine());
        }

        if (Input.GetMouseButtonUp(0) || AmmoCount == 0)
        {
            isShooting = false;
        }

        if (AmmoCount == 0)
        {
            _mainUIHandler.ShowReloadText();
        }

        if (Input.GetKeyDown(KeyCode.R) && AmmoCount != MaxAmmoCapacity)
        {
            ReloadGun();
        }
    }

    private void Gunshot()
    {
        GameObject ammoPooled = ObjectPooler.SharedInstance.GetPooledObject();
        if (ammoPooled != null)
        {
            ammoPooled.transform.position = _bulletSpawnerTransform.position;
            ammoPooled.SetActive(true);
            
            Rigidbody rb = ammoPooled.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(_bulletForce * _bulletSpawnerTransform.forward);
            }
        }
    }

    private void ReloadGun()
    {
        // jouer animation rechargement
        AmmoCount = MaxAmmoCapacity;
        _mainUIHandler.HideReloadText();
        _mainUIHandler.UpdateAmmoAmount(AmmoCount, MaxAmmoCapacity);
    }

    private IEnumerator GunshotCoroutine()
    {
        while(isShooting)
        {
            // degats
            // son
            _gunshotParticles.Play();
            Gunshot();
            AmmoCount--;
            _mainUIHandler.UpdateAmmoAmount(AmmoCount, MaxAmmoCapacity);
            yield return new WaitForSeconds(FireRate);
        }
    }
}
