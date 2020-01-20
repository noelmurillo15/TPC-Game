﻿/*
    *   SceneLoader - Transitions to Main Menu after a delay
    *   Created by : Allan N. Murillo
 */
using System.IO;
using UnityEngine;
using System.Collections;
using GameFramework.Events;
using UnityEngine.SceneManagement;


namespace GameFramework
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string[] sceneNames = null;
        [SerializeField] private int currentSceneIndex = 0;
        [SerializeField] private float transitionDelay = 2f;

        public GameEvent OnLoadScene;
        public GameEvent OnFinishLoadScene;


        public void Initialize()
        {
            var sceneNumber = SceneManager.sceneCountInBuildSettings;
            sceneNames = new string[sceneNumber];

            for (int i = 0; i < sceneNumber; i++)
            {
                sceneNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }

            if (SceneManager.GetActiveScene().name == sceneNames[0])
            {
                LoadMainMenu();
            }
        }

        public void LoadMainMenu()
        {
            StartCoroutine(LoadNewScene("MainMenu"));
        }

        public void LoadCreditsScene()
        {
            StartCoroutine(LoadNewScene("CreditsScreen"));
        }

        public void LoadLevel(int index)
        {
            if (index > sceneNames.Length - 1)
            {
                Debug.LogError("Scene Index out of range : " + index);
            }
            else
            {
                StartCoroutine(LoadNewScene(index));
            }
        }

        public void LoadLevel(string name)
        {
            foreach (var t in sceneNames)
            {
                if (!t.Contains(name)) continue;
                StartCoroutine(LoadNewScene(t));
                return;
            }

            Debug.LogError("SceneLoader::Could not find scene : " + name);
        }

        public static string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        private IEnumerator LoadNewScene(int index)
        {
            OnLoadScene.Raise();
            Debug.Log("__________ Loading : " + sceneNames[index] + " Scene __________");
            yield return new WaitForSeconds(transitionDelay);
            AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            while (!async.isDone) { yield return null; }
            yield return new WaitForSeconds(0.25f);
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("__________ Finsihed Loading : " + sceneNames[currentSceneIndex] + " Scene __________");
            OnFinishLoadScene.Raise();
        }

        private IEnumerator LoadNewScene(string name)
        {
            OnLoadScene.Raise();
            Debug.Log("__________ Loading : " + name + " Scene __________");
            yield return new WaitForSeconds(transitionDelay);
            AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            while (!async.isDone) { yield return null; }
            yield return new WaitForSeconds(0.25f);
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("__________ Finsihed Loading : " + sceneNames[currentSceneIndex] + " Scene __________");
            OnFinishLoadScene.Raise();
        }
    }
}