using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
  /// <summary>
  /// Dock window of Visual Studio 2012 Light theme.
  /// </summary>
  [ToolboxItem(false)]
  internal class VS2012LightDockWindow : DockWindow
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="VS2012LightDockWindow"/> class.
    /// </summary>
    /// <param name="dockPanel">The dock panel.</param>
    /// <param name="dockState">State of the dock.</param>
    public VS2012LightDockWindow(DockPanel dockPanel, DockState dockState)
      : base(dockPanel, dockState)
    {
    }

    public override Rectangle DisplayingRectangle
    {
      get
      {
        Rectangle rect = ClientRectangle;
        if (DockState == DockState.DockLeft)
          rect.Width -= Measures.SplitterSize;
        else if (DockState == DockState.DockRight)
        {
          rect.X += Measures.SplitterSize;
          rect.Width -= Measures.SplitterSize;
        }
        else if (DockState == DockState.DockTop)
          rect.Height -= Measures.SplitterSize;
        else if (DockState == DockState.DockBottom)
        {
          rect.Y += Measures.SplitterSize;
          rect.Height -= Measures.SplitterSize;
        }

        return rect;
      }
    }

    internal class VS2012LightDockWindowSplitterControl : SplitterBase
    {
      private static readonly SolidBrush _horizontalBrush = new SolidBrush(Color.FromArgb(0xFF, 204, 206, 219));
      private static readonly SolidBrush _verticalBrush = new SolidBrush(SystemColors.Control);

      protected override int SplitterSize
      {
        get
        {
          return Measures.SplitterSize;
        }
      }

      protected override void StartDrag()
      {
        DockWindow window = Parent as DockWindow;
        if (window == null)
          return;

        window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds));
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        base.OnPaint(e);

        Rectangle rect = ClientRectangle;

        if (rect.Width <= 0 || rect.Height <= 0)
          return;

        switch (Dock)
        {
          case DockStyle.Right:
          case DockStyle.Left:
              e.Graphics.FillRectangle(_verticalBrush, rect.X, rect.Y, rect.Width, Measures.SplitterSize);

              using (Pen borderPen = new Pen(Color.FromArgb(0xFF, 204, 206, 219)))
              {
                e.Graphics.DrawLine(borderPen, rect.X, 0, rect.X, rect.Height - 1);
                e.Graphics.DrawLine(borderPen, rect.Width - 1, 0, rect.Width - 1, rect.Height - 1);
              }
            break;
          case DockStyle.Bottom:
          case DockStyle.Top:
              e.Graphics.FillRectangle(_horizontalBrush, rect.X, rect.Y, rect.Width, Measures.SplitterSize);
            break;
        }

      }
    }
  }
}