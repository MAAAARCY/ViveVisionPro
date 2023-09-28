using ViveSR.anipal.Eye;
using UnityEngine;

namespace EyeTracking
{
    public class EyeTrackingInfo
    {
        private static EyeData eye;

        //�ق̊J���
        private static float leftBlink;
        private static float rightBlink;

        //�ق��J���Ă��邩�ǂ�������p
        private static bool leftOpenness = true;
        private static bool rightOpenness = true;

        //���b���ق���Ă��邩�̌v���p�^�C�}�[
        private static float timer = 0.0f;

        public static bool LeftOpenness
        {
            get
            {
                SRanipal_Eye_API.GetEyeData(ref eye);

                if (SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out leftBlink, eye) && leftBlink == 0)
                {
                    //leftOpenness = false;
                    if (leftOpenness) timer += Time.deltaTime;
                    
                    if (timer > 0.5f)
                    {
                        leftOpenness = false;
                        timer = 0.0f;
                    }
                }
                else
                {
                    leftOpenness = true;
                }

                return leftOpenness;
            }
        }

        public static bool RightOpenness
        {
            get
            {
                SRanipal_Eye_API.GetEyeData(ref eye);

                if (SRanipal_Eye.GetEyeOpenness(EyeIndex.RIGHT, out rightBlink, eye) && rightBlink == 0)
                {
                    rightOpenness = false;
                }
                else
                {
                    rightOpenness = true;
                }

                return rightOpenness;
            }
        }
    }
}