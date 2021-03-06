﻿using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class MathProblemManager : MonoBehaviour
    {
        public ProblemPhases Current;
        public MathProblemManagerState State;

        public static MathProblemManager I;

        public enum MathProblemManagerState
        {
            TO_BE_SETUPPED,
            PAUSED,
            PRESENTING_PROBLEM,
            FINDING_WHERE_ABOUT,
            SOLVING_PROBLEM,
            PROBLEM_SOLVED
        }

        void Awake()
        {
            if (I == null)
            {
                I = this;
            }            
        }

        public void Setup()
        {
            State = MathProblemManagerState.TO_BE_SETUPPED;
            CreateProblem();
            State = MathProblemManagerState.PRESENTING_PROBLEM;
            
            WhereAboutManager.I.Hide();
            FriendlyNumbersManager.I.Hide();
            SolvingEquationManager.I.Hide();
            DecomposeNumberManager.I.Hide();

            PathManager.I.ShowUp();
            PathManager.I.Setup();
            IntroduceNewProblemManager.I.ShowUp();
        }

        public void CreateProblem()
        {
            Current = new ProblemPhases();
            Current.Setup();
        }

    }
}

