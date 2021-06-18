using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class LevelConfig
    {
        static LevelConfig instance;
        public static LevelConfig Instance
        {
            get { if (instance == null) Create(); return instance; }
        }

        public int NumOfRows { get; set; } = 2;

        public int NumOfColumns { get; set; } = 3;

        public int NumOfColors { get; set; } = 4;

        public float LoadFactor { get; set; } = 1f;

        public bool HorizontalSymmetry { get; set; } = true;

        public bool VerticalSymmetry { get; set; } = true;

        static void Create()
        {
            if (instance != null)
                return;

            instance = new LevelConfig();
        }

        private LevelConfig()
        {
            // We could read player prefs here
        }
    }

}
