using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public float Speed = 20;
    public int ScrollWidth = 100;
    public float ScrollSpeed = 25;
    public float RotateAmount = 10;
    public float RotateSpeed = 100;
    public float MinCameraHeight = 10;
    public float MaxCameraHeight = 40;
    public enum CameraMovement { MOUSE, KEYBOARD }
    public CameraMovement Movement = CameraMovement.MOUSE;

    private void Start()
    {
        foreach(var p in GameManager.Instance.Players)
        {
            if (p.isAi) continue;
            var pos = p.Location.position;
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }
    }

    void Update () 
    {
        if (Movement == CameraMovement.MOUSE) { _moveCameraByMouse(); }
        else { _moveCameraByKeyboard(); }
        _rotateCamera();
	}

    private void _moveCameraByKeyboard()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * Speed * Time.deltaTime, //X-Axis
            Input.GetAxis("Vertical") * Speed * Time.deltaTime, //Z-Axis
            (Input.GetKey(KeyCode.Y)) ? (1.0f * Speed * Time.deltaTime) : //Y-Axis
            ((Input.GetKey(KeyCode.U)) ? (-1.0f * Speed * Time.deltaTime) : 0)
        );
    }

    private void _moveCameraByMouse()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (xpos >= 0 && xpos < ScrollWidth) { movement.x -= ScrollSpeed; }
        else if (xpos <= Screen.width && xpos > Screen.width - ScrollWidth) { movement.x += ScrollSpeed; }

        //vertical camera movement
        if (ypos >= 0 && ypos < ScrollWidth) { movement.z -= ScrollSpeed; }
        else if (ypos <= Screen.height && ypos > Screen.height - ScrollWidth) { movement.z += ScrollSpeed; }

        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement.y -= ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > MaxCameraHeight) { destination.y = MaxCameraHeight; }
        else if (destination.y < MinCameraHeight) { destination.y = MinCameraHeight; }

        //if a change in position is detected perform the necessary update
        if (destination != origin) { 
            transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ScrollSpeed);
        }
    }

    private void _rotateCamera()
    {
        Vector3 origin = transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * RotateAmount;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * RotateSpeed);
        }
    }
}
