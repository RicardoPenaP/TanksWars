using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.TimeCounters
{
    public class Timer
    {
        private Action timerFinishedCallback;
        private float currentTime = 0f;
        private float targetTime = 0f;
        private TimerMode timerMode;

        private bool isTimerActive = false;

        private readonly float startingValue;
        private readonly float objectiveValue;        


        public Timer(float duration, Action timerFinishedCallback, TimerMode timerMode = TimerMode.Default)
        {
            this.timerFinishedCallback = timerFinishedCallback;
            this.timerMode = timerMode;
            switch (this.timerMode)
            {
                case TimerMode.Default:                    
                case TimerMode.Incremental:
                    startingValue = 0f;
                    objectiveValue = duration;
                    break;
                case TimerMode.Decremental:
                    startingValue = duration;
                    objectiveValue = 0f;
                    break;
                default:
                    break;
            }
        }

        public void StartTimer()
        {
            isTimerActive = true;
            currentTime = startingValue;
            targetTime = objectiveValue;
        }

        public void UpdateTimer()
        {
            if (!isTimerActive)
            {
                return;
            }

            switch (timerMode)
            {
                case TimerMode.Default:                
                case TimerMode.Incremental:
                    currentTime += Time.deltaTime;
                    if (currentTime > targetTime)
                    {
                        TimerFinished();
                    }
                    break;
                case TimerMode.Decremental:
                    currentTime -= Time.deltaTime;
                    if (currentTime < targetTime)
                    {
                        TimerFinished();
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void TimerFinished()
        {
            timerFinishedCallback?.Invoke();
        }

    }
}
