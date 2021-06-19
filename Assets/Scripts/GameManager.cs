using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zom.Pie
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        int mainSceneIndex = 0;
        int gameSceneIndex = 1;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(gameSceneIndex);
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(mainSceneIndex); 
        }

        public void ApplicationQuit()
        {
            Application.Quit();
        }
    }

}
