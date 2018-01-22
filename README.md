# Kup-Translator
This tool enables you to open the KUP file from Kuriimu, translates the "OriginalText" and sets the "EditedText" to the target language.

# Pre-Requisites
- [x] Windows
- [x] .NET Framework 4.5.2
- [x] .NET C# Language Version 7.1+

# How does it work
Execute the executable with parameters and it will do the work for you.
If you use a wikia value, you can cross reference the japanese words (names e.g.) with those fanpages.

If the cross reference is not successful, the application will try to translate it to the given language paramwter: romaji or english.
If you want to use the english translation, it will Google Translate. This process is far slower than the romaji procedure. So beware.

## Parameters
/file => Setting the target filename. Example: /file:"C:\temp\XYZ.kup" **Required**

/wikia => Set the wikia cross reference. Example /wikia:"inazuma-eleven" **Optional**

/lng => Setting the target language if wikia is not given or cross reference is not successful. Example: /lng:ro (Romaji) or /lng:en (English) **Required**

/from => In each KUP files, there are names in this format "textXYZ". You can set the number from which the tool shall start the translation. Example: /from:2000 **Optional**

/to => In each KUP files, there are names in this format "textXYZ". You can set the number until which the tool shall do the translation. Example: /to:2100 **Optional**

Full example: KupTranslator.Simple.exe /file:"C:\temp\test.kup" /wikia:"inazuma-eleven" /lng:ro /from:1200 /to:1250

## Log Files
A Log file is being created in the current directory, log directory, where application is executed.
Format: yyyy-MM-dd_hhmmss.log

# Links
Kuriimu -> https://github.com/IcySon55/Kuriimu

Kakasi.NET Wrapper -> https://github.com/linguanostra/Kakasi.NET
