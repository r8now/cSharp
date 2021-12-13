using Todos.Views;
using Xamarin.Forms;

namespace Todos
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TodoEntryPage), typeof(TodoEntryPage));
        }
    }
}