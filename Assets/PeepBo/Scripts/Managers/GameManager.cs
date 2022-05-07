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
        SwitchManager switchManager = new SwitchManager();

        public static ResourceManager Resource { get => Instance.resourceManager; }
        public static UIManager UI { get => Instance.uiManager; }
        public static RoomManager Room { get => Instance.roomManager; }
        public static SwitchManager Switch { get => Instance.switchManager; }

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
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        public static void Test()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
