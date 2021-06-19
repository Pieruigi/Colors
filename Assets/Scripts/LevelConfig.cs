using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    

    public class LevelConfig
    {
    
        public int NumOfRows { get; private set; }

        public int NumOfColumns { get; private set; }

        public int NumOfColors { get; private set; }

        public float LoadFactor { get; private set; }

        public bool HorizontalSymmetry { get; private set; }

        public bool VerticalSymmetry { get; private set; }

        int[] minRowsSteps = new int[] { 8, 12, 16, 20, 24 };
        
        //int[] minColsSteps = new int[] { 4, 6, 9, 12, 16 };

        int[] colorsSteps = new int[] { 2, 3, 4 };

        float[] loadFactorSteps = new float[] { 0.6f, 0.8f, 1f };

        public LevelConfig(LevelParams levelParams)
        {
            // Set configuration
            // Set size
            float minRatio = 0.5f, maxRatio = 0.75f;
            float ratio = Random.Range(minRatio, maxRatio);
            if (levelParams.LevelSizeParam == LevelSizeParam.Random)
            {
                NumOfRows = Random.Range(minRowsSteps[0], minRowsSteps[minRowsSteps.Length-1] + 1);
                //NumOfColumns = (int)((float)NumOfRows * maxRatio);

            }
            else
            {
                int paramId = (int)levelParams.LevelSizeParam;
                NumOfRows = Random.Range(minRowsSteps[paramId], minRowsSteps[paramId + 1] + 1);
            }
            NumOfColumns = (int)((float)NumOfRows * ratio);

            //NumOfRows = 24;
            //NumOfColumns = (int)((float)NumOfRows * maxRatio);



            // Set colors
            if (levelParams.NumOfColorsParam == NumOfColorsParam.Random)
            {
                NumOfColors = Random.Range(2, 5);
            }
            else
            {
                int paramId = (int)levelParams.NumOfColorsParam;
                NumOfColors = colorsSteps[paramId];
                
            }

            // Set load factor
            if (levelParams.LoadFactorParam == LoadFactorParam.Random)
            {
                LoadFactor = Random.Range(loadFactorSteps[0], loadFactorSteps[loadFactorSteps.Length-1]);
            }
            else
            {
                
                if(levelParams.LoadFactorParam == LoadFactorParam.Full)
                {
                    LoadFactor = 1;
                }
                else
                {
                    int paramId = (int)levelParams.LoadFactorParam;
                    LoadFactor = Random.Range(loadFactorSteps[paramId], loadFactorSteps[paramId + 1]);
                }
        
            }

            // Symmetry
            HorizontalSymmetry = false;
            VerticalSymmetry = false;
            switch (levelParams.SymmetryParam)
            {
                case SymmetryParam.Random:
                    HorizontalSymmetry = Random.Range(0, 2) == 0 ? false : true;
                    VerticalSymmetry = Random.Range(0, 2) == 0 ? false : true;
                    break;

                case SymmetryParam.Both:
                    HorizontalSymmetry = true;
                    VerticalSymmetry = true;
                    break;
                case SymmetryParam.Horizontal:
                    HorizontalSymmetry = true;
                    break;
                case SymmetryParam.Vertical:
                    VerticalSymmetry = true;
                    break;
            }
            
  
        }
    }

}
