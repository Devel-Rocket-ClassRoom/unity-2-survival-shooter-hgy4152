using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // 추후 총 교체할 때 배열로 받기
    // 입력 따라서 인덱스로 총 불러오기
    public Gun gun;
    private PlayerInput input;


    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }
    void Update()
    {
        if(input.Fire)
        {
            gun.Fire();


        }
    }
}
