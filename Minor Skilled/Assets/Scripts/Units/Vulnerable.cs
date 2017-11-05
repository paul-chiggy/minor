using UnityEngine;

public abstract class Vulnerable : MonoBehaviour
{
    protected PlayerInfo info;
    public float Health = 100;
    public PlayerInfo Info { get { return info; } set { info = value; } }
    public MeshRenderer[] myColor;
    public Texture2D cursorTexture;

    private void Update()
    {
        if (Health < 0) Die(this.gameObject);
    }

    private void Start()
    {
        info = GetComponent<Player>().Info;
        foreach (MeshRenderer rend in myColor) {
            rend.material.color = info.KingdomColor;
        }
    }

    public abstract void Die(GameObject go);

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    protected void OnMouseEnter()
    {
        info = GetComponent<Player>().Info;
        if (info.isAi)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(2f, 2f), CursorMode.ForceSoftware);
        }
    }

    protected void OnMouseExit()
    {
        if (info.isAi)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
