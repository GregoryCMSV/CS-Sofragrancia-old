using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Components
{
    public partial class PowerBIComponent
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Source { get; set; }

    }
}
