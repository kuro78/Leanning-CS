using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LargeFileReadSample;

/// <summary>
/// Interaction logic for YieldWindow.xaml
/// </summary>
public partial class YieldWindow : Window
{
    public YieldWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            Filter = "All files(*.*)|*.*"
        };

        var result = dialog.ShowDialog();
        if(result == false)
        {
            return;
        }
        var fileName = dialog.FileName;
        if(string.IsNullOrEmpty(fileName))
        {
            return;
        }

        var models = GetModels(fileName);
        // 모델을 한번에 ItemSource에 연결
        //listBox.ItemsSource = models;
        int count = 0;
        // total count는 구할 수 없음
        foreach(var model in models)
        {
            count++;
            if (count % 1000 == 0)
            {
                CountTextBlock.Text = count.ToString("N0");
                await Task.Delay(1);
            }
            listBox.Items.Add(model);
        }
        CountTextBlock.Text = count.ToString("N0");
    }

    private SampleData GetModelFromString(string item)
    {
        var columns = item.Split(",");
        var model = new SampleData();
        var propertys = model.GetType().GetProperties();
        for (int i = 0; i < propertys.Count(); i++)
        {
            var property = propertys[i];
            property.SetValue(model, columns[i], null);
        }
        return model;
    }

    private IEnumerable<SampleData> GetModels(string fileName)
    {
        bool isFirstLine = true;
        using(var reader = new StreamReader(fileName))
        {
            while(reader.EndOfStream == false)
            {
                var line = reader.ReadLine();
                if(isFirstLine == false && line != null)
                {
                    var model = GetModelFromString(line);
                    yield return model;
                }
                else
                {
                    isFirstLine = false;
                }
            }

            reader.Close();
        }
    }
}
