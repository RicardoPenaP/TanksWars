using Gameplay.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;

        private void Start()
        {
            inputReader.OnFireInputUpdated += (context) => {
                if(context)
                {
                    Debug.Log("Fire Button Pressed");
                    
                }
                else
                {
                    Debug.Log("Fire Button Not Pressed");
                }
            };

            inputReader.OnMoveInputUpdated += (context) => {
                Debug.Log(context);
            };
        }

    }
}
