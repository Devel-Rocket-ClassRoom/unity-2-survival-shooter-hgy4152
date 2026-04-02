using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // 나중에 다른 총 추가할 거 생각해서 진행

    protected float coolTime = 0.3f;
    protected float lastShot = 0f;
    protected int damage = 20;
    protected float maxDistance = 10f; // 저격, 샷건일 때 바꾸기

    public ParticleSystem shotEffect;
    public LineRenderer lineRenderer;

    public Transform FirePos;
    public LayerMask targetLayer;

    private Coroutine coShot;

    public virtual void Fire()
    {
        if(Time.time > lastShot + coolTime)
        {
            lastShot = Time.time;


            Vector3 hitPoint = Vector3.zero;
            Ray ray = new Ray(FirePos.position, FirePos.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayer))
            {
                hitPoint = hit.point;
                // 적 데미지
                GameObject enemy = hit.collider.gameObject;

                if(enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().OnDamage(damage, hitPoint);
                }

            }
            else
            {
                // 안맞았을 때도 이펙트 출력
                hitPoint = FirePos.position + FirePos.forward * maxDistance;
            }


            // 멈추고 다시 재생
            if (coShot != null)
            {
                StopCoroutine(coShot);
                coShot = null;
            }
            
            coShot = StartCoroutine(CoShotEffect(hitPoint));




        }    
    }

    IEnumerator CoShotEffect(Vector3 hitPoint)
    {

        shotEffect.Play();

        lineRenderer.SetPosition(0, FirePos.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;

    }


}
