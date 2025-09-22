using UnityEngine;

public class Entity : MonoBehaviour
{
    public Transform m_transform;

    public float m_health = 100f;

    public float m_movementSpeed = 1f;

    public float m_attackDamage = 5f;

    private void OnEnable()
    {
        m_transform = transform;
    }

    public void MoveEntity(Vector2 movementValue)
    {
        if (transform != null)
            transform.position += new Vector3(movementValue.x * m_movementSpeed, movementValue.y * m_movementSpeed, 0) * Time.deltaTime;
    }

    public void ChangeHealth(float changeAmount)
    {
        m_health += changeAmount;

        if (m_health <= 0)
            OnDeath();
    }

    public virtual void OnDeath()
    {

    }
}
