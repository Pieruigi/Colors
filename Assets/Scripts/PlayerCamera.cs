using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class PlayerCamera : MonoBehaviour
    {
        float disp = 3.2f;
        float defaultY = 32.56f;


        // Start is called before the first frame update
        void Start()
        {
            int rows = LevelManager.Instance.NumOfRows;
            float d = (rows - 8) * disp;

            Vector3 pos = transform.position;
            pos.y = defaultY + d;
            transform.position = pos;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
