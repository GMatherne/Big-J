using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    public new Transform camera;

    private void Update(){
        transform.position = camera.position;
    }
}