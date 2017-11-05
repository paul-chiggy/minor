using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public Vector3 Rotation = Vector3.zero;
    public float Speed = 1.0f;
	
	void Update ()
    {
        transform.Rotate(Rotation * Speed * Time.deltaTime);
	}
}
