using System.Collections.Generic;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.App
{
    public sealed class ClientServerTest : MonoBehaviour
    {
        [Button]
        public void Auth()
        {
            var authenticator = ServiceLocator.GetService<UserAuthenticator>();
            authenticator.Authenticate(
                onSuccess: () => { Debug.Log("AUTH SUCCEED!"); },
                onError: () => { Debug.Log("AUTH FAILED!"); }
            );
        }

        [Button]
        public void LoadPlayerState()
        {
            var playerClient = ServiceLocator.GetService<PlayerClient>();
            playerClient.LoadPlayerState(
                onSuccess: objects => Debug.Log($"LOAD SUCCESS {objects}"),
                onError: () => Debug.Log("LOAD FAIL")
            );
        }

        [Button]
        public void SavePlayerState()
        {
            var playerClient = ServiceLocator.GetService<PlayerClient>();


            const string data = "{\n" +
                                "\"money\" : 500,\n" +
                                "\"experience\" : 1000\n" +
                                "}";

            playerClient.SavePlayerState(
                data,
                onSuccess: () => Debug.Log("SAVE SUCCESS "),
                onError: () => Debug.Log("SAVE FAIL")
            );
        }
    }
}