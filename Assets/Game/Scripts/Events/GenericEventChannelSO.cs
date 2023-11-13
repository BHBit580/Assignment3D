using UnityEngine;

    [CreateAssetMenu(menuName = "ScriptableObject/GenericEventChannel")]
    public class GenericEventChannelSO : ScriptableObject
    {
        public delegate void GenericEventDelegate(object data);

        private GenericEventDelegate loadingRequestEventHandler;

        public void RaiseEvent(object data)
        {
            loadingRequestEventHandler?.Invoke(data);
        }

        public void RegisterListener(GenericEventDelegate listener)
        {
            loadingRequestEventHandler += listener;
        }
        public void UnregisterListener(GenericEventDelegate listener)
        {
            loadingRequestEventHandler -= listener;
        }
    }