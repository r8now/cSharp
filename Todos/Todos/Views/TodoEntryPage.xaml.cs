using System;
using System.IO;
using Todos.Models;
using Xamarin.Forms;

namespace Todos.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TodoEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public TodoEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Todo.
            BindingContext = new Todo();
        }

        void LoadNote(string filename)
        {
            try
            {
                // Retrieve the todo and set it as the BindingContext of the page.
                Todo todo = new Todo
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    
                    Date = File.GetCreationTime(filename)
                };
                BindingContext = todo;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load todo.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var todo = (Todo)BindingContext;

            if (string.IsNullOrWhiteSpace(todo.Filename))
            {
                // Save the file.
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.todos.txt");
                File.WriteAllText(filename, todo.Text);
            }
            else
            {
                // Update the file.
                File.WriteAllText(todo.Filename, todo.Text);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todo = (Todo)BindingContext;

            // Delete the file.
            if (File.Exists(todo.Filename))
            {
                File.Delete(todo.Filename);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}