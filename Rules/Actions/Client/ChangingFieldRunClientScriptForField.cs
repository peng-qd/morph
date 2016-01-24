namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field run client script for field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldRunClientScriptForField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the script.
    /// </summary>
    /// <value>
    /// The script.
    /// </value>
    public string Script { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the script.
    /// </summary>
    /// <returns>
    /// The script.
    /// </returns>
    protected override string BuildClientScript()
    {
      return this.Script ?? string.Empty;
    }

    #endregion
  }
}