
using Cinemachine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;
    [SerializeField] Animator animator;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] InputReader input;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] float smoothTime = 0.2f;
    
    private Vector3 moveInput = Vector3.zero;
    private const float ZeroF = 0f;

    // Animator parameters
    static readonly int Speed = Animator.StringToHash("Speed");

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        input.EnablePlayerActions();
    }

    void OnEnable()
    {
        input.Move += Move;
    }

    void OnDisable()
    {
        input.Move -= Move;
    }

    void Move(Vector2 val)
    {
        //moveInput = new Vector3(val.x, 0, val.y);
        //moveInput.Normalize();
    }

    void OnAttack()
    {

    }

    public void Attack()
    {
    }

    void Update()
    {
    }


    void FixedUpdate()
    {
        // characterController.Move(moveInput * moveSpeed * Time.deltaTime);
    }
}
