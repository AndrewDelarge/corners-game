using System;
using Core.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : MonoBehaviour
    {
        public event Action OnSceneLoaded;

        private AsyncOperation sceneLoadingOperation;

        public void LoadScene(CoreData.SceneIndexes scene, Action<AsyncOperation> onCurrentLoadCallback = null)
        {
//            isLoading = true;

            //loadingScreen.Show();
            sceneLoadingOperation = SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Single);
            if (onCurrentLoadCallback != null)
                sceneLoadingOperation.completed += onCurrentLoadCallback;
            
            sceneLoadingOperation.completed += OnLoaded;
        }
        
        public void ReLoadScene(CoreData.SceneIndexes scene, Action<AsyncOperation> onFinish = null)
        {
            LoadScene(CoreData.SceneIndexes.EMPTY, Reload);
            
            void Reload(AsyncOperation operation)
            {
                LoadScene(scene, onFinish);
            }
        }
        

        private void OnLoaded(AsyncOperation operation)
        {
            //loadingScreen.Hide();
//            isLoading = false;
            OnSceneLoaded?.Invoke();
            sceneLoadingOperation = null;
        }
        
        private void FixedUpdate()
        {
            //if (isLoading)
            //    loadingScreen.SetProgress(sceneLoadingOperation.progress);
        }
    }
}