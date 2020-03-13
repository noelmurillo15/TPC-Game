/*
 * ProjectileSpell - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace ANM.Scriptables
{
    [CreateAssetMenu(menuName = "Attack Action/Projectile Spell")]
    public class ProjectileSpell : SpellAction
    {
        public GameObject projectile;
    }
}
