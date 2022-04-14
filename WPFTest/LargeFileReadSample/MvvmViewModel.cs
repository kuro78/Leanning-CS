using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LargeFileReadSample;

public class MvvmViewModel : ObservableObject
{
    public IRelayCommand OpenCommand { get; set; }

    private IEnumerable<SampleData> _syncModels;
    public IEnumerable<SampleData> SyncModels
    {
        get { return _syncModels; }
        set {  SetProperty(ref _syncModels, value); }
    }

    public MvvmViewModel()
    {
        OpenCommand = new RelayCommand(OnOpen);
    }

    private void OnOpen()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "All files (*.*)|*.*"
        };
        if(dialog.ShowDialog() == false)
        {
            return;
        }

        var fileName = dialog.FileName;
        if(string.IsNullOrEmpty(fileName))
        {
            return; 
        }
        SyncModels = GetModels(fileName);
    }

    private IEnumerable<SampleData> GetModels(string fileName)
    {
        bool isFirstLine = true;
        using var reader = new StreamReader(fileName);
        while(!reader.EndOfStream)
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

    private SampleData GetModelFromString(string item)
    {
        var columns = item.Split(",");
        var model = new SampleData();
        var properties = model.GetType().GetProperties();
        for(int i = 0; i < properties.Length; i++)
        {
            var property = properties[i];
            property.SetValue(model, columns[i], null);
        }

        return model;
    }
}
