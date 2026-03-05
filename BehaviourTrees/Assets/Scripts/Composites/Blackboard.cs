using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Blackboard 
    {
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public T GetValue<T>(string key) => dictionary.ContainsKey(key) ? (T)dictionary[key] : default;
        public void SetValue<T>(string key, T value) => dictionary[key] = value;
        public void Clear() => dictionary.Clear();
    }
}
