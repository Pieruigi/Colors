using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{

    public enum LevelSizeParam { Small, Medium, Big, Crazy, Random }

    public enum NumOfColorsParam { Two, Three, Four, Random }

    public enum LoadFactorParam { Low, High, Full, Random }

    public enum SymmetryParam { None, Horizontal, Vertical, Both, Random }

    public class LevelParams
    {
        static LevelParams instance;
        public static LevelParams Instance
        {
            get { if (instance == null) Create(); return instance; }
        }


        public LevelSizeParam LevelSizeParam { get; set; } = LevelSizeParam.Random;

        public NumOfColorsParam NumOfColorsParam { get; set; } = NumOfColorsParam.Random;

        public LoadFactorParam LoadFactorParam { get; set; } = LoadFactorParam.Random;

        public SymmetryParam SymmetryParam { get; set; } = SymmetryParam.Random;

        string sizeKey = "levelSize";
        string numOfColorsKey = "numOfColors";
        string loadFactorKey = "loadFactor";
        string symmetryKey = "symmetry";

        private LevelParams()
        {
            // Load from player prefs
            if (PlayerPrefs.HasKey(sizeKey))
                LevelSizeParam = (LevelSizeParam)PlayerPrefs.GetInt(sizeKey);
            if (PlayerPrefs.HasKey(numOfColorsKey))
                NumOfColorsParam = (NumOfColorsParam)PlayerPrefs.GetInt(numOfColorsKey);
            if (PlayerPrefs.HasKey(loadFactorKey))
                LoadFactorParam = (LoadFactorParam)PlayerPrefs.GetInt(loadFactorKey);
            if (PlayerPrefs.HasKey(symmetryKey))
                SymmetryParam = (SymmetryParam)PlayerPrefs.GetInt(symmetryKey);
        }

        static void Create()
        {
            instance = new LevelParams();
            
        }
    }

}
