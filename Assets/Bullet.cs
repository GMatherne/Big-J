using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime;

	private IEnumerator Start(){
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

}
