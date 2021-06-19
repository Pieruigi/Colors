using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject[] panels;

        private void Awake()
        {
            OpenPanel(0);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenPanel(int id)
        {
            CloseAll();
            panels[id].SetActive(true);
        }

        void CloseAll()
        {
            foreach (GameObject panel in panels)
                panel.SetActive(false);
        }
    }

}
