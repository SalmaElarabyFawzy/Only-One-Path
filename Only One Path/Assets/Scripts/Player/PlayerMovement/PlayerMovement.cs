using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private float speed = 10f;
    private CharacterController _controller;
    private PlayerContrpoller _playerContrpoller;
    private Vector2 Input;
    private AudioSource _sound;


    [Header("Camera")]

    private Transform camera;

   

    private void Awake()
    {
        _playerContrpoller = new PlayerContrpoller();
        _controller = GetComponent<CharacterController>();
         camera = transform.GetChild(0).transform;
        _sound = GetComponent<AudioSource>();
       
    }



    private void OnEnable()
    {
        _playerContrpoller.Enable();
        _playerContrpoller.Player.Movement.performed += cxt => Input = cxt.ReadValue<Vector2>();
        _playerContrpoller.Player.Movement.canceled += cxt => Input = Vector2.zero;

    }



    private void OnDisable()
    {
        _playerContrpoller.Disable();
    }
 


    private void Update()
    {
        Movement(Input);

    }


    private void Movement(Vector2 cxt)
    {
        float WS = cxt.y;
        float AD = cxt.x;
        Vector3 move = (transform.forward* WS+transform.right*AD).normalized;
        if(move.magnitude > 0.1)
        {
           _sound.enabled = true;
          _controller.Move(move * speed * Time.deltaTime);
       
        }
        else  _sound.enabled = false;
      
    }

}
