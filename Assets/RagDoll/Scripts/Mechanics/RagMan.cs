using UnityEngine;

public class RagMan : MonoBehaviour
{
    public bool ragdoll;
    private GameManager gameManager;
    private Rigidbody rb;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        
        HandleRagdoll(false);
        ragdoll = false;
    }
    private void LateUpdate() => HandleRagdoll(gameManager.isPlaying);
    private void Update()
    {
        float velocity = rb.velocity.magnitude;
        gameManager.characterVelocity = velocity;
    }

    /// <summary> sets whether the playermodel is in ragdoll mode or not </summary>
    /// <param name="_normal">triggers ragdoll mode when enabled</param>
    public void HandleRagdoll(bool _normal)
    {
        var bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies)
            body.isKinematic = !_normal;
    }
}
