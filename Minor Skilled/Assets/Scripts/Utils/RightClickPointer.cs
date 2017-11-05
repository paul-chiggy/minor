using UnityEngine;

public class RightClickPointer : MonoBehaviour
{
    public float Speed = 5f;

	private void Update ()
    {
        if (_aboveSurface()) _sinkMyself();
        else _resetMyself();
	}

    private void _sinkMyself()
    {
        transform.Translate(new Vector3((-1 * Speed * Time.deltaTime),0,0));
    }

    private void _resetMyself()
    {
        transform.gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
    }

    private bool _aboveSurface()
    {
        return transform.position.y >= -0.5f;
    }
}
