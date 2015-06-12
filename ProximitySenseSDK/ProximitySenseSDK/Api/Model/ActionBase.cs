using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProximitySenseSDK.Api.Model
{
	public abstract class ActionBase
	{
		private static Dictionary<string, Type> types = new Dictionary<string, Type>();

		public string Type { get; set; }

		public string AppSpecificId { get; set; }
		public ZoneEventDetails ZoneEvent { get; set; }
		public Sighting Sighting { get; set; }

		public static void RegisterActionType<T>(string name)
			where T : ActionBase
		{
			if (types.ContainsKey(name))
				throw new ArgumentException("name");

			types.Add(name, typeof(T));
		}

		public static void RegisterCommonActionTypes()
		{
			RegisterActionType<RichContentAction>("richContent");
		}

		public static ActionBase ParseActionResponse(string resultType, string result)
		{
			if (!types.ContainsKey(resultType))
				return null;

			return (ActionBase) JsonConvert.DeserializeObject(result, types[resultType]);
		}

		public static Type GetActionType(string resultType)
		{
			return types[resultType];
		}
	}
}
