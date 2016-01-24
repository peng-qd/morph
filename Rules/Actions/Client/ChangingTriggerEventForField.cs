namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the changing trigger event for field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingTriggerEventForField<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>
    /// The event.
    /// </value>
    public string Event { get; set; }

    /// <summary>
    /// Gets or sets the trigger.
    /// </summary>
    /// <value>
    /// The trigger.
    /// </value>
    public string Trigger { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger) || string.IsNullOrEmpty(this.Event))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The script.
    /// </returns>
    protected override string PrepareScript(Control control)
    {
      Control trigger = this.GetField(control, this.Trigger);
      if (trigger == null || control.Page == null)
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope");
      var observeControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope");

      if (triggerControl == null || observeControl == null)
      {
        return string.Empty;
      }

      return "$scw('#{2}').change(function() {{$scw('#{0}').trigger('{1}', [$scw(this).val()])}})".FormatWith(
        triggerControl.ClientID, 
        this.Event, 
        observeControl.ClientID);
    }

    #endregion

  }
}