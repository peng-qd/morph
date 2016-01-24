namespace Morph.Forms.Rules.Actions.Server
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Rules.Actions;

  /// <summary>
  /// Defines the set read only class.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class SetReadOnly<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">
    /// The rule context.
    /// </param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control != null)
      {
        ruleContext.Control.Load += this.OnControlLoaded;
      }
    }

    /// <summary>
    /// Called when the control has loaded.
    /// </summary>
    /// <param name="sender">The sender control.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected virtual void OnControlLoaded(object sender, EventArgs e)
    {
      Assert.ArgumentNotNull(sender, "sender");

      IEnumerable<TextBox> controls = ((Control)sender).Controls.Flatten().OfType<TextBox>();
      foreach (TextBox textBox in controls)
      {
        textBox.ReadOnly = true;
      }
    }

    #endregion
  }
}