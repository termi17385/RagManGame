using UnityEngine;
public class LimbCollision : MonoBehaviour
{
    [SerializeField] private new string name;
    private Joint joint;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        joint = GetComponent<Joint>();
    }
    private void OnCollisionEnter(Collision _other)
    {
        if(!_other.collider.CompareTag("Player") && !_other.collider.CompareTag("InvisWall"))
        {
            float x = 1;
            if(name == "Ribs") x = 4;
            LimbManager.instance.LimbDamage(joint.currentForce.magnitude * x,name);
        }
    }

    private void OnMouseDown() => gameManager.isPlaying = true;
}
