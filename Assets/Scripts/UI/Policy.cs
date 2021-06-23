using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zom.Pie.UI
{
    public class Policy : MonoBehaviour
    {
        [SerializeField]
        string policyLink;

        [SerializeField]
        Button buttonPolicy;

        [SerializeField]
        Button buttonAgree;

        [SerializeField]
        GameObject panel;

        string policyPrefsKey = "policy";

        private void Awake()
        {

            // If you already agreed hide panel
            if(PlayerPrefs.HasKey(policyPrefsKey))
            {
                panel.SetActive(false);
            }
            else
            {
                // Set handles
                buttonPolicy.onClick.AddListener(() => { Application.OpenURL(policyLink); });
                buttonAgree.onClick.AddListener(() => { PlayerPrefs.SetString(policyPrefsKey, ""); panel.SetActive(false); });
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

  
    }

}
