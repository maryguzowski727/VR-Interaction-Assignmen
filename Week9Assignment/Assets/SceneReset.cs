using UnityEngine;
// We removed the error-causing line here

public class SceneReset : MonoBehaviour
{
    [Header("Drag Furniture Here")]
    public Transform[] furniture;

    // Memory storage for where things started
    private Vector3[] startPositions;
    private Quaternion[] startRotations;

    void Start()
    {
        // 1. Memorize positions when the game starts
        startPositions = new Vector3[furniture.Length];
        startRotations = new Quaternion[furniture.Length];

        for (int i = 0; i < furniture.Length; i++)
        {
            if (furniture[i] != null)
            {
                startPositions[i] = furniture[i].position;
                startRotations[i] = furniture[i].rotation;
            }
        }
    }

    public void ResetScene()
    {
        for (int i = 0; i < furniture.Length; i++)
        {
            if (furniture[i] != null)
            {
                // Reset Position and Rotation
                furniture[i].position = startPositions[i];
                furniture[i].rotation = startRotations[i];

                // Stop the physics (so it doesn't keep flying after reset)
                if (furniture[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    // 2. This function detects when your hand touches the button
    private void OnTriggerEnter(Collider other)
    {
        // If anything touches the button, trigger the reset!
        // (You can add specific checks for "Hand" here if needed, but this is easiest for now)
        ResetScene();
        Debug.Log("Button Pressed by: " + other.name);
    }
}