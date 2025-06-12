using Spectre.Console;

namespace ModelRouterSample.Utils;

internal static class ConsoleHelper
{
    /// <summary>
    ///     Clears the console and displays the application header.
    /// </summary>
    public static void ShowHeader()
    {
        AnsiConsole.Clear();

        var grid = new Grid();
        grid.AddColumn();
        grid.AddRow(
            new FigletText("Model Router")
                .Centered()
                .Color(Color.Red));
        grid.AddRow(
            Align.Center(
                new Panel("[red]Sample by Thomas Sebastian Jensen " +
                "([link]https://www.tsjdev-apps.de[/])[/]")));

        AnsiConsole.Write(grid);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    ///     Prompts the user for a string input with basic validation.
    /// </summary>
    /// <param name="message">Prompt message to display.</param>
    /// <param name="clearScreen">Whether to clear the console before prompting.</param>
    /// <returns>The user input string.</returns>
    public static string GetString(
        string message,
        bool clearScreen = true)
    {
        if (clearScreen)
        {
            ShowHeader();
        }

        return AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .PromptStyle("white")
                .ValidationErrorMessage("[red]Invalid input[/]")
                .Validate(ValidateShortText));
    }

    /// <summary>
    /// Prompts the user for a valid URL input.
    /// </summary>
    /// <param name="message">Prompt message to display.</param>
    /// <param name="clearScreen">Whether to clear the console before prompting.</param>
    /// <returns>The validated URL string.</returns>
    public static string GetUrl(
        string message,
        bool clearScreen = true)
    {
        if (clearScreen)
        {
            ShowHeader();
        }

        return AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .PromptStyle("white")
                .ValidationErrorMessage("[red]Invalid URL[/]")
                .Validate(ValidateUrl));
    }

    /// <summary>
    /// Writes response information in a table format.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="inputTokens">Number of input tokens.</param>
    /// <param name="outputTokens">Number of output tokens.</param>
    public static void WriteResponseInformation(
        string model,
        int inputTokens,
        int outputTokens)
    {
        var table = new Table()
            .AddColumn("Model")
            .AddColumn("Input Tokens")
            .AddColumn("Output Tokens")
            .AddRow(model, inputTokens.ToString(), outputTokens.ToString());

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Writes formatted markup text to the console.
    /// </summary>
    /// <param name="text">Text to display, using Spectre.Console markup.</param>
    public static void WriteToConsole(
        string text)
    {
        AnsiConsole.Markup(text);
        AnsiConsole.WriteLine();
    }

    // --- Private Validators ---

    private static ValidationResult ValidateShortText(
        string input)
    {
        if (input.Length < 3)
        {
            return ValidationResult.Error("[red]Value too short[/]");
        }

        return ValidationResult.Success();
    }

    private static ValidationResult ValidateUrl(
        string input)
    {
        if (input.Length < 3)
        {
            return ValidationResult.Error("[red]Value too short[/]");
        }

        if (input.Length > 200)
        {
            return ValidationResult.Error("[red]Value too long[/]");
        }

        return Uri.TryCreate(input, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]Value is not a valid URL[/]");
    }
}
