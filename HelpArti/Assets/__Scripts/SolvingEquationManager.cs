using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class SolvingEquationManager : MonoBehaviour
    {

        public CanvasGroup SolvingEquation;

        public static SolvingEquationManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void ShowUp()
        {
            SolvingEquation.gameObject.SetActive(true);
        }

        public void Hide()
        {
            SolvingEquation.gameObject.SetActive(false);
        }
    }
}
