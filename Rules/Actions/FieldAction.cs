namespace Morph.Forms.Rules.Actions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Rules.Actions;

  /// <summary>
  /// Defines the field action class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class FieldAction<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// Gets the trigger field.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="id">The id.</param>
    /// <returns>
    /// The trigger field.
    /// </returns>
    protected Control GetField(Control context, string id)
    {
      Assert.ArgumentNotNull(context, "context");

      if (string.IsNullOrEmpty(id))
      {
        return null;
      }

      var parent = context.Page;

      if (parent == null)
      {
        return null;
      }

      return parent.Controls.Flatten().OfType<BaseControl>().FirstOrDefault(c => c.FieldID == id);
    }

    /// <summary>
    /// Gets the control matching any id.
    /// </summary>
    /// <param name="controls">The controls.</param>
    /// <param name="ids">The ids.</param>
    /// <returns>
    /// The control matching any id.
    /// </returns>
    protected Control GetChildMatchingAnyId(IEnumerable<Control> controls, params string[] ids)
    {
      Assert.ArgumentNotNull(controls, "controls");
      Assert.ArgumentNotNull(ids, "ids");

      return ids.Select(id => controls.FirstOrDefault(c => c.ClientID.EndsWith(id, StringComparison.OrdinalIgnoreCase))).FirstOrDefault(control => control != null);
    }
  }
}