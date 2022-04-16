using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataGridNumberColumn;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IList<Person> _persons;

    public MainWindow()
    {
        InitializeComponent();

        _persons = new ObservableCollection<Person>
        {
            new Person { Name = "kuro7801", Sex = true, Age = 11, Address = "GyeongGi1" },
            new Person { Name = "kuro7802", Sex = false, Age = 12, Address = "GyeongGi2" },
            new Person { Name = "kuro7803", Sex = true, Age = 13, Address = "GyeongGi3" },
            new Person { Name = "kuro7804", Sex = false, Age = 14, Address = "GyeongGi4" },
            new Person { Name = "kuro7805", Sex = true, Age = 15, Address = "GyeongGi5" },
            new Person { Name = "kuro7806", Sex = false, Age = 16, Address = "GyeongGi6" },
            new Person { Name = "kuro7807", Sex = true, Age = 17, Address = "GyeongGi7" },
            new Person { Name = "kuro7808", Sex = false, Age = 18, Address = "GyeongGi8" },
            new Person { Name = "kuro7809", Sex = true, Age = 19, Address = "GyeongGi9" },
            new Person { Name = "kuro7810", Sex = true, Age = 20, Address = "GyeongGi10" },
            new Person { Name = "kuro7811", Sex = false, Age = 21, Address = "GyeongGi11" },
            new Person { Name = "kuro7812", Sex = true, Age = 22, Address = "GyeongGi12" },
            new Person { Name = "kuro7813", Sex = false, Age = 23, Address = "GyeongGi13" },
            new Person { Name = "kuro7814", Sex = true, Age = 24, Address = "GyeongGi14" },
            new Person { Name = "kuro7815", Sex = false, Age = 25, Address = "GyeongGi15" },
            new Person { Name = "kuro7816", Sex = true, Age = 26, Address = "GyeongGi16" },
            new Person { Name = "kuro7817", Sex = false, Age = 27, Address = "GyeongGi17" },
            new Person { Name = "kuro7818", Sex = true, Age = 28, Address = "GyeongGi18" },
            new Person { Name = "kuro7819", Sex = true, Age = 29, Address = "GyeongGi19" },
            new Person { Name = "kuro7820", Sex = false, Age = 30, Address = "GyeongGi20" }
        };
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        PeopleDataGrid.ItemsSource = _persons;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        _persons.Add(new Person { Name = "Add1", Age = 41, Address = "GyeongGi99" });
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        if(PeopleDataGrid.SelectedItem != null)
        {
            _persons.Remove(PeopleDataGrid.SelectedItem as Person);
        }
    }
}
