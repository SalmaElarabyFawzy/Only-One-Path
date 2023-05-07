using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControl : MonoBehaviour
{
    [Header("Player")]
    private Transform player;


    [Header("Camera")]
    [SerializeField] float mouseSenstivity = 3f;
    private float mouseXdir = 0;
    private Vector2 MouseDir;
    private CameraControle _camera;
  

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = new CameraControle();
    }


    private void OnEnable()
    {
        _camera.Enable();
        _camera.FPSCamera.MouseX.performed += cxt => MouseDir.x = cxt.ReadValue<float>();
        _camera.FPSCamera.MouseY.performed += _   => MouseDir.y = _.ReadValue<float>();
        _camera.FPSCamera.MouseX.canceled  += cxt => MouseDir.x = 0f;
        _camera.FPSCamera.MouseY.canceled  += _   => MouseDir.y = 0f;
    }


    private void OnDisable()
    {
        _camera.Disable();
    }
   


    void Update()
    {
       Rotation();
    }



    void Rotation()
    {
        float mouseX = MouseDir.x * mouseSenstivity * Time.deltaTime;
        float mouseY = MouseDir.y * mouseSenstivity * Time.deltaTime;
         mouseXdir -= mouseY;
         mouseXdir = Mathf.Clamp(mouseXdir, -50f, 30f);   
         transform.localRotation = Quaternion.Euler(mouseXdir,0, 0);
         player.Rotate(Vector3.up*mouseX);
    }
}
