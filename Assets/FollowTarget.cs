using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target; // The object to follow
    [SerializeField] private bool matchRotation = false;
    [SerializeField] private bool matchScale = false;

    void LateUpdate()
    {
        if (target == null) return;

        // Copy position
        transform.position = target.position;

        // Optional: copy rotation
        if (matchRotation)
            transform.rotation = target.rotation;

        // Optional: copy scale
        if (matchScale)
            transform.localScale = target.localScale;
    }
}
