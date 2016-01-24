namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field copy value from field class.
  /// </summary>
  public sealed class ChangingFieldCopyValueFromField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Holder))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets or sets the holder.
    /// </summary>
    /// <value>
    /// The holder.
    /// </value>
    public string Holder { get; set; }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The script.
    /// </returns>
    protected override string PrepareScript(Control control)
    {
      var holder = this.GetField(control, this.Holder);
      if (holder == null)
      {
        return string.Empty;
      }

      var holderControl = this.GetChildMatchingAnyId(holder.Controls.Flatten(), holder.ID, holder.ID + "scope");
      if (holderControl == null)
      {
        return string.Empty;
      }

      return (base.PrepareScript(control) ?? string.Empty).Replace("{{0}}", holderControl.UniqueID);      
    }

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      return "$(this).val($($('[name=\"{{0}}\"]:checked')[0] || $('[name=\"{{0}}\"]')[0]).val())";
    }
  }
}