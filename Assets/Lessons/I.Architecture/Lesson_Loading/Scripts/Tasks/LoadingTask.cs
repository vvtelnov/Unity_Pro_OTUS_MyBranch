using System.Threading.Tasks;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    public abstract class LoadingTask : ScriptableObject
    {
        public abstract Task<Result> Do();
        
        public struct Result
        {
            public bool success;
            public string error;
        }
    }
}