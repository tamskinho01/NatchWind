using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public GameObject Player;
    
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        transform.position = position;
    }
}
