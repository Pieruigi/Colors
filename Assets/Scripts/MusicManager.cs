using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class MusicManager : MonoBehaviour
    {
        
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            audioSource.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
