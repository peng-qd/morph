namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing fields hide current field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldsHideCurrentField<T> : ChangingFieldsRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      var disableValidation = new ChangingFieldsToValuesDisableValidation<ConditionalRuleContext>();
      disableValidation.Trigger = this.Trigger;
      disableValidation.Trigger2 = this.Trigger2;
      disableValidation.TriggerValue = this.TriggerValue;
      disableValidation.TriggerValue2 = this.TriggerValue2;

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