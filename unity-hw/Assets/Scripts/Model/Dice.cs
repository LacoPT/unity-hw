using System.Linq;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] [Range(0.6f, 5f)] private float rayCastDistance = 1.5f;
    [SerializeField] [Range(0f, 60f)] private float angleRange = 35f;
    [SerializeField] [Range(2.5f, 10f)] private float forceScale = 5f;
    [SerializeField] [Range(0.05f, 0.1f)] private float torqueScale = 0.1f;
    
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Roll()
    {

        var xrot = Random.Range(-angleRange, angleRange);
        var zrot = Random.Range(-angleRange, angleRange);

        Vector3 direction = Quaternion.Euler(xrot, 0, zrot) * Vector3.up;
        rb.AddForce(direction * forceScale, ForceMode.Impulse);
        rb.AddTorque(new Vector3(xrot * torqueScale, 0, zrot * torqueScale), ForceMode.Impulse);
    }

    public int GetScore()
    {
        //This is made so directions actually updated when the score is requested
        Vector3[] directions =
        {
            transform.right,
            -transform.up,
            -transform.forward,
            transform.forward,
            transform.up,
            -transform.right
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(transform.position, directions[i], rayCastDistance, floorLayer))
                return i + 1;
        }

        return 0;
    }

    //This actually works well, trust me i tested it
    public bool IsStill()
    {
        var still = rb.linearVelocity == Vector3.zero && rb.angularVelocity == Vector3.zero;
        return still;
    }
}
