using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace MVVMApplication.Models;

/// <summary>
/// Busy Message
/// </summary>
public class BusyMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// BusyID
    /// </summary>
    public string BusyID { get; set; }
    /// <summary>
    /// Busy Text
    /// </summary>
    public string BusyText { get; set; }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="value"></param>
    public BusyMessage(bool value) : base(value)
    {
    }
}
