// Author: Pietro Polsinelli - http://designAGame.eu
// Twitter https://twitter.com/ppolsinelli
// All free as in free beer :-)

// Modified from DemiLib
// http://demigiant.github.io/apis/demilib/html/namespace_d_g_1_1_de_extensions.html


using UnityEngine;

namespace OL
{
    public class TheScene : MonoBehaviour
    {
        public GameObject[] activateOnStartup, deactivateOnStartup;
        
        void Start()
        {
            foreach (GameObject go in activateOnStartup)
            {
                if (go != null)
                {
                go.SetActive(true);
                if (go.GetComponent<CanvasGroup>() != null)
                    go.GetComponent<CanvasGroup>().alpha = 1;
            }
            }
            foreach (GameObject go in deactivateOnStartup)
            {
                if (go != null)
                {
                go.SetActive(false);
                if (go.GetComponent<CanvasGroup>() != null)
                    go.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        }
    }
}
