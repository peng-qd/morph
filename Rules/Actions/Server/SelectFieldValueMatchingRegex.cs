namespace Morph.Forms.Rules.Actions.Server
{
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;
  using SysListControl = System.Web.UI.WebControls.ListControl;

  /// <summary>
  /// Defines the select field value matching regex class.
  /// </summary>
  public class SelectFieldValueMatchingRegex<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    public string FieldId { get; set; }

    /// <summary>
    /// Gets or sets the regex.
    /// </summary>
    /// <value>
    /// The regex.
    /// </value>
    public string Pattern { get; set; }

    #endregion

    #region Public methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control == null || 
          string.IsNullOrEmpty(this.FieldId) || 
          string.IsNullOrEmpty(this.Pattern))
      {   
        return;
      }

      ruleContext.Control.Load += (s, e) => this.CopyMatchingValue(ruleContext.Control);
    }

    /// <summary>
    /// Copies the matching value.
    /// </summary>
    /// <param name="control">The control.</param>
    protected virtual void CopyMatchingValue(Control control)
    {
      Assert.ArgumentNotNull(control, "control");

      var holder = (IResult)this.GetField(control, this.FieldId);
      var target = (IResult)control;

      if (holder == null)
      {
        return;
      }

      var match = Regex.Match(holder.Result.Value.ToString(), this.Pattern);
      string value = match.Success ? match.Value.Trim() : string.Empty;

      target.DefaultValue = ConvertToFieldSpecificValue(target, value);
    }

    /// <summary>
    /// Converts to field specific value.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    protected virtual string ConvertToFieldSpecificValue(IResult target, string value)
    {
      var field = target as ListControl;

      if (field != null)
      {
        var item = field.Items.FindByValue(value) ?? field.Items.FindByText(value);

        if (item != null)
        {
          var list = field.Controls.Flatten().OfType<SysListControl>().FirstOrDefault();
          if (list != null)
          {
            list.ClearSelection();
            list.SelectedValue = item.Value;
          }

          return item.Value;
        }
      }

      return value;
    }

    #endregion
  }
}