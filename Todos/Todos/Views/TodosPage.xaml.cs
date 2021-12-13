using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Todos.Models;
using Xamarin.Forms;

namespace Todos.Views
{
    public partial class TodosPage : ContentPage
    {
        public TodosPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var todos = new List<Todo>();

            // Create a Todo object from each file.
            var files = Directory.EnumerateFiles(App.FolderPath, "*.todos.txt");
            foreach (var filename in files)
            {
                todos.Add(new Todo
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            // Set the data source for the CollectionView to a
            // sorted collection of todos.
            collectionView.ItemsSource = todos
                .OrderBy(d => d.Date)
                .ToList();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the TodoEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(TodoEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the TodoEntryPage, passing the filename as a query parameter.
                Todo todo = (Todo)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(TodoEntryPage)}?{nameof(TodoEntryPage.ItemId)}={todo.Filename}");
            }
        }
    }
}