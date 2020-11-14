using UnityEngine;

public class Camera_Follow_Script : MonoBehaviour
{
    private Transform Player;
    private Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        camPos = transform.position;
        Player = FindObjectOfType<Player_Controller_Script>().gameObject.transform;
    }

    private void LateUpdate()
    {
        camPos.y = Player.position.y;
        transform.position = camPos;
    }
}
