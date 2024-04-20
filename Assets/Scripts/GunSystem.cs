using UnityEngine;

public class GunSystem : MonoBehaviour
{
    
    public int damage;
    public float fireRate;
    public float spread;
    public float range;
    public float timeBetweenShots;
    public int bulletsPerShot;
    public bool fullyAutomatic;

    private bool shooting;
    private bool readyToShoot;

    public new Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask enemy;

    public GameObject muzzleFlash;

    private void Start(){
        readyToShoot = true;
    }

    private void Update(){
        ShootingInput();
    }

    private void ShootingInput(){

        if(fullyAutomatic){
            shooting = Input.GetKey(GameManager.shootKey);
        }else{
            shooting = Input.GetKeyDown(GameManager.shootKey);
        }

        if(readyToShoot && shooting){
            Shoot();
        }

    }

    private void Shoot(){

        readyToShoot = false;

        float xBulletSpread = Random.Range(-spread, spread);
        float yBulletSpread = Random.Range(-spread, spread);

        Vector3 direction = camera.transform.forward + new Vector3(xBulletSpread, yBulletSpread, 0);

        if(Physics.Raycast(camera.transform.position, direction, out rayHit, range, enemy)){

            Debug.Log(rayHit.collider.name);

            if(rayHit.collider.CompareTag("Enemy")){
                Debug.Log("Hit enemy");
                //rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
            }

        }

        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity, attackPoint.transform);

        Invoke(nameof(ResetShot), fireRate);

        for(int i = 1; i < bulletsPerShot; i++){
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    private void ResetShot(){
        readyToShoot = true;
    }

}
