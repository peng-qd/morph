namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field hide current field class.
  /// </summary>
  public class ChangingFieldHideCurrentField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      var disableValidation = new ChangingFieldToValueDisableValidation<ConditionalRuleContext>();
      disableValidation.Trigger = this.Trigger;
      disableValidation.TriggerValue = this.TriggerValue;

      disableValidation.Apply(ruleContext);

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      return "$(this).parent().parent().hide();";
    }

    #endregion
  }
}