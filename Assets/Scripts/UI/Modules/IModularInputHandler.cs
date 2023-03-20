using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TestTask.UI.Modules
{
    /// <summary>
    /// This interface can help us greatly if we'll decide to expand InputManager.
    /// For now it simply helps us guarantee that all our InputManager modules will implement an initialization.
    /// </summary>
    public interface IModularInputHandler
    {
        public void Initialize();
    }
}