var SettingsOptions = new SettingsOptions();
var VoiceOption = new VoiceOptions();

AnsiConsole.Write(
    new FigletText("J.A.R.V.I.S")
        .Centered()
        .Color(Color.Red));

#region Setup

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("Settings.json", optional: false, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();

#endregion

#region Read configuration

configuration.GetSection("LLM").Bind(SettingsOptions);
// configuration.GetSection("VoiceThreeM").Bind(VoiceOption);
configuration.GetSection("VoiceOne").Bind(VoiceOption);

#endregion

await SpeekSetupAsync();
await StartPromptsAsync();

async Task SpeekSetupAsync(string text = "Setup complete.")
{
    ISpeech maleGBVoice = new PiperSpeak(VoiceOption);

    await maleGBVoice.SpeakAsync(text);
    Task.WaitAll();
}

async Task StartPromptsAsync()
{
    var chat = new LlmChat(SettingsOptions, VoiceOption);
    await chat.RunAsync();
}