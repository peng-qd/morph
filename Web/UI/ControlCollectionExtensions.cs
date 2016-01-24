namespace Morph.Forms.Web.UI
{
  using System.Collections.Generic;
  using System.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Defines the control collection extensions class.
  /// </summary>
  public static class ControlCollectionExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// Flattens the specified controls.
    /// </summary>
    /// <param name="controls">The controls.</param>
    /// <returns>
    /// The IEnumerable.
    /// </returns>
    public static IEnumerable<Control> Flatten(this ControlCollection controls)
    {
      Assert.ArgumentNotNull(controls, "controls");

      foreach (Control child in controls)
      {
        if (child != null)
        {
          yield return child;

          foreach (var childOfType in child.Controls.Flatten())
          {
            yield return childOfType;
          }
        }
      }
    }

    #endregion
  }
}