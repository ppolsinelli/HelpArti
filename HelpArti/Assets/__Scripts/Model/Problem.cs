using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HelpArti
{
    public class Problem
    {
        public Type ProblemType;
        public State ProblemState;

        public List<int> Factors = new List<int>();

        public int ProposedResult;
        public int ProposedRemainder;

        public int Result;
        public int Remainder;

        public enum Type
        {
            SUM,
            DIFFERENCE,
            MULTIPLICATION,
            DIVISION
        }

        public enum State
        {
            TO_BE_SETUPPED,
            NO_PROPOSED_SOLUTION,
            WRONG_SOLUTION,
            DONE
        }

        public Problem(int firstFactor, int secondFactor, Type type)
        {
            Factors[0] = firstFactor;
            Factors[1] = secondFactor;
            ProblemType = type;
            ProblemState = State.TO_BE_SETUPPED;
            ComputeResult();
        }
        
        public void ComputeResult()
        {
            switch (ProblemType)
            {
                case Type.SUM:
                    Result = Factors.Aggregate((a, x) => a + x);
                    break;

                case Type.MULTIPLICATION:
                    Result = Factors.Aggregate((a, x) => a * x);
                    break;
            }

            if (ProblemState == State.TO_BE_SETUPPED)
            {
                ProblemState = State.NO_PROPOSED_SOLUTION;
            }
        }

    }
}
