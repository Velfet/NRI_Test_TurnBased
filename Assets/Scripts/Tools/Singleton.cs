using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                //This assumes that there is no singleton object yet
                GameObject gameObject = new GameObject();
                gameObject.name = typeof(T).Name;
                _instance = gameObject.AddComponent<T>();
            }
            return _instance;
        }
        private set
        {

        }
    }

    public virtual void Awake()
    {
        if(_instance == null)
        {
            //This assumes that there is already a singleton object
            _instance = this.GetComponent<T>();
        }
        else if(_instance != null && _instance != this.GetComponent<T>())
        {
            Debug.LogWarning("Destroying instance from " + gameObject.name);
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                //This assumes that there is no singleton object yet
                GameObject gameObject = new GameObject();
                gameObject.name = typeof(T).Name;
                _instance = gameObject.AddComponent<T>();
            }
            return _instance;
        }
        private set
        {

        }
    }

    public virtual void Awake()
    {
        if(_instance == null)
        {
            //This assumes that there is already a singleton object
            _instance = this.GetComponent<T>();
            DontDestroyOnLoad(this);
        }
        else if(_instance != null && _instance != this.GetComponent<T>())
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}
