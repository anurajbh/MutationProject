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
    private Transform _startSpawnPoint;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpTime;
    [SerializeField] private Transform _playerFeet;
    [SerializeField] private LayerMask _groundMask;

    public float _maxReturnDistance;

    public float attackCooldown = 2f;
    public float attackTimer = 0f;
    public bool initiateCooldown = false;

    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask layers;
    private float _FurthestPosX;
    private float _previousPosX;
    [SerializeField] public float _distanceTraveledX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _startSpawnPoint = GameObject.Find("StartSpawn_Point").transform;

        anim = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.Log("The Player Rigidbody component is NULL");
        }

        _FurthestPosX = 0f;
        _distanceTraveledX = 0f;

        _previousPosX = 0f;
    }

    private void Update()
    {
        MovementInput();
        CheckForCooldown();
        if (Input.GetAxisRaw("Attack") == 0)
        {
            anim.SetBool("IsAttacking", false);
        }
        if (_hInput != 0)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsAttacking", false);
        }
        else if (_hInput == 0)
        {
            anim.SetBool("IsRunning", false);
        }

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
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsAttacking", true);
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

        if (transform.position.x > _startSpawnPoint.position.x)
        {
            SpawnManager.Instance._spawnStarted = true;
        }

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

        if (transform.position.x < _FurthestPosX - _maxReturnDistance)
        {
            Debug.Log("Will return");
            transform.position = new Vector2(_previousPosX, transform.position.y);
        }

        if ((transform.position.x - _FurthestPosX) > 0)
        {
            _distanceTraveledX += (transform.position.x - _FurthestPosX);
            _FurthestPosX = transform.position.x;
        }

        _previousPosX = transform.position.x;

    }

    private void FixedUpdate()
    {

        
        _hInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(_hInput * _speed, rb.velocity.y);  
    }
}
