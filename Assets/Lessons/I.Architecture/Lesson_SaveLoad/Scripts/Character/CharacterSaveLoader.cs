// using UnityEngine;
//
// namespace Lessons.Architecture.SaveLoad
// {
//     public sealed class CharacterSaveLoader : MonoBehaviour
//     {
//         private const string PLAYER_PREFS_KEY = "Lesson/CharacterData";
//
//         [SerializeField]
//         private CharacterService characterService;
//
//         private void Awake()
//         {
//             this.LoadData();
//         }
//
//         private void OnApplicationQuit()
//         {
//             this.SaveData();
//         }
//
//         private void LoadData()
//         {
//             if (PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
//             {
//                 var json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
//                 var characterData = JsonUtility.FromJson<CharacterData>(json);
//                 this.characterService.SetCharacterData(characterData);
//
//                 Debug.Log($"<color=orange>LOAD CHARACTER DATA {json}</color>");
//             }
//         }
//
//         private void SaveData()
//         {
//             CharacterData characterData = this.characterService.GetCharacterData();
//             var json = JsonUtility.ToJson(characterData);
//             PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
//
//             Debug.Log($"<color=yellow>SAVE CHARACTER DATA {json}</color>");
//         }
//     }
// }