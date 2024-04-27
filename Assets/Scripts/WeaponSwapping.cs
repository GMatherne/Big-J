using UnityEngine;

public class WeaponSwapping : MonoBehaviour
{

    public int selectedWeapon = 0;
    private int numberOfWeapons;

    private void Start(){
        numberOfWeapons = transform.childCount;
        SelectWeapon();
    }

    private void Update(){
        
        if(!GameManager.Instance.ableToSwapWeapon || GameManager.Instance.paused){
            return;
        }
        
        int previousWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            selectedWeapon = (selectedWeapon + 1) % numberOfWeapons;
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selectedWeapon <= 0){
                selectedWeapon = numberOfWeapons - 1;
            }else{
                selectedWeapon--;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            selectedWeapon = 0;
        }else if(Input.GetKeyDown(KeyCode.Alpha2) && numberOfWeapons >= 2){
            selectedWeapon = 1;
        }else if(Input.GetKeyDown(KeyCode.Alpha3) && numberOfWeapons >= 3){
            selectedWeapon = 2;
        }else if(Input.GetKeyDown(KeyCode.Alpha4) && numberOfWeapons >= 4){
            selectedWeapon = 3;
        }

        if(previousWeapon != selectedWeapon){
            SelectWeapon();

            GameManager.Instance.ableToShoot = false;
            Invoke(nameof(ResetShot), GameManager.Instance.weaponSwapShootingCooldown);

        }

    }

    private void SelectWeapon(){

        int i = 0;

        foreach(Transform weapon in transform){
            if(i == selectedWeapon){
                weapon.gameObject.SetActive(true);
            }else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }

    private void ResetShot(){
        GameManager.Instance.ableToShoot = true;
    }
}