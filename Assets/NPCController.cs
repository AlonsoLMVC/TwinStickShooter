using Unity.VisualScripting;
using UnityEngine;

public class NPCController : Entity
{

    public Vector2 m_targetMoveLocation;

    public float m_damageCooldown = 0.25f;
    float m_originalCooldown = 0.25f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_originalCooldown = m_damageCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetMovementDirection = m_targetMoveLocation - new Vector2(transform.position.x, transform.position.y);

        MoveEntity(Vector2.ClampMagnitude(targetMovementDirection, 1));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.rigidbody.name.Contains("Bullet"))
        {
            ChangeHealth(-collision.rigidbody.GetComponent<BulletController>().m_damage);

            Destroy(collision.rigidbody.gameObject);
        }

        if (collision.rigidbody.name.Contains("Player"))
        {
            m_damageCooldown -= Time.deltaTime;

            if (m_damageCooldown <= 0)
            {
                collision.rigidbody.GetComponent<PlayerController>().ChangeHealth(-m_attackDamage);

                m_damageCooldown = m_originalCooldown;
            }
        }
    }


    public override void OnDeath()
    {
        Destroy(gameObject);

        NPCManager.instance.m_NPCList.Remove(this);

        NPCManager.instance.SpawnNPC();
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody.name.Contains("Player"))
        {
            m_damageCooldown = m_originalCooldown;
        }
    }
}
