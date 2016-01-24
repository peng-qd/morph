namespace Morph.Forms.Rules.Conditions
{
  using Sitecore;
  using Sitecore.Rules;
  using Sitecore.Rules.Conditions;
  using Sitecore.Web;

  /// <summary>
  /// Defines the query string contains class.
  /// </summary>
  /// <typeparam name="T"> Rule context </typeparam>
  public class QueryStringContains<T> : WhenCondition<T> where T : RuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the name of the query string.
    /// </summary>
    /// <value>
    /// The name of the query string.
    /// </value>
    public string QueryStringName { get; set; }

    #endregion

    #region Protected methods

    /// <summary>
    /// Executes the specified rule context.
    /// </summary>
    /// <param name="ruleContext">
    /// The rule context.
    /// </param>
    /// <returns>
    /// The boolean.
    /// </returns>
    protected override bool Execute(T ruleContext)
    {
      return !string.IsNullOrEmpty(this.QueryStringName) && 
             !string.IsNullOrEmpty(WebUtil.GetQueryString(this.QueryStringName, null));
    }

    #endregion
  }
}