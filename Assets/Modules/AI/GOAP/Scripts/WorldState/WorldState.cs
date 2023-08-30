using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.GOAP
{
    [AddComponentMenu("AI/GOAP/World State")]
    [DisallowMultipleComponent]
    public class WorldState : MonoBehaviour, IFactState
    {
        private readonly Dictionary<string, bool> facts = new();
        
        [Space]
        [SerializeField]
        private FactInspector[] inspectors;

        public bool GetFact(string id)
        {
            return this.facts[id];
        }

        public bool TryGetFact(string id, out bool value)
        {
            return this.facts.TryGetValue(id, out value);
        }

        public bool ContainsFact(string id)
        {
            return this.facts.ContainsKey(id);
        }

        [Button]
        public void SetFact(string key, bool value)
        {
            this.facts[key] = value;
        }

        [Button]
        public void RemoveFact(string key)
        {
            this.facts.Remove(key);
        }

        [Button]
        public void UpdateFacts()
        {
            for (int i = 0, count = this.inspectors.Length; i < count; i++)
            {
                var inspector = this.inspectors[i];
                inspector.OnUpdate(this);
            }
        }

        public IEnumerator<KeyValuePair<string, bool>> GetEnumerator()
        {
            return this.facts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}