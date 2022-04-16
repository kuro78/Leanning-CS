using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;

namespace DataGridNumberColumn;

/// <summary>
/// DataGridBehavior
/// </summary>
public class DataGridBehavior : Behavior<DataGrid>
{
    /// <summary>
    /// ShowrowNumber
    /// </summary>
    public bool ShowRowNumber { get; set; }

    protected override void OnAttached()
    {
        if(ShowRowNumber)
        {
            AssociatedObject.RowHeaderWidth = 40;
            AssociatedObject.LoadingRow += AssociatedObject_LoadingRow;
            AssociatedObject.UnloadingRow += AssociatedObject_UnloadingRow;
        }
    }

    /// <summary>
    /// 로우 언로딩 이벤트 - 삭제시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AssociatedObject_UnloadingRow(object sender, DataGridRowEventArgs e)
    {
        RefreshRowNumber();
    }

    /// <summary>
    /// 전체 번호 다시 출력
    /// </summary>
    private void RefreshRowNumber()
    {
        foreach(var item in AssociatedObject.Items)
        {
            if( AssociatedObject.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow row)
            {
                row.Header = row.GetIndex() + 1;
            }
        }
    }

    private void AssociatedObject_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        e.Row.Header = e.Row.GetIndex() + 1;
    }

    protected override void OnDetaching()
    {
        if(ShowRowNumber)
        {
            AssociatedObject.LoadingRow -= AssociatedObject_LoadingRow;
            AssociatedObject.UnloadingRow -= AssociatedObject_UnloadingRow;
        }
    }
}
