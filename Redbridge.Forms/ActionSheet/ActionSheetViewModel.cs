using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public class ActionSheetOption<T> : ActionSheetOption
	{
		public ActionSheetOption() { }
		public ActionSheetOption(T value) 
		{
			Value = value;
		}

		public new T Value { get; set; }
	}

	public class ActionSheetOption
	{
		public string Title { get; set; }
		public object Value { get; set; }

		public static ActionSheetOption<T> FromOption<T> (T option)
		{
			var enumType = typeof(T);
			var typeInfo = enumType.GetTypeInfo();
			FieldInfo fieldInfo = null;

			if (IsNullableEnum(enumType))
			{ 
				fieldInfo = typeInfo.GetDeclaredField("value");
			}
			else
				fieldInfo = typeInfo.GetDeclaredField(option.ToString());
			
			var descriptionAttribute = fieldInfo.GetCustomAttributes<EnumDescriptionAttribute>().FirstOrDefault();
			var displayText = option.ToString();
			if (descriptionAttribute != null)
			{
				displayText = descriptionAttribute.Description;
			}
			return new ActionSheetOption<T>(option) { Title = displayText };
		}

		public static bool IsNullableEnum(Type t)
		{
			Type u = Nullable.GetUnderlyingType(t);
			return (u != null) && u.GetTypeInfo().IsEnum;
		}
	}

	public class ActionSheetViewModel<T> : ViewModel
	{
		public ActionSheetViewModel()
		{
			CancelMessage = "Cancel";
		}

		public string Title { get; set; }
		public string DestructionMessage { get; set; }
		public string CancelMessage { get; set; }
		public ActionSheetOption<T>[] Options { get; set; }
	}

	public class ActionSheetViewModel : ViewModel
	{
		public ActionSheetViewModel()
		{
			CancelMessage = "Cancel";
		}

		public string Title { get; set; }
		public string DestructionMessage { get; set; }
		public string CancelMessage { get; set; }
		public object Owner { get; set; }
		public ActionSheetOption[] Options { get; set; }

		public static ActionSheetViewModel<T> FromEnum<T>(string title = "Select value", string cancelText = "Cancel", string destructionText = null)
		{
			return new ActionSheetViewModel<T>()
			{
				Title = title,
				CancelMessage = cancelText,
				Options = CreateOptionsFromEnum<T>(),
				DestructionMessage = destructionText,
			};
		}

		public static ActionSheetOption<T>[] CreateOptionsFromEnum<T>(object owner = null)
		{
			var options = new List<ActionSheetOption<T>>();
			var typeInfo = typeof(T).GetTypeInfo();
			var fields = typeInfo.DeclaredFields.Where(df => df.FieldType == typeof(T)).ToArray();

			foreach (var field in fields)
			{
				var option = new ActionSheetOption<T>();

				var descriptionAttribute = field.GetCustomAttributes<EnumDescriptionAttribute>().FirstOrDefault();

				if (descriptionAttribute == null)
					option.Title = field.Name;
				else
					option.Title = descriptionAttribute.Description;

				option.Value = (T)field.GetValue(null);
				options.Add(option);
			}

			return options.ToArray();
		}
	}
}
