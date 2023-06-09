using System;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    //Facebook Plugin
    public class FB
    {
        public static void Init(Action success = null)
        {
            Debug.Log("<color=blue>Init Facebook</color>");
            success?.Invoke();
        }
    }
}