using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    Vector2 m_directionInput;
    public Animator feetAnimator, BHAnimator;


    private void Update()
    {
       
        MoveEntity(Vector2.ClampMagnitude(m_directionInput, 1));
        
    }

   

    public override void OnDeath()
    {
        
    }

    public void OnMove(InputValue value)
    {
        m_directionInput = value.Get<Vector2>();

        if (m_directionInput != Vector2.zero)
        {
            feetAnimator.SetBool("isWalking", true);

            // Only update facing direction when we actually have input
            feetAnimator.SetFloat("directionX", m_directionInput.x);
            feetAnimator.SetFloat("directionY", m_directionInput.y);

            BHAnimator.SetFloat("directionX", m_directionInput.x);
            BHAnimator.SetFloat("directionY", m_directionInput.y);
        }
        else
        {
            // Release = let Update() handle stopping walk, do not touch direction floats
            Debug.Log("STOPPED WALKING");
            feetAnimator.SetBool("isWalking", false);
            BHAnimator.SetBool("isWalking", false);
        }

        Debug.Log(m_directionInput);
    }


}
