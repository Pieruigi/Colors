using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zom.Pie.UI
{
    public class OptionGroup : MonoBehaviour
    {
        enum OptionType { LevelSize, NumOfColors, LoadFactor, Symmetry }

        [SerializeField]
        OptionType optionType;

        [SerializeField]
        Text textLabel;

        [SerializeField]
        Text textValue;

        [SerializeField]
        Button buttonPrev;

        [SerializeField]
        Button buttonNext;

        //[SerializeField]
        string[] options;

        int currentId = -1;

        private void Awake()
        {
            SetLabel();

            buttonPrev.onClick.AddListener(Prev);
            buttonNext.onClick.AddListener(Next);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            InitParam();
            UpdateUI();
        }

        void Prev()
        {
            if (currentId == 0)
                return;

            currentId--;

            UpdateParam();
            UpdateUI();
        }

        void Next()
        {
            if (currentId == options.Length-1)
                return;

            currentId++;

            UpdateParam();
            UpdateUI();
        }


        void InitParam()
        {
            switch (optionType)
            {
                case OptionType.LevelSize:
                    currentId = (int)LevelParams.Instance.LevelSizeParam;
                    options = (new string[] { "Small", "Medium", "Big", "Crazy", "Random" });
                    break;
                case OptionType.NumOfColors:
                    currentId = (int)LevelParams.Instance.NumOfColorsParam;
                    options = (new string[] { "Two", "Three", "Four", "Random" });
                    break;
                case OptionType.LoadFactor:
                    currentId = (int)LevelParams.Instance.LoadFactorParam;
                    options = (new string[] { "Low", "High", "Full", "Random" });
                    break;
                case OptionType.Symmetry:
                    currentId = (int)LevelParams.Instance.SymmetryParam;
                    options = (new string[] { "None", "Horizontal", "Vertical", "Both", "Random" });
                    break;
            }

           
        }

        void UpdateParam()
        {
            switch (optionType)
            {
                case OptionType.LevelSize:
                    LevelParams.Instance.SetLevelSizeParam((LevelSizeParam)currentId);
                    break;
                case OptionType.NumOfColors:
                    LevelParams.Instance.SetNumOfColorsParam((NumOfColorsParam)currentId);
                    break;
                case OptionType.LoadFactor:
                    LevelParams.Instance.SetLoadFactorParam((LoadFactorParam)currentId);
                    break;
                case OptionType.Symmetry:
                    LevelParams.Instance.SetSymmetryParam((SymmetryParam)currentId);
                    break;
            }

        }

        void UpdateUI()
        {
            // Set buttons
            buttonPrev.interactable = false;
            buttonNext.interactable = false;
            if (currentId < options.Length - 1)
            {
                buttonNext.interactable = true;
            }
            if (currentId > 0)
            {
                buttonPrev.interactable = true;
            }

            // Label
            textValue.text = options[currentId];
        }

        void SetLabel()
        {
            switch (optionType)
            {
                case OptionType.LevelSize:
                    textLabel.text = "Level Size";
                    break;
                case OptionType.NumOfColors:
                    textLabel.text = "Number Of Colors";
                    break;
                case OptionType.LoadFactor:
                    textLabel.text = "Load Factor";
                    break;
                case OptionType.Symmetry:
                    textLabel.text = "Symmetry";
                    break;
            }
        }
    }

}
