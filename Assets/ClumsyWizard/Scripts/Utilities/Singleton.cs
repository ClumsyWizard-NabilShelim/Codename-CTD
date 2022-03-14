using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T instance;

		protected virtual void Awake()
		{
			instance = this as T;
		}
	}

	public class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			if(instance != null)
			{
				Destroy(gameObject);
				return;
			}

			base.Awake();
		}
	}

	public class PersistantSingleton<T> : Singleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}
	}
}