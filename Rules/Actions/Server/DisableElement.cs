namespace Morph.Forms.Rules.Actions.Server
{
  using System.Web.UI.WebControls;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Rules.Actions;

  /// <summary>
  /// Defines the hide element inputs class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class DisableElement<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control != null)
      {
        var webControl = ruleContext.Control as WebControl;
        if (webControl != null)
        {
          webControl.Enabled = false;
        }
      }
    }

    #endregion
  }
}