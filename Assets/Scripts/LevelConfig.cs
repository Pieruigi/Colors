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

        public int NumOfRows { get; set; } = 6;

        public int NumOfColumns { get; set; } = 9;

        public int NumOfColors { get; set; } = 4;

        public float LoadFactor { get; set; } = 0.7f;

        public bool HorizontalSymmetry { get; set; }

        public bool VerticalSymmetry { get; set; }

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
