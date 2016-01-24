namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the client action class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ClientAction<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control == null || !ruleContext.Control.Visible)
      {
        return;
      }

      ruleContext.Control.PreRender += (s, e) => RegisterScript(ruleContext.Control, this.PrepareScript(ruleContext.Control));
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The script.
    /// </returns>
    protected abstract string PrepareScript(Control control);

    /// <summary>
    /// Registers the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="script">The script.</param>
    private static void RegisterScript(Control control, string script)
    {
      Assert.ArgumentNotNull(control, "control");

      if (string.IsNullOrEmpty(script))
      {
        return;
      }

      var ready = "$scw(document).ready(function(){{{0}}});".FormatWith(script);

      ScriptManager.RegisterStartupScript(control, typeof(Page), ready, ready, true);
    }

    #endregion

  }
}