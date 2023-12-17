using UnityEngine;

/*
 * Allows me to access the DontDestroyOnLoadObjects
 */

public class DontDestroyOnLoadSpy : MonoBehaviour
{
    private static DontDestroyOnLoadSpy _instance;
    public static DontDestroyOnLoadSpy Instance {
        get {
            return _instance;
        }
    }
 
    void Awake()
    {
        if (_instance != null) Destroy(this);
        this.gameObject.name = this.GetType().ToString();
        _instance = this;
        DontDestroyOnLoad(this);
    }
 
    public GameObject[] GetAllRootsOfDontDestroyOnLoad() {
        return this.gameObject.scene.GetRootGameObjects();
    }
}