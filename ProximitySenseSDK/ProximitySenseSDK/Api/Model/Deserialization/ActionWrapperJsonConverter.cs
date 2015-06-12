using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProximitySenseSDK.Api.Model.Deserialization
{
	internal class ActionWrapperJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var item = JObject.Load(reader);

			var type = item["type"].Value<string>();
			var result = (ActionBase) item["result"].ToObject(ActionBase.GetActionType(type));

			return new ActionWrapper
			{
				Type = type,
				Result = result
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ActionWrapper).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
		}
	}
}