using UnityEngine;
using System.Collections;

namespace HelpArti
{
    public class PathManager : MonoBehaviour
    {
        public CanvasGroup PathBar;
        public CanvasGroup FullPath;

        public static PathManager I;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        public void Setup()
        {
            //todo
        }

        public void ShowUp()
        {
            PathBar.gameObject.SetActive(true);
        }

        public void Hide()
        {
            PathBar.gameObject.SetActive(false);
        }
    }
}
