using UnityEngine;
using System.Collections;

public class AntRotation : MonoBehaviour
{

    public UISprite Ant;

    int moveSpeed = 3;
    int i = 1;
    // Use this for initialization
    void Start()
    {
        //TestForRotation();

    }

    public void RotateLeft()
    {
        i += 3;
        Ant.transform.localRotation = Quaternion.Euler( new Vector3(0,0,i));
        //Debug.LogError("aa" + i);
    }


    public void RotateRight()
    {
        i -= 3;
        Ant.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, i));
    }


    //向左移动
    public void OnLeftMoveClick()
    {
        Debug.LogError("OnLeftMoveClick");
        //transform.Translate(transform.localPosition * Time.deltaTime * 30f);
        Ant.transform.localPosition -= new Vector3(moveSpeed,0,0);
    }


    //向右移动
    public void OnRightMoveClick()
    {
        Debug.LogError("OnRightMoveClick");
        Ant.transform.localPosition += new Vector3(moveSpeed, 0, 0);

    }


    //向上移动
    public void OnUpMoveClick()
    {
        Debug.LogError("OnUpMoveClick");
        Ant.transform.localPosition += new Vector3(0,moveSpeed, 0);
    }


    //向下移动
    public void OnDownMoveClick()
    {
        Debug.LogError("OnDownMoveClick");
        Ant.transform.localPosition -= new Vector3(0,moveSpeed, 0);
    }




    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            TestForRotation();
        }
    }

    void TestForRotation()
    {
        GameObject pointA = GameObject.Find("PointA");
        GameObject pointB = GameObject.Find("PointB");
        Vector3 vecA = Ant.GetComponent<Transform>().localPosition;
        Vector3 vecB = Input.mousePosition;
        //Vector3 direction = vecB - vecA;                                    ///< 终点减去起点（AB方向与X轴的夹角）
        Vector3 direction = vecA - vecB;                                  ///< （BA方向与X轴的夹角）
        float angle = Vector3.Angle(direction, Vector3.right);              ///< 计算旋转角度
        direction = Vector3.Normalize(direction);                           ///< 向量规范化
        float dot = Vector3.Dot(direction, Vector3.up);                  ///< 判断是否Vector3.right在同一方向
        if (dot < 0)
            angle = 360 - angle;
        Debug.LogWarning("vecA：" + vecA.ToString() + ", vecB：" + vecB.ToString() + ", angle: " + angle.ToString());
        Ant.GetComponent<Transform>().Rotate(0, 0, angle);
    }*/
}
