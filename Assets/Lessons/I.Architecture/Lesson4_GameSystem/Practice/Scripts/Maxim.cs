// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// public interface IAppListener
// {
// }
//
// public interface ILoadingListener : IAppListener
// {
//     void OnAppLoading();
// }
//
// public interface IInitializeListener : IAppListener
// {
//     void OnAppInitialize();
// }
//
// public interface IQuitListener : IAppListener
// {
//     void OnAppQuit();
// }
//
//
//
//
// public class AppObserver : MonoBehaviour
// {
//     private readonly List<IAppListener> _appListeners = new();
//
//     [SerializeField]
//     private bool autoRun;
//     
//     private void Awake()
//     {
//         _appListeners.AddRange(GetComponentsInChildren<IAppListener>());
//
//         if (this.autoRun)
//         {
//             this.Load();
//             this.Initialize();
//         }
//     }
//     
//     
//
//     public void Load() //
//     {
//         foreach (var appListener in _appListeners)
//         {
//             if (appListener is ILoadingListener listener)
//                 listener.OnAppLoading();
//         }
//     }
//
//     public void Initialize() //
//     {
//         foreach (var appListener in _appListeners)
//         {
//             if (appListener is IInitializeListener listener)
//                 listener.OnAppInitialize();
//         }
//     }
//
//     private void OnApplicationQuit()
//     {
//         foreach (var appListener in _appListeners)
//         {
//             if (appListener is IQuitListener listener)
//                 listener.OnAppQuit();
//         }
//     }
// }
//
//
//
//
// public interface IGameListener
// {
// }
//
// public interface IGameStartListener : IGameListener
// {
//     void OnGameStart();
// }
//
// public interface IGamePauseListener : IGameListener
// {
//     void OnGamePause();
// }
//
// public interface IGameResumeListener : IGameListener
// {
//     void OnGameResume();
// }
//
// public interface IGameRestartListener : IGameListener
// {
//     void OnGameRestart();
// }
//
// public interface IGameFinishListener : IGameListener
// {
//     void OnGameFinish();
// }
//
//
//
//
//
//
//
// public class GameObserver : MonoBehaviour
// {
//     private List<IGameListener> _gameListeners = new();
//
//     public void SetListeners(List<IGameListener> listeners)
//     {
//         _gameListeners = listeners;
//     }
//
//     public void StartGame()
//     {
//         foreach (var gameListener in _gameListeners)
//         {
//             if (gameListener is IGameStartListener listener)
//                 listener.OnGameStart();
//         }
//     }
//
//     public void PauseGame()
//     {
//         foreach (var gameListener in _gameListeners)
//         {
//             if (gameListener is IGamePauseListener listener)
//                 listener.OnGamePause();
//         }
//     }
//
//     public void ResumeGame()
//     {
//         foreach (var gameListener in _gameListeners)
//         {
//             if (gameListener is IGameResumeListener listener)
//                 listener.OnGameResume();
//         }
//     }
//
//     public void RestartGame()
//     {
//         foreach (var gameListener in _gameListeners)
//         {
//             if (gameListener is IGameRestartListener listener)
//                 listener.OnGameRestart();
//         }
//     }
//
//     public void FinishGame()
//     {
//         foreach (var gameListener in _gameListeners)
//         {
//             if (gameListener is IGameFinishListener listener)
//                 listener.OnGameFinish();
//         }
//     }
// }
//
// [RequireComponent(typeof(GameObserver))]
// public class GameListenersInstaller : MonoBehaviour, IInitializeListener
// {
//     void IInitializeListener.OnAppInitialize()
//     {
//         var gameListener = GetComponentsInChildren<IGameListener>().ToList();
//         var gameObserver = GetComponent<GameObserver>();
//
//         gameObserver.SetListeners(gameListener);
//     }
// }