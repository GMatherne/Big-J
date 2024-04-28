using System.Collections;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    
    [Header("Gun Stats")]
    public int damage;
    public float fireRate;
    public float spread;
    public float range;
    //public float timeBetweenShots;
    //public int bulletsPerShot;
    public bool fullyAutomatic;

    private bool shooting;

    [Header("Attack Point")]
    public new Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;

    [Header("Enemy")]
    public LayerMask enemy;

    [Header("Effects")]
    public GameObject muzzleFlash;

    public AudioSource audioSource;

    public AudioClip shootSoundEffect;

    private void Start(){
        Invoke(nameof(ResetShot), GameManager.Instance.weaponSwapShootingCooldown);
    }

    private void Update(){
        ShootingInput();
    }

    private void ShootingInput(){

        if(fullyAutomatic){
            shooting = Input.GetKey(GameManager.Instance.shootKey);
        }else{
            shooting = Input.GetKeyDown(GameManager.Instance.shootKey);
        }

        if(GameManager.Instance.ableToShoot && shooting && !GameManager.Instance.paused){
            Shoot();
        }

    }

    private void Shoot(){

        GameManager.Instance.ableToSwapWeapon = false;
        GameManager.Instance.ableToShoot = false;

        float xBulletSpread = Random.Range(-spread, spread);
        float yBulletSpread = Random.Range(-spread, spread);

        Vector3 direction = camera.transform.forward + new Vector3(xBulletSpread, yBulletSpread, 0);

        if(Physics.Raycast(camera.transform.position, direction, out rayHit, range, enemy)){

            if(rayHit.collider.CompareTag("Enemy")){
                rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            }

        }

        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity, attackPoint.transform);

        audioSource.PlayOneShot(shootSoundEffect);

        Invoke(nameof(ResetShot), fireRate);

        /*
        for(int i = 1; i < bulletsPerShot; i++){
            Invoke(nameof(Shoot), timeBetweenShots);
        }
        */
    }

    private void ResetShot(){
        GameManager.Instance.ableToShoot = true;
        GameManager.Instance.ableToSwapWeapon = true;
    }

}