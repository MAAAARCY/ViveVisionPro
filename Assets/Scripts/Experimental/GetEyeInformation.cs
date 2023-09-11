using UnityEngine;
using ViveSR.anipal.Eye;

public class GetEyeInformation : MonoBehaviour
{
    //�擾�Ăяo��-----------------------------
    //�Ăяo�����f�[�^�i�[�p�̊֐�
    EyeData eye;
    //-------------------------------------------

    //���E�ʒu--------------------
    //x,y��
    //���̓��E�ʒu�i�[�p�֐�
    Vector2 LeftPupil;
    //���̓��E�ʒu�i�[�p�֐�
    Vector2 RightPupil;
    //------------------------------

    //�܂Ԃ��̊J���------------
    //���̂܂Ԃ��̊J����i�[�p�֐�
    float leftBlink;
    //�E�̂܂Ԃ��̊J����i�[�p�֐�
    float rightBlink;
    //------------------------------

    //�������--------------------
    //origin�F�N�_�Cdirection�F���C�̕����@x,y,z��
    //���ڂ̎����i�[�ϐ�
    Vector3 CombineGazeRayorigin;
    Vector3 CombineGazeRaydirection;
    //���ڂ̎����i�[�ϐ�
    Vector3 LeftGazeRayorigin;
    Vector3 LeftGazeRaydirection;
    //�E�ڂ̎����i�[�ϐ�
    Vector3 RightGazeRayorigin;
    Vector3 RightGazeRaydirection;
    //------------------------------

    //�œ_���--------------------
    //���ڂ̏œ_�i�[�ϐ�
    //���C�̎n�_�ƕ����i�����B�̓��e�Ɠ����j
    Ray CombineRay;
    /*���C���ǂ��ɏœ_�����킹�����̏��DVector3 point : �����x�N�g���ƕ��̂̏Փˈʒu�Cfloat distance : ���Ă��镨�̂܂ł̋����C
       Vector3 normal:���Ă��镨�̖̂ʂ̖@���x�N�g���CCollider collider : �Փ˂����I�u�W�F�N�g��Collider�CRigidbody rigidbody�F�Փ˂����I�u�W�F�N�g��Rigidbody�CTransform transform�F�Փ˂����I�u�W�F�N�g��Transform*/
    //�œ_�ʒu�ɃI�u�W�F�N�g���o�����߂�public�ɂ��Ă��܂��D
    public static FocusInfo CombineFocus;
    //���C�̔��a
    //float combineFocusradius;
    //���C�̍ő�̒���
    //float combineFocusmaxDistance;
    //�I�u�W�F�N�g��I��I�ɖ������邽�߂Ɏg�p����郌�C���[ ID
    //int combinefocusableLayer = 0;

    void Update()
    {
        //���܂�------------------------------------
        //�G���[�m�FViveSR.Error.��WORK�Ȃ琳��ɓ����Ă���D�i�t���[�����[�N�̕��ɓ����ς݂����炢��Ȃ������j
        if (SRanipal_Eye_API.GetEyeData(ref eye) == ViveSR.Error.WORK)
        {
            //�ꉞ�@�킪����ɓ����Ă鎞�̏����������ɂ�����
        }
        //-------------------------------------------


        //�擾�Ăяo��-----------------------------
        SRanipal_Eye_API.GetEyeData(ref eye);
        //-------------------------------------------

        //���E�ʒu---------------------�iHMD����ƌ��m�����C�ڂ��Ԃ��Ă��ʒu�͕Ԃ����CHMD���O���ƂƎ~�܂�D�ڂ��Ԃ��Ă�Ƃ��͂ǂ��̒l�Ԃ��Ă�̂���D�ꉞ�܂Ԃ��ђʂ��Ă���ۂ��H�H�H�j
        //getPupilPosition();
        //------------------------------

        //�܂Ԃ��̊J���------------�iHMD�����ĂȂ��Ă�1���Ԃ��Ă���H�H��j
        //getEyeOpenness();
        //------------------------------


        //�������--------------------�i�ڂ��Ԃ�ƌ��m����Ȃ��j
        getGazeRay();
        //------------------------------

        //�œ_���--------------------
        //getEyeFocusPoint();
        //------------------------------
    }

    void getPupilPosition()
    {
        //���̓��E�ʒu���擾
        if (SRanipal_Eye.GetPupilPosition(EyeIndex.LEFT, out LeftPupil))
        {
            //�l���L���Ȃ獶�̓��E�ʒu��\��
            Debug.Log("Left Pupil" + LeftPupil.x + ", " + LeftPupil.y);
        }
        //�E�̓��E�ʒu���擾
        if (SRanipal_Eye.GetPupilPosition(EyeIndex.RIGHT, out RightPupil))
        {
            //�l���L���Ȃ�E�̓��E�ʒu��\��
            Debug.Log("Right Pupil" + RightPupil.x + ", " + RightPupil.y);
        }
    }

    void getEyeOpenness()
    {
        //���̂܂Ԃ��̊J������擾
        if (SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out leftBlink, eye))
        {
            //�l���L���Ȃ獶�̂܂Ԃ��̊J�����\��
            Debug.Log("Left Blink" + leftBlink);
        }
        //�E�̂܂Ԃ��̊J������擾
        if (SRanipal_Eye.GetEyeOpenness(EyeIndex.RIGHT, out rightBlink, eye))
        {
            //�l���L���Ȃ�E�̂܂Ԃ��̊J�����\��
            Debug.Log("Right Blink" + rightBlink);
        }
    }

    void getGazeRay()
    {
        //���ڂ̎�����񂪗L���Ȃ王������\��origin�F�N�_�Cdirection�F���C�̕���
        if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out CombineGazeRayorigin, out CombineGazeRaydirection, eye))
        {
            Debug.Log("COMBINE GazeRayorigin" + CombineGazeRayorigin.x + ", " + CombineGazeRayorigin.y + ", " + CombineGazeRayorigin.z);
            Debug.Log("COMBINE GazeRaydirection" + CombineGazeRaydirection.x + ", " + CombineGazeRaydirection.y + ", " + CombineGazeRaydirection.z);
        }

        //���ڂ̎�����񂪗L���Ȃ王������\��origin�F�N�_�Cdirection�F���C�̕���
        if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out LeftGazeRayorigin, out LeftGazeRaydirection, eye))
        {
            Debug.Log("Left GazeRayorigin" + LeftGazeRayorigin.x + ", " + LeftGazeRayorigin.y + ", " + LeftGazeRayorigin.z);
            Debug.Log("Left GazeRaydirection" + LeftGazeRaydirection.x + ", " + LeftGazeRaydirection.y + ", " + LeftGazeRaydirection.z);
        }


        //�E�ڂ̎�����񂪗L���Ȃ王������\��origin�F�N�_�Cdirection�F���C�̕���
        if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out RightGazeRayorigin, out RightGazeRaydirection, eye))
        {
            Debug.Log("Right GazeRayorigin" + RightGazeRayorigin.x + ", " + RightGazeRayorigin.y + ", " + RightGazeRayorigin.z);
            Debug.Log("Right GazeRaydirection" + RightGazeRaydirection.x + ", " + RightGazeRaydirection.y + ", " + RightGazeRaydirection.z);
        }
    }

    void getEyeFocusPoint()
    {
        //radius, maxDistance�CCombinefocusableLayer�͏ȗ���
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
