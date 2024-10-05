using System.Collections;
using UnityEngine;
public class ExplodeWeapon : Weapon
{
    public float explodeDuration = 0.3f;

    private bool isAttacking = false;

    #region Controls
    public override void DoPrimary()
    {
        if (isAttacking)
        {
            return;
        }

        Explode();
    }

    public override void DoSecondary()
    {
        if (isAttacking)
        {
            return;
        }
    }
    #endregion

    #region Explode Weapon
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ColorEntity(weaponColor);
            // enemy.TakeDamage(1);
        }
    }
    #endregion

    #region Attacks
    private void Explode()
    {
        StartCoroutine(IExplode());
    }

    private IEnumerator IExplode()
    {
        isAttacking = true;

        Vector3 originalScale = transform.localScale;

        Vector3 targetScale = originalScale * 20;

        float elapsedTime = 0f;

        while (elapsedTime < explodeDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / explodeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        yield return new WaitForSeconds(explodeDuration);

        elapsedTime = 0f;

        while (elapsedTime < explodeDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / explodeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;

        isAttacking = false;
    }
    #endregion
}