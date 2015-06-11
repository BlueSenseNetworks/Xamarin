using System;
using System.Collections.Generic;

namespace ProximitySenseSDK.Api.Model
{
	public class ActionBase
	{
		private static Dictionary<string, Type> types = new Dictionary<string, Type>();

		public string Type { get; set; }
		public string Result { get; set; }

		public static ActionBase ParseActionResponse(string result)
		{
			return null;
		}

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
	}
}
