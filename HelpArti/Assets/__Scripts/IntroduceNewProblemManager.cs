﻿using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class IntroduceNewProblemManager : MonoBehaviour
    {
        public CanvasGroup IntroduceNewProblem;

        public static IntroduceNewProblemManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void ShowUp()
        {
            IntroduceNewProblem.gameObject.SetActive(true);
        }

        public void Hide()
        {
            IntroduceNewProblem.gameObject.SetActive(false);
        }
    }
}
