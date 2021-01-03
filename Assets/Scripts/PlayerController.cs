using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _hInput;
    private float _groundedRadius = 0.3f;
    private float _jumpCounter;
    private bool _isGrounded;
    private bool _isJumping;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpTime;
    [SerializeField] private Transform _playerFeet;
    [SerializeField] private LayerMask _groundMask;

    public float attackCooldown = 2f;
    public float attackTimer = 0f;
    public bool initiateCooldown = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask layers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.Log("The Player Rigidbody component is NULL");
        }
    }

    private void Update()
    {
        MovementInput();
        CheckForCooldown();
    }

    private void CheckForCooldown()
    {
        if(initiateCooldown)
        {
            attackTimer += Time.deltaTime;
        }   
        if(attackTimer>=attackCooldown)
        {
            initiateCooldown = false;
            attackTimer = 0f;
        }
        if (Input.GetAxisRaw("Attack") != 0 && !initiateCooldown)
        {
            AttackEnemy();
            initiateCooldown = true;
        }
    }

    private void AttackEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layers);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag=="Enemy")
            {
                collider.GetComponent<EnemyHealth>().HitEnemy();
            }
        }
    }

    private void MovementInput()
    {
        //Casts a circle around the player's feet, checking for ground
        _isGrounded = Physics2D.OverlapCircle(_playerFeet.position, _groundedRadius, _groundMask);

        if (_isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            _isJumping = true;
            _jumpCounter = _jumpTime;
            rb.velocity = Vector2.up * _jumpForce;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (_jumpCounter > 0 && _isJumping == true)
            {
                rb.velocity = Vector2.up * _jumpForce;
                _jumpCounter -= Time.deltaTime;
            }

            else
            {
                _isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _isJumping = false;
        }


        //Rotates the player sprite when the player is walking left or right
        if (_hInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (_hInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        _hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(_hInput * _speed, rb.velocity.y);  
    }
}
