using UnityEngine;

public interface IHealthInterface
{
    /// <summary>
    /// General health system for enemies and player. Enemies and player pull from this.
    /// </summary>
    /// <returns></returns>
    int GetHealth();
    int GetMaxHealth();
}
