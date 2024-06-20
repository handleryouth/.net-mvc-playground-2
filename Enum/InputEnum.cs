
public enum InputType
{
    file, text, checkbox, radiobox
}

namespace contactForm.Enum
{
	public static class InputEnum
	{
        public static string ToInputTypeString(this InputType enumName)
        {
            switch (enumName)
            {
                case InputType.file:
                    return "file";
                case InputType.text:
                    return "text";
                case InputType.checkbox:
                    return "checkbox";
                case InputType.radiobox:
                    return "radio";
                default:
                    throw new ArgumentOutOfRangeException(nameof(enumName), enumName, "please check the value!");
            }
        }
    }
}

