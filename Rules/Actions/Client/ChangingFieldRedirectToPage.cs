namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Data.Items;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Links;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Defines the changing field redirect to page class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldRedirectToPage<T> : ChangingFieldRunClientScriptForField<T> where T : ConditionalRuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the item id.
    /// </summary>
    /// <value>
    /// The item id.
    /// </value>
    public string ItemId { get; set; }

    #endregion

    #region Protected methods

    /// <summary>
    /// Gets the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      if (string.IsNullOrEmpty(this.ItemId) || Context.Database == null)
      {
        return string.Empty;
      }

      Item item = Context.Database.GetItem(this.ItemId);
      if (item == null)
      {
        return string.Empty;
      }

      return "window.location = '{0}'".FormatWith(LinkManager.GetItemUrl(item));
    }

    #endregion
  }
}