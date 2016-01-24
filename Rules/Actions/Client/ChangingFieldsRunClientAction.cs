namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the changing fields run script class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ChangingFieldsRunClientAction<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Fields

    /// <summary>
    /// The client script template
    /// </summary>
    private readonly static string clientScriptTemplate = "$scw('#{3}').change(function(){{$scw('[name=\"{1}\"]').first().trigger('change')}});$scw('#{0}').change(function(){{(function d($) {{var el1=$('[name=\"{1}\"]'); el2=$('[name=\"{4}\"]'); if (new RegExp('{2}').test($(el1.filter(':checked')[0] || ($(el1[0]).is(':checkbox') ? $() : el1[0])).val() || '') {6} new RegExp('{5}').test($(el2.filter(':checked')[0] || ($(el2[0]).is(':checkbox') ? $() : el2[0])).val() || '')) {{ $scw.each([$scw('#{7}')], function(){{{8}}})}};}}).apply(this, [$scw])}}).triggerHandler('change');";

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the trigger2.
    /// </summary>
    /// <value>
    /// The trigger2.
    /// </value>
    public string Trigger2 { get; set; }

    /// <summary>
    /// Gets or sets the trigger value2.
    /// </summary>
    /// <value>
    /// The trigger value2.
    /// </value>
    public string TriggerValue2 { get; set; }

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    /// <value>
    /// The operator.
    /// </value>
    public string Operator { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger2) || string.IsNullOrEmpty(this.Operator))
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
      Control trigger2 = this.GetField(control, this.Trigger2);
      if (trigger == null || trigger2 == null || control.Page == null || Context.Database == null)
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope");
      var triggerControl2 = this.GetChildMatchingAnyId(trigger2.Controls.Flatten(), trigger2.ID, trigger2.ID + "scope");
      var observeControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope");

      var operatorItem = Context.Database.GetItem(this.Operator);

      if (triggerControl == null || triggerControl2 == null || observeControl == null || operatorItem == null)
      {
        return string.Empty;
      }

      return clientScriptTemplate.FormatWith(
        triggerControl.ClientID,
        triggerControl.UniqueID,
        this.TriggerValue,
        triggerControl2.ClientID,
        triggerControl2.UniqueID,
        this.TriggerValue2,
        operatorItem.Name == "and" ? "&&" : "||",
        observeControl.ClientID,
        this.BuildClientScript() ?? string.Empty);
    }

    #endregion
  }
}