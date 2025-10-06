using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(PlayerController))]
public class PlayerAttackController : MonoBehaviour
{
    PlayerController m_playerController;

    public GameObject m_bulletPrefab;

    public GameObject hand_object, gun_object, bullet_spawn_object;

    public float m_attackSpeed;

    Vector3 aimPosition;

    public Animator BHAnimator;

    float reloadTimer;
    float reloadTime = 0.5f;

    bool canFire = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reloadTimer = 0f;
        m_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        reloadTimer -= Time.deltaTime;
        if (reloadTimer > 0) canFire = true;
       
    }

    void PlayerInput()
    {
        aimPosition.z = 0;
        Vector3 aimDirection = aimPosition.normalized;

        if (reloadTimer < 0 && aimDirection != Vector3.zero)
        {
            reloadTimer = reloadTime;
            SpawnBullet(aimDirection);
        }



    }

 

    public void OnAttack(InputValue value)
    {
        aimPosition = value.Get<Vector2>();


        if (aimPosition != Vector3.zero)
        {
            Vector3 aimDirection = aimPosition.normalized;

            SetHandsFlip(aimDirection);
            SetGunPose(aimDirection);

            BHAnimator.SetBool("isWalking", true);


            



            BHAnimator.SetFloat("directionX", aimPosition.x);
            BHAnimator.SetFloat("directionY", aimPosition.y);
        }
        else
        {
         
            BHAnimator.SetBool("isWalking", false);
        }

    }



    void SpawnBullet(Vector3 aimDirection)
    {
        if (!m_bulletPrefab || !bullet_spawn_object) return;

        // Spawn at bullet_spawn_object position and rotation
        GameObject bullet = Instantiate(
            m_bulletPrefab,
            bullet_spawn_object.transform.position,
            bullet_spawn_object.transform.rotation
        );

        var bC = bullet.GetComponent<BulletController>();
        bC.m_direction = aimDirection;
        bC.m_damage = m_playerController.m_attackDamage;
    }




    [SerializeField] float gunSpriteForwardDegrees = 0f;
    // If your gun art is drawn pointing UP, set this to +90.
    // If it’s drawn pointing RIGHT, leave as 0.


    const float DEADZONE = 0.0f;

    void SetHandsFlip(Vector3 aimDirection)
    {
        if (!hand_object) return;

        // Keep hands upright, just mirror on X
        hand_object.transform.localRotation = Quaternion.identity;

        Vector3 hs = hand_object.transform.localScale;
        if (aimDirection.x < -DEADZONE)
            hs.x = -Mathf.Abs(hs.x);
        else if (aimDirection.x > DEADZONE)
            hs.x = Mathf.Abs(hs.x);

        hand_object.transform.localScale = hs;
    }

    
    [SerializeField] float leftSnapEpsilon = 0.5f;       // degrees

    void SetGunPose(Vector3 aimDirection)
    {
        if (!gun_object) return;
        if (aimDirection.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + gunSpriteForwardDegrees;

        // Snap near-left to exactly 180 to avoid quaternion ambiguity
        if (Mathf.Abs(Mathf.DeltaAngle(angle, 180f)) <= leftSnapEpsilon)
            angle = 180f;

        // Apply rotation
        gun_object.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        // Flip rule: flip when facing left half-plane
        var s = gun_object.transform.localScale;
        bool facingLeft = Mathf.Abs(Mathf.DeltaAngle(angle - gunSpriteForwardDegrees, 180f)) < 90f; // |angle-180| < 90
        s.y = facingLeft ? -Mathf.Abs(s.y) : Mathf.Abs(s.y);
        gun_object.transform.localScale = s;
    }









}
