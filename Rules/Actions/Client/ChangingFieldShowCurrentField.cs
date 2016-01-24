namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field show current field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldShowCurrentField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      return "$(this).parent().parent().show();";
    }

    #endregion
  }
}