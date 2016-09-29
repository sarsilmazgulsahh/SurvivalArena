using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class TimeCounter : MonoBehaviour
    {
        Text text;                      

        void Awake ()
        {
            
            text = GetComponent <Text> ();
        }


		private float minutes = 0.0f;
		private bool timerTrigger = true;
		private float timespent = 0.0f;
		private float seconds = 0.0f;
		
		void Update ()
		{
			if(timerTrigger)
			{
				timespent += seconds;
				seconds += Time.deltaTime;
				if (seconds > 60)
				{
					minutes += 1;
					seconds = 0;
				}
			}
			text.text = "Time Survived: " + minutes + " Min. " + Mathf.RoundToInt(seconds) + " Sec.";
		}
    }
}