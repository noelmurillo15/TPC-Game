/*
 * ProjectileSpell SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Actions/ProjectileSpell")]
    public class ProjectileSpell : SpellAction
    {
        public GameObject projectile;
    }
}