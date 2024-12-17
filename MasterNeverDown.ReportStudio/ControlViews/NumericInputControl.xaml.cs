namespace AakStudio.Shell.UI.Showcase.ControlViews;

public partial class NumericInputControl : UserControl
{
    public NumericInputControl()
    {
        InitializeComponent();
    }
    
    public ReoGridControl Grid
    {
        get => (ReoGridControl)GetValue(GridProperty);
        set => SetValue(GridProperty, value);
    }
    
    public static readonly DependencyProperty GridProperty =
        DependencyProperty.Register("Grid", typeof(ReoGridControl), typeof(NumericInputControl), new PropertyMetadata(null));


    public int NumericValue
    {
        get => (int)GetValue(NumericValueProperty);
        set => SetValue(NumericValueProperty, value);
    }
    public static readonly DependencyProperty NumericValueProperty =
        DependencyProperty.Register("NumericValue", typeof(int), typeof(NumericInputControl), new PropertyMetadata(0));

    public Visibility TitleVisibility
    {
        get => (Visibility)GetValue(TitleVisibilityProperty);
        set => SetValue(TitleVisibilityProperty, value);
    }
    public static readonly DependencyProperty TitleVisibilityProperty =
        DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(NumericInputControl), new PropertyMetadata(Visibility.Collapsed));

        
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(NumericInputControl), new PropertyMetadata("Title"));

       
        
    private void IncrementButton_Click(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(IncrementValueEvent));
        NumericValue++;
    }

    private void DecrementButton_Click(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(DecrementValueEvent));
        NumericValue--;
    }
    
    public static readonly RoutedEvent IncrementValueEvent = EventManager.RegisterRoutedEvent(
        "IncrementValue", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericInputControl));

    public event RoutedEventHandler IncrementValue
    {
        add { AddHandler(IncrementValueEvent, value); }
        remove { RemoveHandler(IncrementValueEvent, value); }
    }
    
    public static readonly RoutedEvent DecrementValueEvent = EventManager.RegisterRoutedEvent(
        "DecrementValue", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericInputControl));

    public event RoutedEventHandler DecrementValue
    {
        add { AddHandler(DecrementValueEvent, value); }
        remove { RemoveHandler(DecrementValueEvent, value); }
    }

    protected virtual void OnMyCustomEvent(RoutedEventArgs e)
    {
        RaiseEvent(e);
    }
}