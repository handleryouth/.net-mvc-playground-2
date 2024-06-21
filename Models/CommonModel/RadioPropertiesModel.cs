
namespace contactForm.Models.CommonModel
{
	public class RadioPropertiesModel <T>
	{
		public readonly string text;
		public readonly T value;
        public readonly string id;


        public RadioPropertiesModel(string text, T value)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            id = Guid.NewGuid().ToString();
        }
    }
}

