using System;
using System.Collections;
using UnityEngine;


[RequireComponent (typeof(PlayerController))]
public class PlayerAttackController : MonoBehaviour
{
    PlayerController m_playerController;

    public GameObject m_bulletPrefab;

    public float m_attackSpeed;

    public enum m_FireMode
    {
        SemiAuto,
        FullAuto
    }

    public m_FireMode m_fireMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        Vector3 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimPosition.z = 0;

        Vector3 aimDirection = aimPosition - transform.position;

        aimDirection = aimDirection.normalized;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (m_fireMode == m_FireMode.SemiAuto)
            {
                SpawnBullet(aimDirection);
            }
            else if (m_fireMode == m_FireMode.FullAuto)
            {
                StartCoroutine(StartFiring());
            }
        }
    }

    IEnumerator StartFiring()
    {
        Debug.Log("startedFiring");
        while (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimPosition.z = 0;

            Vector3 aimDirection = aimPosition - transform.position;

            aimDirection = aimDirection.normalized;

            SpawnBullet(aimDirection);

            yield return new WaitForSecondsRealtime(1 / m_attackSpeed);
        }
        
    }

    void SpawnBullet(Vector3 aimDirection)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
        var bC = bullet.GetComponent<BulletController>();
        bC.m_direction = aimDirection;
        bC.m_damage = m_playerController.m_attackDamage;
    }

    

}
