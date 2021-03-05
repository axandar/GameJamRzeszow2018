using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
	private static T _instance;
	private static readonly object InstanceLock = new object();
	private static bool Quitting;

	public static T Instance{
		get{
			GameObject go = new GameObject(typeof(T).ToString());
			lock(InstanceLock){
				if(_instance != null || Quitting){
					return _instance;
				}

				_instance = FindObjectOfType<T>();
				if(_instance != null){
					return _instance;
				}

				_instance = go.AddComponent<T>();

				DontDestroyOnLoad(_instance.gameObject);

				return _instance;
			}
		}
	}

	protected virtual void Awake(){
		if(_instance == null){
			_instance = gameObject.GetComponent<T>();
		}else if(_instance.GetInstanceID() != GetInstanceID()){
			Destroy(gameObject);
			throw new System.Exception($"Instance of {GetType().FullName} " + 
				$"already exists, removing {ToString()}");
		}
	}

	protected virtual void OnApplicationQuit(){
		Quitting = true;
	}
}