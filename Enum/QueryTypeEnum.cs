namespace contactForm.Enum;

public enum QueryType
{
	GeneralEnquiry,
	SupportRequest
}

public static class QueryTypeEnum
{
	public static string convertToQueryType(this QueryType queryType)
	{
		switch (queryType)
		{
			case QueryType.GeneralEnquiry:
				return "General Enquiry";
			case QueryType.SupportRequest:
				return "Support Request";
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}

