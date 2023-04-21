using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Platformer2D
{
    public class SetLocale : MonoBehaviour
    {
        [SerializeField] private Locale locale;

        public void Apply()
        {
            // Asettaa käytetyn käännösassetin
            LocalizationSettings.SelectedLocale = locale;
            Debug.Log("Locale set " + locale.ToString());
        }
    }
}
