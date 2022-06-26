
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    //kamera do pokoi
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //kamera sledzaca gracza
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //kamera do pokoi
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //kamera sledzaca gracza
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * transform.localScale.x), Time.deltaTime * cameraSpeed);

    }
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
