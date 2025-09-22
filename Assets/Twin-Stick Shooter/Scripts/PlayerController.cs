using UnityEngine;

public class PlayerController : Entity
{
    Vector2 m_directionInput;

    private void Update()
    {
        PlayerInput();
       
        MoveEntity(Vector2.ClampMagnitude(m_directionInput, 1));
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_directionInput = new Vector2(m_directionInput.x, 1);
        }
        else if (Input.GetKey (KeyCode.S)) 
        {
            m_directionInput = new Vector2(m_directionInput.x, -1);
        }
        else
        {
            m_directionInput = new Vector2(m_directionInput.x, 0);

        }

        if (Input.GetKey(KeyCode.A))
        {
            m_directionInput = new Vector2(-1, m_directionInput.y);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_directionInput = new Vector2(1, m_directionInput.y);
        }
        else
        {
            m_directionInput = new Vector2(0, m_directionInput.y);
        }


    }

    public override void OnDeath()
    {
        
    }
}
