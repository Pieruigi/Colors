using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class PlayerCamera : MonoBehaviour
    {
        float disp = 0;
        float minY = 32f;
        float maxY = 90;

        private void Awake()
        {
            int diff = 24 - 8;
            disp = (90 - 32) / (float)diff;
        }

        // Start is called before the first frame update
        void Start()
        {
            int rows = LevelManager.Instance.NumOfRows;
            float d = (rows - 8) * disp;

            Vector3 pos = transform.position;
            pos.y = minY + d;
            transform.position = pos;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
