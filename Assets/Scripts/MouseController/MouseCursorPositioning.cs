using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MouseController
{
    public sealed class MouseCursorPositioning : MouseSimulate
    {
        public static void setCursorPosition(int cursorPositionX, int cursorPositionY)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);
        }

        public static void setCursorPositionByLaserPointer(Vector2 LaserPointerUVPosition)
        {
            int cursorPositionX = Mathf.RoundToInt(1920f * LaserPointerUVPosition.x);
            int cursorPositionY = Mathf.RoundToInt(1080f * LaserPointerUVPosition.y);

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);
        }

        public static void setCursorPositionByTrackPad(Vector2 TrackPadTouchDelta, float cursorSpeed)
        {
            Vector2 CursorPosition = new Vector2((float)System.Windows.Forms.Cursor.Position.X, (float)System.Windows.Forms.Cursor.Position.Y);
            Vector2 CursorDelta = cursorSpeed * TrackPadTouchDelta;
            CursorPosition = new Vector2(CursorPosition.x + CursorDelta.x, CursorPosition.y - CursorDelta.y);

            int cursorPositionX = Mathf.RoundToInt(CursorPosition.x);
            int cursorPositionY = Mathf.RoundToInt(CursorPosition.y);

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);

            Debug.Log("x:" + cursorPositionX + ", y:" + cursorPositionY);
        }

        public static void setCursorPositionByEyeTracking(Vector2 CombineFocusUVPosition)
        {
            if (CombineFocusUVPosition == Vector2.zero) return;

            int cursorPositionX = Mathf.RoundToInt(CombineFocusUVPosition.x);
            int cursorPositionY = Mathf.RoundToInt(CombineFocusUVPosition.y);

            Debug.Log("cursorX:" + cursorPositionX + ", cursorY:" + cursorPositionY);

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);
        }

        public static void setCursorPositionByHMDForward(Vector2 HMDForwardUVPosition)
        {
            if (HMDForwardUVPosition == Vector2.zero) return;

            int cursorPositionX = Mathf.RoundToInt(1920f*HMDForwardUVPosition.x);
            int cursorPositionY = Mathf.RoundToInt(1080f*HMDForwardUVPosition.y);

            //Debug.Log("cursorX:" + cursorPositionX + ", cursorY:" + cursorPositionY);

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);


        }

        public static Queue<float> setCursorPositionsByEyeFocus(Queue<float> positionParams, RaycastHit hit, bool xEnable)
        {
            if (xEnable)
            {
                positionParams.Enqueue(1920f * hit.textureCoord.x);
            }
            else
            {
                positionParams.Enqueue(1080f * hit.textureCoord.y);
            }

            if (positionParams.Count == 101)
            {
                positionParams.Dequeue();
            }

            return positionParams;
        }
        
        public static Queue<float> setCursorPositionsByGazeRay(Queue<float> positionParams, RaycastHit hit, Vector3 HMDLocalRotation, bool xEnable)
        {
            float rotate = 0f;
            
            if (xEnable)
            {
                positionParams.Enqueue(1920f - (1920f * hit.textureCoord.x)/* - (810f * Mathf.Abs(Mathf.Cos(HMDLocalRotation.y-90)))*/);
            }
            else
            {
                positionParams.Enqueue(1080f - 1080f * hit.textureCoord.y);
            }

            if (positionParams.Count == 101)
            {
                //positionParams.Enqueue(GetMedian(positionParams));
                positionParams.Dequeue();
            }

            return positionParams;
        }

        public static float GetPositionParamsMedian(Queue<float> positionParams)
        {
            List<float> positionList = new List<float>();

            foreach(float num in positionParams)
            {
                positionList.Add(num);
            }

            positionList.Sort();

            if (positionList.Count%2 == 0)
            {
                return (positionList[positionList.Count / 2 - 1] + positionList[positionList.Count / 2]) / 2;
            }
            else
            {
                return positionList[positionList.Count / 2];
            }
        }
    }
}
