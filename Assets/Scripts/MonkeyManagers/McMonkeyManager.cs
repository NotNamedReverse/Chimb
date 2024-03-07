using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using PlayFab.AdminModels;
using UnityEngine;


namespace MonkeyManagers.McMonkeyManager
{

    public class McMonkeyManager : MonoBehaviour
    {
        public enum GameMode
        {
            Casual,
            Tag,
            Infection,
            Freeze,
            Battle,
            Zombies,

        }
        private static McMonkeyManager _instance;

        public static McMonkeyManager Instance { get { return _instance; } }

        // ---------------------- //

        [Header("Playfab")]
        public Playfablogin Playfablogin;

        [Header("Floats")]
        public float hapticWait = 0.05f;

        [Header("Game Mode Stuff")]

        public GameMode CurrentGameMode = GameMode.Casual;

        [Header("Hitsounds")]

        public AudioClip[] water, stone, tree, grass, metal, glass, snow, dirt, carpet, wood, lava;
        
        [SerializeField] 
        public Dictionary<string, AudioClip[]> audio;
        

        private void AddBan(string playerId, uint hours)
        {
            PlayFabAdminAPI.BanUsers(new BanUsersRequest()
            {
                Bans = new List<BanRequest>() {
                new BanRequest() {
                DurationInHours = hours,
                PlayFabId = playerId,
                Reason = "Automatic ban for WH",
            }
        }
            }, result =>
            {
                Application.Quit();
            }, error =>
            {
                Application.Quit();
            });
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }

            // -- hitsounds

            audio = new Dictionary<string, AudioClip[]> {
            { "Water", water },
            { "Stone", stone },
            { "Tree", tree },
            { "Grass", grass },
            { "Metal", metal },
            { "Glass", glass },
            { "Snow", snow },
            { "Dirt", dirt },
            { "Carpet", carpet },
            { "Wood", wood }
            };
        }
        void Start()
        {
            Debug.Log("McMonkeyManager Successfully started.");
        }

        private void Update()
        {
            if (CurrentGameMode != GameMode.Casual)
            {
                AddBan(Playfablogin.MyPlayFabID, 100);
            }
        }
    }

}