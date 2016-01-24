namespace Morph.Forms.Rules.Actions.Client
{
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field to value disable validation class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldToValueDisableValidation<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    public string Trigger { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string TriggerValue { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control == null || string.IsNullOrEmpty(this.Trigger))
      {
        return;
      }

      ruleContext.Control.Load += (s, e) => this.OnFieldsLoaded(ruleContext.Control);
    }

    /// <summary>
    /// Called when the field has loaded.
    /// </summary>
    /// <param name="context">The control.</param>
    protected virtual void OnFieldsLoaded(Control context)
    {
      Assert.ArgumentNotNull(context, "context");

      var trigger = this.GetField(context, this.Trigger) as IResult;

      if (trigger == null)
      {
        return;
      }

      var match = new Regex(this.TriggerValue ?? string.Empty).Match((trigger.Result.Value ?? string.Empty).ToString());
      if (match.Success)
      {
        foreach (var validator in context.Controls.Flatten().OfType<BaseValidator>())
        {
          validator.Enabled = false;
        }
      }
    }

    #endregion
  }
}