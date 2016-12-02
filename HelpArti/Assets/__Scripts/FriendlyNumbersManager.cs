using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class FriendlyNumbersManager : MonoBehaviour
    {

        public CanvasGroup FriendlyNumbers;

        public static FriendlyNumbersManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void ShowUp()
        {
            FriendlyNumbers.gameObject.SetActive(true);
        }

        public void Hide()
        {
            FriendlyNumbers.gameObject.SetActive(false);
        }

    }
}
