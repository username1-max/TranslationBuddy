# TranslationBuddy

This tool uses AI driven components to translate speech to another language. The tool is currently only supported in a Windows environment (this has currently only been tested with Windows 11).

The current implementation uses:
* Transcriber: The [Vosk](https://alphacephei.com/vosk/) speech recongition models
* Translation: [Gpt4o-mini](https://openai.com/index/gpt-4o-mini-advancing-cost-efficient-intelligence/) model from Open AI with a simple prompt constructed [here](TranslationBuddy/TranslationBuddy/TranslationBuddy.cs). The calls are made using the [chat completions](https://platform.openai.com/docs/guides/text-generation) API.
* Speech synthesis: [System.Speech](https://www.nuget.org/packages/System.Speech/) library which uses the [Microsoft Speech API](https://learn.microsoft.com/en-ca/previous-versions/windows/desktop/ms723627(v=vs.85)) under the hood.

## Demo
[Demo](https://youtube.com/shorts/Z63Zr6SoqJY)

## Setup Guide
* Clone the repo to your machine
* Download Vosk models from [this link](https://alphacephei.com/vosk/models). For the purposes of the demo, I'm using the `vosk-model-en-us-0.22` for English and `vosk-model-es-0.42` for Spanish. Both of these are licensed under the `Apache 2.0` license.
* Configure the voices for the language of choice. You can learn more about how to add voices here. If you use the `System.Speech` library as I did, please follow the guidance [here](#adding-voices-in-windows-speech-api)
* Update the corresponding model path configuration and the name of the Speech API narrator in [this](AIClientLib/AIClientLib/Client/Voice/Models/Vosk/VoiceModelProvider.cs) file (the latter is determined based on the name attribute given in the registry). More details can be found [here](#adding-voices-in-windows-speech-api).
* You will also need to get an Open AI API key and an org id. Once you get both, you will need to update [this](AIClientLib/AIClientLib/Client/OpenAiClient.cs) config file.

# Adding Voices in Windows Speech AP
Adding voices uses the Windows speech API is not very easy.

* The first thing you will need to do is add additional voices. To do this, please first check that the language of interested actually has supported voices based on your operating system [here](https://support.microsoft.com/en-us/windows/appendix-a-supported-languages-and-voices-4486e345-7730-53da-fcfe-55cc64300f01#WindowsVersion=Windows_11).
* If you find there is text-to-speech support for the language of interest, you will then need to download the language pack (specifically text to speech for the language of choice). To do this, open the start menu and search for **Language settings**. From here select, **Add a language** and then select the language of interest. Download the text to speech pack for that language which should add the corresponding voices.

Once the language package has been downloaded, you then need to update the registry. The answer in this [Stack Overflow](https://stackoverflow.com/questions/51811901/speechsynthesizer-doesnt-get-all-installed-voices-3) post guides you on what needs to be done but in a netshell you need to copy the new language registry entry including its attributes and child keys from `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech_OneCore\Voices\Tokens` to `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens` so it becomes available. The naming scheme here would have both the name of the language and the narrator which you can again refer back to [here](https://support.microsoft.com/en-us/windows/appendix-a-supported-languages-and-voices-4486e345-7730-53da-fcfe-55cc64300f01#WindowsVersion=Windows_11) to identify which narrator to add. Many options are available here including updating the registry manually, using a script, etc. This is not something I have automated yet so you'll need to do this yourself for now.
