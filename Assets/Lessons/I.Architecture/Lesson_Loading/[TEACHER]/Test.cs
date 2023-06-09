using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Lessons.I.Architecture.Lesson_Loading._TEACHER_
{
    public sealed class Test : MonoBehaviour
    {
        private async void Start()
        {
            var obj = await Task.Factory
                .StartNew(this.GenerateName)
                .ConfigureAwait(continueOnCapturedContext: true);

            Debug.Log($"OBJ {obj == null}");
        }

        private object GenerateName()
        {
            Thread.Sleep(500);
            
            return null;
        }
    }
}