using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Zom.Pie.UI
{
    public class MusicButton : MonoBehaviour
    {
        [SerializeField]
        Text buttonText;

        [SerializeField]
        AudioMixer mixer;

        bool musicOff = false;



        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SwitchMusicOnOff);    
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void SwitchMusicOnOff()
        {
            musicOff = !musicOff;

            mixer.SetFloat("MasterVolume", musicOff ? -80f : 0f);
            buttonText.text = musicOff ? "on" : "off";
        }
    }

}
