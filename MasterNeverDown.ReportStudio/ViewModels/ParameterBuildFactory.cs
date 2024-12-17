

using CheckBox = System.Windows.Controls.CheckBox;
using RadioButton = System.Windows.Controls.RadioButton;
using TextBox = System.Windows.Controls.TextBox;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public class ParameterBuildFactory
{
    private static readonly Lazy<ParameterBuildFactory> Instance = new(() => new ParameterBuildFactory());

    private readonly DataSourceContext? _dataSourceContext =
        (App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext)!;

    public static ParameterBuildFactory Factory => Instance.Value;

    private ParameterBuildFactory()
    {
    }

    public UIElement Build(Parameter p)
    {
        switch (p.InputType)
        {
            case InputType.TextBox:
                return new TextBox();
            case InputType.ComboBox:
            {
                var comboBox = new ComboBox
                {
                    Width = 150,
                    SelectedValuePath = "value",
                    DisplayMemberPath = "key",
                    Name = p.Name
                };

                //todo: get data from db
                var conn = _dataSourceContext!.Connections.FirstOrDefault(c => c.Name.Equals(p.Connection));

                if (conn == null) return comboBox;
                var connectionString = GetConnectionString(conn.Host, conn.User, conn.Password,
                    conn.Database,
                    conn.Port.ToString());
                using IDbConnection db = new NpgsqlConnection(connectionString);
                var result = db.Query(p.Script).ToList();
                comboBox.ItemsSource = result;

                return comboBox;
            }
            case InputType.CheckBox:
                return new CheckBox();
            case InputType.RadioButton:
                return new RadioButton();
            case InputType.MultiSelectComboBox:
                return new ComboBox { IsEditable = true, IsTextSearchEnabled = true };
            case InputType.Date:
                return new DatePicker();
            // case InputType.DateRange:
            //     // Custom control for date range
            //     return new DateRangePicker();
            case InputType.DateTime:
                return new DatePicker { SelectedDateFormat = DatePickerFormat.Long };
            // case InputType.DateTimeRange:
            //     // Custom control for date-time range
            //     return new DateTimeRangePicker();
            case InputType.Year:
                return new ComboBox { ItemsSource = GetYears() };
            case InputType.Month:
                return new ComboBox { ItemsSource = GetMonths() };
            case InputType.Day:
                return new ComboBox { ItemsSource = GetDays() };
            case InputType.Int:
                return new TextBox
                    { InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.Number) } } };
            case InputType.Float:
                return new TextBox
                {
                    InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.NumberFullWidth) } }
                };
            case InputType.DateRange:
                break;
            case InputType.DateTimeRange:
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(p.InputType), p.InputType, null);
        } return new TextBox();
    }

    private string GetConnectionString(string servername, string uid, string pwd, string db, string port)
    {
        return $"host={servername};User ID={uid};password={pwd};database={db};port={port};pooling=false;";
    }

    private static int[] GetYears()
    {
        var currentYear = DateTime.Now.Year;
        var years = new int[100];
        for (var i = 0; i < 100; i++)
        {
            years[i] = currentYear - i;
        }
        return years;
    }

    private static string[] GetMonths()
    {
        return new[]
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
            "November", "December"
        };
    }

    private static int[] GetDays()
    {
        var days = new int[31];
        for (var i = 0; i < 31; i++)
        {
            days[i] = i + 1;
        }

        return days;
    }
}