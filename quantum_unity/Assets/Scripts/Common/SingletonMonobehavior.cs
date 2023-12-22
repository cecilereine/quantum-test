using UnityEngine;

namespace QuantumSoccerTest.Common
{
    public class SingletonMonobehavior<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        { 
            get 
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    { 
                        var obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance; 
            }

        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}
