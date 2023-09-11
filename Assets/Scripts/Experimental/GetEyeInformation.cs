using UnityEngine;
using ViveSR.anipal.Eye;

public class GetEyeInformation : MonoBehaviour
{
    //取得呼び出し-----------------------------
    //呼び出したデータ格納用の関数
    EyeData eye;
    //-------------------------------------------

    //瞳孔位置--------------------
    //x,y軸
    //左の瞳孔位置格納用関数
    Vector2 LeftPupil;
    //左の瞳孔位置格納用関数
    Vector2 RightPupil;
    //------------------------------

    //まぶたの開き具合------------
    //左のまぶたの開き具合格納用関数
    float leftBlink;
    //右のまぶたの開き具合格納用関数
    float rightBlink;
    //------------------------------

    //視線情報--------------------
    //origin：起点，direction：レイの方向　x,y,z軸
    //両目の視線格納変数
    Vector3 CombineGazeRayorigin;
    Vector3 CombineGazeRaydirection;
    //左目の視線格納変数
    Vector3 LeftGazeRayorigin;
    Vector3 LeftGazeRaydirection;
    //右目の視線格納変数
    Vector3 RightGazeRayorigin;
    Vector3 RightGazeRaydirection;
    //------------------------------

    //焦点情報--------------------
    //両目の焦点格納変数
    //レイの始点と方向（多分③の内容と同じ）
    Ray CombineRay;
    /*レイがどこに焦点を合わせたかの情報．Vector3 point : 視線ベクトルと物体の衝突位置，float distance : 見ている物体までの距離，
       Vector3 normal:見ている物体の面の法線ベクトル，Collider collider : 衝突したオブジェクトのCollider，Rigidbody rigidbody：衝突したオブジェクトのRigidbody，Transform transform：衝突したオブジェクトのTransform*/
    //焦点位置にオブジェクトを出すためにpublicにしています．
    public static FocusInfo CombineFocus;
    //レイの半径
    //float combineFocusradius;
    //レイの最大の長さ
    //float combineFocusmaxDistance;
    //オブジェクトを選択的に無視するために使用されるレイヤー ID
    //int combinefocusableLayer = 0;

    void Update()
    {
        //おまけ------------------------------------
        //エラー確認ViveSR.Error.がWORKなら正常に動いている．（フレームワークの方に内蔵済みだからいらないかも）
        if (SRanipal_Eye_API.GetEyeData(ref eye) == ViveSR.Error.WORK)
        {
            //一応機器が正常に動いてる時の処理をここにかける
        }
        //-------------------------------------------


        //取得呼び出し-----------------------------
        SRanipal_Eye_API.GetEyeData(ref eye);
        //-------------------------------------------

        //瞳孔位置---------------------（HMDを被ると検知される，目をつぶっても位置は返すが，HMDを外すとと止まる．目をつぶってるときはどこの値返してんのか謎．一応まぶた貫通してるっぽい？？？）
        //getPupilPosition();
        //------------------------------

        //まぶたの開き具合------------（HMDを被ってなくても1が返ってくる？？謎）
        //getEyeOpenness();
        //------------------------------


        //視線情報--------------------（目をつぶると検知されない）
        getGazeRay();
        //------------------------------

        //焦点情報--------------------
        //getEyeFocusPoint();
        //------------------------------
    }

    void getPupilPosition()
    {
        //左の瞳孔位置を取得
        if (SRanipal_Eye.GetPupilPosition(EyeIndex.LEFT, out LeftPupil))
        {
            //値が有効なら左の瞳孔位置を表示
            Debug.Log("Left Pupil" + LeftPupil.x + ", " + LeftPupil.y);
        }
        //右の瞳孔位置を取得
        if (SRanipal_Eye.GetPupilPosition(EyeIndex.RIGHT, out RightPupil))
        {
            //値が有効なら右の瞳孔位置を表示
            Debug.Log("Right Pupil" + RightPupil.x + ", " + RightPupil.y);
        }
    }

    void getEyeOpenness()
    {
        //左のまぶたの開き具合を取得
        if (SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out leftBlink, eye))
        {
            //値が有効なら左のまぶたの開き具合を表示
            Debug.Log("Left Blink" + leftBlink);
        }
        //右のまぶたの開き具合を取得
        if (SRanipal_Eye.GetEyeOpenness(EyeIndex.RIGHT, out rightBlink, eye))
        {
            //値が有効なら右のまぶたの開き具合を表示
            Debug.Log("Right Blink" + rightBlink);
        }
    }

    void getGazeRay()
    {
        //両目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
        if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out CombineGazeRayorigin, out CombineGazeRaydirection, eye))
        {
            Debug.Log("COMBINE GazeRayorigin" + CombineGazeRayorigin.x + ", " + CombineGazeRayorigin.y + ", " + CombineGazeRayorigin.z);
            Debug.Log("COMBINE GazeRaydirection" + CombineGazeRaydirection.x + ", " + CombineGazeRaydirection.y + ", " + CombineGazeRaydirection.z);
        }

        //左目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
        if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out LeftGazeRayorigin, out LeftGazeRaydirection, eye))
        {
            Debug.Log("Left GazeRayorigin" + LeftGazeRayorigin.x + ", " + LeftGazeRayorigin.y + ", " + LeftGazeRayorigin.z);
            Debug.Log("Left GazeRaydirection" + LeftGazeRaydirection.x + ", " + LeftGazeRaydirection.y + ", " + LeftGazeRaydirection.z);
        }


        //右目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
        if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out RightGazeRayorigin, out RightGazeRaydirection, eye))
        {
            Debug.Log("Right GazeRayorigin" + RightGazeRayorigin.x + ", " + RightGazeRayorigin.y + ", " + RightGazeRayorigin.z);
            Debug.Log("Right GazeRaydirection" + RightGazeRaydirection.x + ", " + RightGazeRaydirection.y + ", " + RightGazeRaydirection.z);
        }
    }

    void getEyeFocusPoint()
    {
        //radius, maxDistance，CombinefocusableLayerは省略可
        if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus/*, combineFocusradius, combineFocusmaxDistance, combinefocusableLayer*/))
        {
            int cursorMaxPositionX = 1920;
            int cursorMaxPositionY = 1080;

            int cursorPositionX = Mathf.RoundToInt(500.0f * CombineFocus.point.x);
            int cursorPositionY = Mathf.RoundToInt(500.0f * CombineFocus.point.y);

            if (cursorPositionX > cursorMaxPositionX)
            {
                cursorPositionX = cursorMaxPositionX;
            }
            if (cursorPositionY > cursorMaxPositionY)
            {
                cursorPositionY = cursorMaxPositionY;
            }

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(cursorPositionX, cursorPositionY);

            Debug.Log("Combine Focus Point" + CombineFocus.point.x + ", " + CombineFocus.point.y + ", " + CombineFocus.point.z);
            Debug.Log("x:" + System.Windows.Forms.Cursor.Position.X + ", y:" + System.Windows.Forms.Cursor.Position.Y);
        }
    }
}
