using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components

{
    public partial class TabNavigation
    {
        [Parameter]
        public List<TabItem> Tabs { get; set; } = new();
        [Parameter]
        public string ActiveTab { get; set; }
        [Parameter]
        public EventCallback<string> ActiveTabChanged { get; set; }
        private async Task OnTabClick(string tabId)
        {
            await ActiveTabChanged.InvokeAsync(tabId);
        }

        public class TabItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Icon { get; set; }
        }
    }
}