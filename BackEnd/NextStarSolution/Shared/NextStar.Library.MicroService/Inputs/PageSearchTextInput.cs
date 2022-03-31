namespace NextStar.Library.MicroService.Inputs;

public class PageSearchTextInput : PageSortInput
{
    public string SearchText { get; set; } = string.Empty;
}