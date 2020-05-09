namespace Redbridge.Tests
{
	//[TestFixture()]
	//public class ValidationExceptionDeserializationTests
	//{
	//	[Test()]
	//	public void TestCase()
	//	{
	//		var results = new ValidationResult[] { new ValidationResult() };

	//		string rawJson = JsonConvert.SerializeObject(results, new JsonSerializerSettings()
	//		{
	//			ContractResolver = new CamelCasePropertyNamesContractResolver()
	//		});

	//		var jsonResponseStream = rawJson.ToMemoryStream();
	//		dynamic body = jsonResponseStream.DeserializeJson();

	//		// For your future self, this makes no sense at all.
	//		// Depending on implementation, you may get back an array here.
	//		if (body is JArray)
	//		{
	//			var bodyItem = body[0]["message"];
	//			var message = bodyItem.Value;
	//			if (message != null)
	//				throw new ValidationException(message);
	//			else
	//				throw new ValidationException();
	//		}
	//		else
	//			throw new ValidationException(body.message.Value);

	//	}
	//}
}
