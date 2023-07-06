using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD
{
    public class ButtonInWindow : MonoBehaviour
    {
        public Button button;

        public void DisableActive() 
        {
            button.interactable = false;
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }

}