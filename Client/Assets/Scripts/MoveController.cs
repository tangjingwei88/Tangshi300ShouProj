using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{

    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }


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
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, joyPositionX));
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            // transform.LookAt(new Vector3(transform.position.x + joyPositionX, transform.position.y, transform.position.z + joyPositionY));
            //移动玩家的位置（按朝向位置移动）
            transform.Translate(transform.localPosition * Time.deltaTime * 30f);
            
            //播放奔跑动画
           // animation.CrossFade("run");
        }
    }
}

