namespace Morph.Forms.Rules.Actions.Server
{
  using System.Globalization;

  using Sitecore;
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the read from visitor class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ReadFromVisitor<T> : ReadValue<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (!Tracker.IsActive || Tracker.Current.Contact == null)
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
      switch (this.Name.ToLower(CultureInfo.InvariantCulture))
      {
        case "visitcount":
          return Tracker.Current.Contact.System.VisitCount;
      }
      return string.Empty;
    }

    #endregion
  }
}