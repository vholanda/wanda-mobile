using Newtonsoft.Json;
using Plugin.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WandaWHTW
{
	static class Repository
	{
		public static Uri BaseUri = new Uri("http://wanda.gear.host/api/");
		//public static Uri BaseUri = new Uri("http://10.0.15.224:1972/api/");

		public static async Task<bool> Login(string rg, string password)
		{
			RestClient client = new RestClient(BaseUri);
			var request = new RestRequest("security/token", Method.POST)
				.AddObject(new
				{
					grant_type = "password",
					username = rg,
					password = password
				}, "grant_type", "username", "password")
				.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			var result = await client.ExecuteTaskAsync(request);
			var loginResult = (LoginResult)JsonConvert.DeserializeObject(result.Content, typeof(LoginResult));
			CrossSettings.Current.AddOrUpdateValue("token", loginResult.access_token);
			CrossSettings.Current.AddOrUpdateValue("current_conversation", loginResult.conversationId);
			return result.StatusCode == System.Net.HttpStatusCode.OK;
		}


		//public static async Task<ChatsPage.Model> CreateConsulta()
		//{
		//	RestClient client = GetDefaultClient();
		//	var request = new RestRequest("conversation", Method.POST);
		//	var result = await client.ExecuteTaskAsync(request);
		//	if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
		//	ChatsPage.Model m = (ChatsPage.Model)JsonConvert.DeserializeObject(result.Content, typeof(ChatsPage.Model));
		//	return m;
		//}

		public static async Task<HistoricChatPage> SendMessage(Guid conversationId, String msg)
		{
			RestClient client = GetDefaultClient();
			var request = new RestRequest("conversation/" + conversationId + "/message", Method.POST);
			request.AddJsonBody(new { message = msg });
			var result = await client.ExecuteTaskAsync(request);
			if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
			var hist = (HistoricChatPage)JsonConvert.DeserializeObject(result.Content, typeof(HistoricChatPage));
			return hist;
		}

		//public static async Task<bool> FinalizeConsulta(Guid conversationId)
		//{
		//	//RestClient client = GetDefaultClient();
		//	//var request = new RestRequest("conversation/" + conversationId, Method.PATCH);
		//	//var result = await client.ExecuteTaskAsync(request);
		//	//return result.StatusCode == System.Net.HttpStatusCode.OK;
		//}

		public static async Task<ChatsPage.Model[]> ListarChats()
		{
			RestClient client = GetDefaultClient();
			var request = new RestRequest("conversation", Method.GET);
			var result = await client.ExecuteTaskAsync(request);
			if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
			var listas = (ChatsPage.Model[])JsonConvert.DeserializeObject(result.Content, typeof(ChatsPage.Model[]));
			return listas;
		}

		internal static async Task<HistoricChatPage.Model> GetChat(Guid conversationId)
		{
			RestClient client = GetDefaultClient();
			var request = new RestRequest("conversation/" + conversationId.ToString()+  "/message", Method.GET);
			var result = await client.ExecuteTaskAsync(request);
			if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
			var lista = (HistoricChatPage.Model)JsonConvert.DeserializeObject(result.Content, typeof(HistoricChatPage.Model));
			return lista;
		}

		public static async Task<AgendaPage.Model[]> GetAgenda(DateTime date)
		{
			RestClient client = GetDefaultClient();
			var request = new RestRequest("schedule/?date=" + date.ToString("yyyy-MM-dd"), Method.GET);
			var result = await client.ExecuteTaskAsync(request);
			if (result.StatusCode != System.Net.HttpStatusCode.OK) return null;
			var lista = (AgendaPage.Model[])JsonConvert.DeserializeObject(result.Content, typeof(AgendaPage.Model[]));
			return lista;
		}

		public static RestClient GetDefaultClient()
		{
			RestClient client = new RestClient(BaseUri);
			client.AddDefaultHeader("Authorization", "bearer " + CrossSettings.Current.GetValueOrDefault<string>("token"));
			return client;
		}
	}
}
