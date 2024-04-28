using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float health;

	private IEnumerator Start(){
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage){

        health -= damage;

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
