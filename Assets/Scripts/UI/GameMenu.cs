using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie.UI
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        bool open;

        bool enablePlayerOnClose = false;

        // Start is called before the first frame update
        void Start()
        {
            LevelManager.Instance.OnGameCompleted += OpenDelayed;

            panel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsOpen())
                    Close();
                else
                    Open();
            }
        }

        public void Open()
        {
            panel.SetActive(true);
            
           
            if (!PlayerController.Instance.Disabled)
            {
                PlayerController.Instance.Disabled = true;
                enablePlayerOnClose = true;
            }
            
        }

        public void Close()
        {
            panel.SetActive(false);
            if (enablePlayerOnClose)
            {
                enablePlayerOnClose = false;
                PlayerController.Instance.Disabled = false;
            }
   

        }

        public bool IsOpen()
        {
            return panel.activeSelf;
        }


        void OpenDelayed()
        {
            StartCoroutine(DoOpenDelayed());
        }

        IEnumerator DoOpenDelayed()
        {
            yield return new WaitForSeconds(2);

            Open();
        }
    }

}
