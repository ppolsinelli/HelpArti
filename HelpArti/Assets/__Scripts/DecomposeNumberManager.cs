using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class DecomposeNumberManager : MonoBehaviour
    {
        public CanvasGroup DecomposeNumber;

        public static DecomposeNumberManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void ShowUp()
        {
            DecomposeNumber.gameObject.SetActive(true);
        }

        public void Hide()
        {
            DecomposeNumber.gameObject.SetActive(false);
        }
    }
}
