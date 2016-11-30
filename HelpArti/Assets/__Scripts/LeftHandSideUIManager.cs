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
        
        public Image Plus; 
        public Image Multiply; 
        public Image LeftParenthesis;
        public Image RightParenthesis;
        public Image EqualsImage;

        /// <summary>
        /// has a text child
        /// </summary>
        public Image NumberPrefab;

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
                    foreach (int factor in problem.Factors)
                    {
                        
                    }
                    break;
            }


        }



    }
}
