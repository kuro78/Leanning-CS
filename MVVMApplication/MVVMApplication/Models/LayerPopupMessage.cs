using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace MVVMApplication.Models;

/// <summary>
/// Layer Popup Message
/// </summary>
public class LayerPopupMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// 컨트롤 이름
    /// </summary>
    public string ControlName { get; set; }
    /// <summary>
    /// 컨트롤에 전달할 파라미터
    /// </summary>
    public object Parameter { get; set; } = null;
    public LayerPopupMessage(bool value):base(value)
    {
    }
}
