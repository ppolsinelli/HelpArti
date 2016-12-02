using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class WhereAboutManager : MonoBehaviour
    {
        public CanvasGroup WhereAbout;

        public static WhereAboutManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void ShowUp()
        {
            WhereAbout.gameObject.SetActive(true);
        }

        public void Hide()
        {
            WhereAbout.gameObject.SetActive(false);
        }

    }

}
