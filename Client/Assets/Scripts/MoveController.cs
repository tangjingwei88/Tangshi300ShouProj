using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{

    private float moveSpeed = 5;
    private float rotateSpeed = 5; 

    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    #region 方向按钮
    //向左移动
    public void OnLeftMoveClick()
    {
        Debug.LogError("OnLeftMoveClick");
        //transform.Translate(transform.localPosition * Time.deltaTime * 30f);
        transform.Translate(new Vector3(transform.localPosition.x * Time.deltaTime * moveSpeed,transform.localPosition.y,0));
    }


    //向右移动
    public void OnRightMoveClick()
    {
        Debug.LogError("OnRightMoveClick");
        transform.Translate(new Vector3(-transform.localPosition.x * Time.deltaTime * moveSpeed, transform.localPosition.y, 0));

    }


    //向上移动
    public void OnUpMoveClick()
    {
        Debug.LogError("OnUpMoveClick");
        transform.Translate(new Vector3(transform.localPosition.x, transform.localPosition.y * Time.deltaTime * moveSpeed,0));

    }


    //向下移动
    public void OnDownMoveClick()
    {
        Debug.LogError("OnDownMoveClick");
        transform.Translate(new Vector3(transform.localPosition.x, -transform.localPosition.y * Time.deltaTime * moveSpeed, 0));
    }

    #endregion

    #region  旋转按钮
    //向左旋转
    public void OnLeftRotateClick()
    {

    }


    //向右旋转
    public void OnRightRotateClick()
    {

    }
    #endregion

    //移动摇杆结束
    void OnJoystickMoveEnd(MovingJoystick move)
    {
        //停止时，角色恢复idle
        if (move.joystickName == "MoveJoystick")
        {
            //animation.CrossFade("idle");
        }
    }


    //移动摇杆中
    void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "MoveJoystick")
        {
            return;
        }

        //获取摇杆中心偏移的坐标
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {

            Debug.LogError("joyPositionX" + joyPositionX);
            Debug.LogError("joyPositionY" + joyPositionY);
            transform.localPosition = new Vector3( joyPositionX,joyPositionY,0);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, joyPositionY * Time.deltaTime * 30));
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            // transform.LookAt(new Vector3(0, 0, transform.position.z + joyPositionY));
            //移动玩家的位置（按朝向位置移动）
            transform.Translate(transform.localPosition * Time.deltaTime * 30f);
            
            //播放奔跑动画
           // animation.CrossFade("run");
        }
    }
}

