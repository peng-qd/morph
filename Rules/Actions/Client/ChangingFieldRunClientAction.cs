namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the changing field run client action class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ChangingFieldRunClientAction<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Fields

    /// <summary>
    /// The client script template
    /// </summary>
    private readonly static string clientScriptTemplate = "$scw('#{0}').change(function(){{ (function d($, p) {{ var el = $('[name=\"{1}\"]'); if (new RegExp('{2}').test($(el.filter(':checked')[0] || ($(el[0]).is(':checkbox') ? $() : el[0])).val() || '')) {{ $scw.each([$scw('#{3}')], function(){{{4}}})}}}}).apply(this, [$scw]) }}).triggerHandler('change');";

    #endregion
    
    #region Properties

    /// <summary>
    /// Gets or sets the field trigger.
    /// </summary>
    /// <value>
    /// The field trigger.
    /// </value>
    public string Trigger { get; set; }

    /// <summary>
    /// Gets or sets the trigger value.
    /// </summary>
    /// <value>
    /// The trigger value.
    /// </value>
    public string TriggerValue { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.BuildClientScript()) ||
          string.IsNullOrEmpty(this.Trigger))
      {
        return;
      }

      base.Apply(ruleContext);
    }

  /// <summary>
    /// Registers the script.
    /// </summary>
    /// <param name="control">The control.</param>
    protected override string PrepareScript(Control control)
    {
      Control trigger = this.GetField(control, this.Trigger);
      if (trigger == null || control.Page == null)
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope", trigger.ID + "checkbox");
      var observeControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope", control.ID + "checkbox");

      if (triggerControl == null || observeControl == null)
      {
        return string.Empty;
      }

      return clientScriptTemplate.FormatWith(
        triggerControl.ClientID,
        triggerControl.UniqueID,
        this.TriggerValue,
        observeControl.ClientID,
        this.BuildClientScript() ?? string.Empty);
    }

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected abstract string BuildClientScript();

    #endregion
  }
}