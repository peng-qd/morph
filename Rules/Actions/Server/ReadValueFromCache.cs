namespace Morph.Forms.Rules.Actions.Server
{
  using System.Web;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the read value from cache class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ReadValueFromCache<T> : ReadValue<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (HttpContext.Current == null)
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <returns>
    /// The value.
    /// </returns>
    protected override object GetValue()
    {
      return HttpContext.Current.Cache[this.Name] ?? string.Empty;
    }

    #endregion
  }
}