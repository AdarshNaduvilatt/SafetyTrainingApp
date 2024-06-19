using UnityEngine;

public class ProximityCheck : MonoBehaviour
{
    public GameObject menuButtons;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        menuButtons.SetActive(distance < 3.0f); // Show buttons when within 3 units
    }
}