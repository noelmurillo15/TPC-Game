/*
 * MonoExtension -
 * Created by : Allan N. Murillo
 * Last Edited : 3/4/2020
 */

using UnityEngine;
using System.Collections;
using Action = System.Action;

namespace ANM.Framework.Extensions
{
    public static class MonoExtension
    {
        public static void InvokeAfter(this MonoBehaviour mono, Action method, float delay)
        {
            mono.StartCoroutine(InvokeAfterRoutine(method, delay));
        }

        private static IEnumerator InvokeAfterRoutine(Action method, float delay)
        {
            yield return new WaitForSeconds(delay);
            method();
        }
    }
}
