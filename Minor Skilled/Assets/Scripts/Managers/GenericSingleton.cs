using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>(); //should be already in scene on GO
                //this will work good only with scripts that 
                //don't require smth to be dragged in, otherwise
                //we get a nullReferenceException at some point
                if (instance == null) 
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

    /**
     * Gives persistence across all scenes
     */ 
	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
            //for this to work properly GO must be a root game object
            //or a component on a root game object, otherwise we'll get warnings
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
