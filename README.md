# Kup-Translator
This tool enables you to open the KUP file from Kuriimu, translates the "OriginalText" and sets the "EditedText" to the target language.

# Pre-Requisites
- [x] Windows
- [x] .NET Framework 4.5.2
- [x] .NET C# Language Version 7.1+

# Kup-Translator.Simple
## How does it work
Execute the executable with parameters and it will do the work for you.
If you use a wikia value, you can cross reference the japanese words (names e.g.) with those fanpages.

If the cross reference is not successful, the application will try to translate it to the given language parameter: romaji or english.
If you want to use the english translation, it will Google Translate. This process is far slower than the romaji procedure. So beware.

## Parameters
/file => Setting the target filename. Example: /file:"C:\temp\XYZ.kup" **Required**

/wikia => Set the wikia cross reference. Example /wikia:"inazuma-eleven" **Optional**

/lng => Setting the target language if wikia is not given or cross reference is not successful. Example: /lng:ro (Romaji) or /lng:en (English) **Required**

/from => In each KUP files, there are names in this format "textXYZ". You can set the number from which the tool shall start the translation. Example: /from:2000 **Optional**

/to => In each KUP files, there are names in this format "textXYZ". You can set the number until which the tool shall do the translation. Example: /to:2100 **Optional**

Full example: KupTranslator.Simple.exe /file:"C:\temp\test.kup" /wikia:"inazuma-eleven" /lng:ro /from:1200 /to:1250

# Kup-Translator.Exchanger
## How does it work
The Exchanger uses the same cross reference option to evaluate given translated names, e.g. dub names, and creates a reference name to its origin. For example the origin name "Endou Mamoru" is the dub name "Mark Evans". That way, you can translate names easily, if the wikia has listed the characters.

It will create in the process a .csv-file in the output directory of the application.
The file has the following column structure
Kup-Entry	English Name	Length	Original Name	Length	Check Charlength
- [x] Kup-Entry => Name of the kupEntry like text0001
- [x] English Name => Original dub name (in english)
- [x] Length => Count of chars of english name
- [x] Original Name => Original japanese name in romaji
- [x] Length => Count of chars of original name
- [x] Check => True: You should check the char lengths of both names, False: Original Name is not longer than English Name (Byte Length ...)

## Parameters
/sf => Setting the source .kup file. Example /sf:"C:\temp\test.kup" **Required**

/wikia => Set the wikia cross reference. Example /wikia:"inazuma-eleven" **Required**

/from => In each KUP files, there are names in this format "textXYZ". You can set the number from which the tool shall start the translation. Example: /from:2000 **Optional**

/to => In each KUP files, there are names in this format "textXYZ". You can set the number until which the tool shall do the translation. Example: /to:2100 **Optional**

/rc => Recursive check. If you have already translated a batch of fullnames in a kup-file, there is possibly a nickname version which you can link to that translation. It will recursively check if a similar entry exists and puts the original name in. Example: /rc:true **Optional**

/mbl => You can enable "Match ByteLength". That way, the original names will be auto-cut to match the length of their english names. For example "Matsukaze Tenma" is too long for, so it should fit the respective bytelength, than it will cut it to "Tenma". Example: /mbl:true **Optional**

## Log Files
A Log file is being created in the current directory, log directory, where application is executed.
Format: yyyy-MM-dd_hhmmss.log

# Links
Kuriimu -> https://github.com/IcySon55/Kuriimu

Kakasi.NET Wrapper -> https://github.com/linguanostra/Kakasi.NET

WikiaCSharpWrapper -> https://github.com/Insighter2k/WikiaCSharpWrapper

If you have any questions, feedback or anything else, tell me.
This version is mostly done for inazuma eleven games. It might not work for your game, but i can MAYBE make it work. Just ask.
