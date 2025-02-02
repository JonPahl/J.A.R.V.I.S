var SettingsOptions = new SettingsOptions();
var VoiceOption = new VoiceOptions();

AnsiConsole.Write(
    new FigletText("J.A.R.V.I.S")
    .Centered()
    .Color(Color.Red));

AnsiConsole.MarkupLine($"[blue] [bold {Color.Red}]J[/]ust [bold {Color.Red}]A[/] [bold {Color.Red}]R[/]ather [bold {Color.Red}]V[/]ery [bold {Color.Red}]I[/]ntelligent [bold {Color.Red}]S[/]ystem [/]");

#region Setup

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("Settings.json", optional: false, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();

#endregion

#region Read configuration

configuration.GetSection("LLM").Bind(SettingsOptions);
//configuration.GetSection("en_GB-alba-medium").Bind(VoiceOption);

#endregion

#region Voice Options
configuration.GetSection("en_GB-northern_english_male-medium").Bind(VoiceOption);
//configuration.GetSection("en_US-kristin-medium").Bind(VoiceOption);
//configuration.GetSection("en_GB-semaine-mediumMale").Bind(VoiceOption);
//configuration.GetSection("en_GB-semaine-mediumFemale").Bind(VoiceOption);
#endregion

if (SettingsOptions.SpeakResponse)
    await SpeekSetupAsync();

await StartPromptsAsync();

var message = GetClosingMessage();
if (SettingsOptions.SpeakResponse)
{
    await SpeekSetupAsync(message);
    Task.WaitAll();
    Environment.Exit(0);
} else
{
    Environment.Exit(0);
}

async Task SpeekSetupAsync(string text = "Setup complete.")
{
    SpeechBase maleGBVoice = SpeechFactory.GetSpeech(SpeechType.PIPER, VoiceOption);

    await maleGBVoice.SpeakAsync(text);
    Task.WaitAll();
}

async Task StartPromptsAsync()
{
    var chat = new LlmChat(SettingsOptions, VoiceOption);
    await chat.RunAsync();
}

string GetClosingMessage()
{
    var x = new ShutdownMessage();
    var msg = x.GetMessage();
    return msg;
}