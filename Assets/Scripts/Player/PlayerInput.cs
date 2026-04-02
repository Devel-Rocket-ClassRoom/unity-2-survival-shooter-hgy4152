using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 문자열에 한해서 최적화를 잘 해줌
    // input manager 에서 쓰는 문자열 변수로 사용. 오타 방지
    public static readonly string MoveAxis = "Vertical";
    public static readonly string MoveRow = "Horizontal";
    public static readonly string FireButton = "Fire1";


    public float MoveForward { get; private set; }
    public float MoveSide { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire { get; private set; }


    private void Update()
    {
        MoveForward = Input.GetAxis(MoveAxis);
        MoveSide = Input.GetAxis(MoveRow);
        Fire = Input.GetButton(FireButton);



    }
}
