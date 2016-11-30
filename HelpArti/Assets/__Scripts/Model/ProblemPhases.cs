using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HelpArti
{
    public class ProblemPhases
    {
        public Problem Root;

        public Dictionary<Problem,Problem> ProblemTree = new Dictionary<Problem, Problem>();

        public void Setup()
        {
            CreateNewProblem();
        }
        
        public void CreateNewProblem()
        {
            Root = new Problem(Random.Range(0,100), Random.Range(0, 100),Problem.Type.MULTIPLICATION);
        }

        public void DoProblemTransform()
        {
            
        }

    }
}
