using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; // Recommended for JSON in Unity

namespace __ProjectMain.Scripts.Utilities.LevelEditor
{
    public static class LevelApi
    {
        private static readonly string BaseUrl = "https://infiltr8.sermar.de/api/level";
        private static readonly string BaseUrlAuth = "https://infiltr8.sermar.de/api/user";

        // --- Models ---
        [Serializable]
        public class LevelSummary
        {
            public string Id = "";
            public string Name = "";
            public string Author = "";
        }

        [Serializable]
        public class Level
        {
            public string Id = "";
            public string Name = "";
            public string Author = "";
            public string Content = "";
            public DateTime UploadDate;
        }

        [Serializable]
        public class PagedResult<T>
        {
            public List<T> Items = new List<T>();
            public int Page;
            public int PageSize;
            public int TotalItems;
            public int TotalPages;
        }

        [Serializable]
        public class JsonLevel
        {
            public string Name = "";
            public string Username = "";
            public string Password = "";
            public string Content = "";
        }

        [Serializable]
        public class AuthJson
        {
            public string Username = "";
            public string Password = "";
        }

        // --- GET paginated ---
        public static async Task<PagedResult<LevelSummary>> GetLevelsAsync(int page = 1, int pageSize = 10,
            string search = "")
        {
            var url = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(search))
                url += $"&search={UnityWebRequest.EscapeURL(search)}";

            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error fetching levels: {request.error}");
                return null;
            }

            return JsonConvert.DeserializeObject<PagedResult<LevelSummary>>(request.downloadHandler.text);
        }
        
        public static async Task<List<LevelSummary>> GetLevelsByUserAsync(string username)
        {
            // Build correct endpoint: /api/level/me?username={username}
            var url = $"{BaseUrl}/me?username={UnityWebRequest.EscapeURL(username)}";

            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
    if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error fetching levels for user {username}: {request.error}");
                return null;
            }

            return JsonConvert.DeserializeObject<List<LevelSummary>>(request.downloadHandler.text);
        }


        // --- GET by ID ---
        public static async Task<Level> GetLevelByIdAsync(string id)
        {
            using var request = UnityWebRequest.Get($"{BaseUrl}/{id}");
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error fetching level {id}: {request.error}");
                return null;
            }

            return JsonConvert.DeserializeObject<Level>(request.downloadHandler.text);
        }

        // --- POST / create ---
        public static async Task<Level> CreateLevelAsync(string name,  string content, string password, string username)
        {
            var json = JsonConvert.SerializeObject(new JsonLevel
            {
                Name = name,
                Content = content,
                Password = password,
                Username = username
            });

            using var request = new UnityWebRequest(BaseUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error creating level: {request.error}");
                return null;
            }

            return JsonConvert.DeserializeObject<Level>(request.downloadHandler.text);
        }

        // --- DELETE ---
        public static async Task<bool> DeleteLevelAsync(string id, string password, string username)
        {
            var json = JsonConvert.SerializeObject(new AuthJson
            {
                Password = password,
                Username = username
            });
            using var request = UnityWebRequest.Delete($"{BaseUrl}/{id}");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error deleting level {id}: {request.error}");
                return false;
            }

            return true;
        }
        
        public static async Task<bool> GetAuthState(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new AuthJson
            {
                Password = password,
                Username = username
            });
            using var request = UnityWebRequest.Get($"{BaseUrlAuth}");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("Content-Type", "application/json"); 

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error fetching auth status: {request.error}");
                return false;
            }

            return true;
        }
        
        public static async Task<bool> CreateUser(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new AuthJson
            {
                Password = password,
                Username = username
            });
            using var request = new UnityWebRequest(BaseUrlAuth, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("Content-Type", "application/json"); 

            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError($"Error creating account: {request.error}");
                return false;
            }

            return true;
        }
    }
}