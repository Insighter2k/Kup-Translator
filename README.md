# Simple-Kup-Translator
This tool enables you to open the KUP file from Kuriimu, translates the "OriginalText" and sets the "EditedText" to the target language.

# Pre-Requisites
- [x] Windows
- [x] .NET Framework 4.5.2
- [x] .NET C# Language Version 7.1+

# How does it work
Execute the executable with parameters and you it will do the work for you.

## Parameters
-file => Setting the target filename. Example: -file:"C:\temp\XYZ.kup" **Required**

-lng => Setting the target language. Example: -lng:ro (Romaji) or -lng:en (English) **Required**

-from => In each KUP files, there are names in this format "textXYZ". You can set the number from which the tool shall start the translation. Example: -from:2000 **Optional**

-to => In each KUP files, there are names in this format "textXYZ". You can set the number until which the tool shall do the translation. Example: -to:2100 **Optional**

Full example: SimpleKupTranslator.exe -file:"C:\temp\test.kup" -lng:ro -from:1200 -to:1250

# Links
Kuriimu -> https://github.com/IcySon55/Kuriimu
Kasaki Wrapper -> https://github.com/linguanostra/Kakasi.NET
