using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Components
{
    public partial class PageIntroduction
    {
        [Parameter]
        public string Icon { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Description { get; set; }
    }
}
