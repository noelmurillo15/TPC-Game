/*
 * MonoExtension -
 * Created by : Allan N. Murillo
 * Last Edited : 3/4/2020
 */

using UnityEngine;
using System.Collections;
using Actions = System.Action;

namespace ANM.Framework.Extensions
{
    public static class MonoExtension
    {
        public static void InvokeAfter(this MonoBehaviour mono, Actions method, float delay)
        {
            mono.StartCoroutine(InvokeAfterRoutine(method, delay));
        }

        private static IEnumerator InvokeAfterRoutine(Actions method, float delay)
        {
            yield return new WaitForSeconds(delay);
            method();
        }
    }
}
