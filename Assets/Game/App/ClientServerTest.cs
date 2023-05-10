using System;
using System.Collections.Generic;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.App
{
    public sealed class ClientServerTest : MonoBehaviour
    {
        [Button]
        public void GetPlayerState()
        {
            var playerClient = ServiceLocator.GetService<PlayerClient>();
            var money = playerClient.GetData<Int64>("money");
            Debug.Log($"SUCCESS Money: {money}");
        }

        [Button]
        public void SavePlayerState()
        {
            // var playerSaver = ServiceLocator.GetService<Player___Saver>();
            // playerSaver.SavePlayerState(
            //     new Dictionary<string, object>
            //     {
            //         {"money", 500},
            //         {"experience", 777}
            //     },
            //     onSuccess: () => Debug.Log("SAVE SUCCESS "),
            //     onError: () => Debug.Log("SAVE FAIL")
            // );


            // const string data = "{\n" +
            //                     "\"money\" : 500,\n" +
            //                     "\"experience\" : 1000\n" +
            //                     "}";
            //

            // playerClient.SavePlayerState(
            //     new Dictionary<string, object>
            //     {
            //         {"money", 500},
            //         {"experience", 777}
            //     },
            //     onSuccess: () => Debug.Log("SAVE SUCCESS "),
            //     onError: () => Debug.Log("SAVE FAIL")
            // );
        }
    }
}