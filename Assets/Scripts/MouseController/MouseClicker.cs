//using UnityEngine;

namespace MouseController
{
    public sealed class MouseClicker : MouseSimulate
    {
        private static bool once = true;

        public static void clickOnce(int clickPatern, bool controllerState) //一度だけクリックする
        {
            switch(clickPatern)
            {
                case 0:
                    if (controllerState && once)
                    {
                        MouseSimulate.LeftDownClick();
                        MouseSimulate.LeftUpClick();
                        once = false;
                    }
                    else
                    {
                        MouseSimulate.LeftUpClick();
                        once = true;
                    }
                    break;
                case 1:
                    if (controllerState)
                    {
                        MouseSimulate.RightDownClick();
                        MouseSimulate.RightUpClick();
                    }
                    else
                    {
                        MouseSimulate.RightUpClick();
                    }
                    break;
                case 2:
                    if (controllerState)
                    {
                        MouseSimulate.MiddleDownClick();
                        MouseSimulate.MiddleUpClick();
                    }
                    else
                    {
                        MouseSimulate.MiddleUpClick();
                    }
                    break;
            }
        }

        public static void clickDown(int clickPatern, bool controllerState) //controllerStateがtrueの間マウスボタンを押し続ける
        {
            switch (clickPatern)
            {
                case 0:
                    if (controllerState)
                    {
                        MouseSimulate.LeftDownClick();
                    }
                    else
                    {
                        MouseSimulate.LeftUpClick();
                    }
                    break;
                case 1:
                    if (controllerState)
                    {
                        MouseSimulate.RightDownClick();
                    }
                    else
                    {
                        MouseSimulate.RightUpClick();
                    }
                    break;
                case 2:
                    if (controllerState)
                    {
                        MouseSimulate.MiddleDownClick();
                    }
                    else
                    {
                        MouseSimulate.MiddleUpClick();
                    }
                    break;
            }
        }
    }
}