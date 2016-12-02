// Author:  - http://www.demigiant.com
// Created: 2015/12/13 17:00
// License Copyright (c) Daniele Giardini
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace HelpArti
{
    public class LeftHandSideUIManager : MonoBehaviour
    {
        [Header("Configuration")]

        public Sprite Plus;
        public Sprite Multiply;
        public Sprite LeftParenthesis;
        public Sprite RightParenthesis;
        public Sprite EqualsImage;

        public Color[] NumberColors;

        /// <summary>
        /// Has a text child
        /// </summary>
        public Image NumberPrefab;
        public Image OperatorPrefab;

        public static LeftHandSideUIManager I;

        void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void DisplayProblemLeftHandSide(Problem problem, CanvasGroup container)
        {
            //clean container
            foreach (Transform child in container.transform)
            {
                Destroy(child.gameObject);
            }

            switch (problem.ProblemType)
            {
                case Problem.Type.SUM:
                    int index = 1;
                    foreach (int factor in problem.Factors)
                    {
                        Image instantiate = (Image)Instantiate(NumberPrefab, container.transform, false);
                        instantiate.transform.GetComponentInChildren<Text>().text = factor.ToString();
                        if (index < problem.Factors.Count)
                        {
                            Image operatorImage = (Image)Instantiate(OperatorPrefab, container.transform, false);
                            operatorImage.sprite = Plus;
                        }
                    }
                    break;
            }
        }

    }
}
