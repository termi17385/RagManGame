using UnityEngine;
using TMPro;

public class RagScore : MonoBehaviour
{
    [SerializeField] private float minForceToAddScore = 1f;
    [SerializeField] private TextMeshProUGUI scoreBoardText;
    [SerializeField] private Transform content;

    public float currentScore = 0;
    private Joint[] joints;
    private GameManager gameManager;
    
    private void OnEnable() => joints = GetComponentsInChildren<Joint>();

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        LimbManager.instance.DamageEvent += HandleLimbDamage;
    }

    private void LateUpdate() => scoreBoardText.text = $"{currentScore:000000000}";
    private void FixedUpdate() { if(gameManager.isPlaying && gameManager.dropped) currentScore += ScoreRagdoll(); }

    /// <summary> Is called by event to handle spawning of damage markers
    /// when the ragdoll takes said amount of damage </summary>
    /// <param name="_damage">damage to limb</param>
    /// <param name="_limbName">which limb is damaged</param>
    private void HandleLimbDamage(float _damage, string _limbName)
    {
        if(_damage > minForceToAddScore && gameManager.isPlaying && gameManager.dropped)
        {
            GameObject obj = Resources.Load<GameObject>(_limbName);
            if(obj == null) Debug.LogError(Resources.Load<GameObject>(_limbName) + _limbName);
            Instantiate(obj, content);
        }
        Debug.Log($"Name: {_limbName} \n damage: {_damage}");
    }
    
    /// <summary>
    /// Handles calculating a score from the join force </summary>
    float ScoreRagdoll()
    {
        float totalForce = 0;
        foreach (var joint in joints)
            if (joint.currentForce.magnitude > minForceToAddScore)
                totalForce += joint.currentForce.magnitude * 0.1f;

        return totalForce;
    }
}
