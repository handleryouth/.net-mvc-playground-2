
namespace contactForm.VariableModels;

public class HeaderModels
{
    public string title;
    public string actionName;
    public string controllerName;

    public HeaderModels(string title, string actionName, string controllerName)
    {
        this.title = title;
        this.actionName = actionName;
        this.controllerName = controllerName;
    }
}



