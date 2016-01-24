namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field clear value class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldClearValue<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      return "$(this).val('');";
    }
  }
}