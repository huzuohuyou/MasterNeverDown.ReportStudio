using WindowsInput;
using WindowsInput.Native;

namespace AakStudio.Shell.UI.Showcase.Views;

public partial class TemplateEditorView : UserControl
{
    public TemplateEditorView(TemplateEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        // grid1.IsEnabled = false;
        Grid.MouseDown += Grid_MouseDown;

    }
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is ReoGridControl grid)
        {
            var cell = grid.CurrentWorksheet.Cells[grid.CurrentWorksheet.FocusPos];
            var text = cell.Data;
            cell.StartEdit();
            cell.Data = cell.Data + " ";
            var sim = new InputSimulator();
             sim.Keyboard.KeyPress(VirtualKeyCode.BACK);
             cell.Data = text;
            // sim.Mouse.RightButtonUp();
            // 发送一个按键事件，例如按下和释放Enter键
            // sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            // sim.Keyboard.KeyPress(VirtualKeyCode.BACK);

        }
    }
    
 
}