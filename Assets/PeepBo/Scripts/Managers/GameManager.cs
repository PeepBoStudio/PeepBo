using PeepBo.UI.Popup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeepBo.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }


        ResourceManager resourceManager = new ResourceManager();
        UIManager uiManager = new UIManager();
        RoomManager roomManager = new RoomManager();
        CommandManager commandManager = new CommandManager();
        AudioManager audioManager = new AudioManager();
        HogamManager hogamManager = new HogamManager();


        public static ResourceManager Resource { get => Instance.resourceManager; }
        public static UIManager UI { get => Instance.uiManager; }
        public static RoomManager Room { get => Instance.roomManager; }
        public static CommandManager Command { get => Instance.commandManager; }
        public static AudioManager Audio { get => Instance.audioManager; }
        public static HogamManager Hogam { get => Instance.hogamManager; }



        public static string DummyEpisode;

        static void Init()
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<GameManager>();
                }
                instance = go.GetComponent<GameManager>();

                InitInputManager();
                Instance.audioManager.InitAudioManager();

                DontDestroyOnLoad(instance.gameObject);
            }
        }

        public static void LoadMainSceneAndEpisodePopup()
        {
            var scene = SceneManager.LoadSceneAsync("MainScene");
			scene.completed += ShowEpisodePopup;
        }

		private static void ShowEpisodePopup(AsyncOperation obj)
		{
            GameManager.UI.ShowPopupUI<UI_EpisodePopup>();
		}

		private static void InitInputManager()
        {
            instance.gameObject.AddComponent<InputManager>();
        }
    }
}
