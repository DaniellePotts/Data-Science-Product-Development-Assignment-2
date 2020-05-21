using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using System.Threading.Tasks;

public class APIClient : MonoBehaviour
{
	public delegate void GetApiResponse(string response);
	public event GetApiResponse onGetApiResponse;
    
    public void CallApi(string url, List<string> formFields, string method)
	{
        switch (method.ToUpper())
        {
            case "POST":
                StartCoroutine(MakePostRequest(url, formFields));
                break;
            case "GET":
                StartCoroutine(MakeGetRequest(url));
                break;
        }
	}

	IEnumerator MakePostRequest(string url, List<string> formData)
	{
		WWWForm form = new WWWForm();
        var data = JsonUtils.ToJson<string>(formData.ToArray());
		form.AddField("data", data);

		using (UnityWebRequest www = UnityWebRequest.Post(url, form))
		{
            yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
				{
					sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
				}

				var response = new List<APIResponse>();
				response.Add(new APIResponse(www.downloadHandler.text));

				onGetApiResponse?.Invoke(www.downloadHandler.text);
			}
		}
	}

    IEnumerator MakeGetRequest(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                var response = new List<APIResponse>();
                response.Add(new APIResponse(www.downloadHandler.text));

                onGetApiResponse?.Invoke(www.downloadHandler.text);
            }
        }
    }
}
