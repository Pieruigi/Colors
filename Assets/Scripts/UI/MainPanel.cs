using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zom.Pie.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField]
        Button buttonPlay;

        [SerializeField]
        Button buttonCustomize;

        [SerializeField]
        Button buttonExit;

        // Start is called before the first frame update
        void Start()
        {
            buttonPlay.onClick.AddListener(() => { GameManager.Instance.LoadGameScene(); });
            buttonExit.onClick.AddListener(() => { GameManager.Instance.ApplicationQuit(); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
