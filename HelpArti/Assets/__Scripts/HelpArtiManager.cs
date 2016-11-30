using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class HelpArtiManager : MonoBehaviour
    {
        public static HelpArtiManager I;

        void Awake()
        {
            if (I == null)
            {
                I = this;
                DontDestroyOnLoad(this);
            }
        }

        void Start()
        {
            MathProblemManager.I.Setup();
        }

    }
}
