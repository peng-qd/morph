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
  /// Defines the changing fields to values disable validation class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldsToValuesDisableValidation<T> : ChangingFieldToValueDisableValidation<T> where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the field2.
    /// </summary>
    /// <value>
    /// The field2.
    /// </value>
    public string Trigger2 { get; set; }

    /// <summary>
    /// Gets or sets the value2.
    /// </summary>
    /// <value>
    /// The value2.
    /// </value>
    public string TriggerValue2 { get; set; }

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    /// <value>
    /// The operator.
    /// </value>
    public string Operator { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger2) || string.IsNullOrEmpty(this.Operator))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Called when the fields has loaded.
    /// </summary>
    /// <param name="context">The context.</param>
    protected override void OnFieldsLoaded(Control context)
    {
      Assert.ArgumentNotNull(context, "context");

      var operatorItem = Context.Database.GetItem(this.Operator);
      var trigger2 = this.GetField(context, this.Trigger2) as IResult;     

      if (trigger2 == null || operatorItem == null)
      {
        return;
      }

      var match = new Regex(this.TriggerValue2 ?? string.Empty).Match((trigger2.Result.Value ?? string.Empty).ToString());

      if (match.Success && operatorItem.Name == "and")
      {
        base.OnFieldsLoaded(context);
        return;
      }
      

      if (match.Success)
      {
        foreach (var validator in context.Controls.Flatten().OfType<BaseValidator>())
        {
          validator.Enabled = false;
        }
      }
      else if (operatorItem.Name != "and")
      {
        base.OnFieldsLoaded(context);
      }
    }

    #endregion

  }
}