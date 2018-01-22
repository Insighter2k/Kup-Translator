# Kup-Translator
This tool enables you to open the KUP file from Kuriimu, translates the "OriginalText" and sets the "EditedText" to the target language.

# Pre-Requisites
- [x] Windows
- [x] .NET Framework 4.5.2
- [x] .NET C# Language Version 7.1+

# How does it work
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

## Log Files
A Log file is being created in the current directory, log directory, where application is executed.
Format: yyyy-MM-dd_hhmmss.log

# Links
Kuriimu -> https://github.com/IcySon55/Kuriimu

Kakasi.NET Wrapper -> https://github.com/linguanostra/Kakasi.NET

WikiaCSharpWrapper -> https://github.com/Insighter2k/WikiaCSharpWrapper

# Example (Log)
This is a log, taken from Inazuma Eleven Galaxy for their player names
018-01-22_07:49:41 - Filepath: D:\temp\chara_text_ja.kup 
2018-01-22_07:49:41 - Wikia: inazuma-eleven
2018-01-22_07:49:41 - Language: ro
2018-01-22_07:49:41 - Romaji settings:kakasi -C -rhepburn -Ha -ka -Ka -Ea -Ja -s
2018-01-22_07:49:42 - Loading KUP file
2018-01-22_07:49:42 - From Count: 3000
2018-01-22_07:49:42 - To Count: 6099
2018-01-22_07:49:42 - Begin translation
...
2018-01-22_07:49:46 - [俄然/がぜん]　[則丸/のりまる] => Gazen Norimaru
2018-01-22_07:49:46 - [北上/きたがみ]　[総吉/そうきち] => Kitagami Soukichi
2018-01-22_07:49:46 - [足黒/あしぐろ]　[芳一朗/ほういちろう] => Ashiguro Houichirou
2018-01-22_07:49:46 - [矢部/やべ]　[半兵衛/はんべえ] => Yabe Hanbe
2018-01-22_07:49:46 - [加納/かのう]　[一活/いっかつ] => [ Kanou / kanou ] [ Ichi Katsu / ikkatsu ]
2018-01-22_07:49:46 - [浅間/あさま]　[伝兵衛/でんべい] => Asama Denbei
2018-01-22_07:49:46 - [山鉄/さんてつ]　[銀二/ぎんじ] => Santetsu Ginji
2018-01-22_07:49:46 - [落村/おちむら]　[伝来/でんらい] => Ochimura Denrai
2018-01-22_07:49:46 - [工党/くとう]　[千児/せんじ] => Kutou Senji
2018-01-22_07:49:46 - [稲葉/いなば]　[大安/だいあん] => Inaba Taian
2018-01-22_07:49:47 - [前林/まえばやし]　[香林/こうりん] => Maebayashi Kourin
2018-01-22_07:49:47 - [氏木/うじき]　[神酒丸/みきまる] => Ujiki Mikimaru
2018-01-22_07:49:47 - [四方山/よもやま]　[胴心/どうしん] => Yomoyama Doushin
...
2018-01-22_07:52:21 - Translating now (the missing entries) to ro
2018-01-22_07:52:22 - ジャンヌ・ダルク => jannu . daruku
2018-01-22_07:52:22 - ラ・イール => ra . i^ru
2018-01-22_07:52:22 - ジル・ド・レ => jiru . do . re
2018-01-22_07:52:22 - [諸葛/しょかつ]　[亮/りょう] => [ Shokatsu / shokatsu ] [ Akira / ryou ]
2018-01-22_07:52:22 - ＳＲ－１０１Ｘ => SR-101X
2018-01-22_07:52:22 - ＳＲ－１０２Ｘ => SR-102X
2018-01-22_07:52:22 - ＳＲ－１０３Ｘ => SR-103X
2018-01-22_07:52:22 - ＳＲ－１０４Ｘ => SR-104X
2018-01-22_07:52:22 - ＳＲ－１０５Ｘ => SR-105X
2018-01-22_07:52:22 - ＳＲ－１０６Ｘ => SR-106X
2018-01-22_07:52:22 - ＳＲ－１０７Ｘ => SR-107X
2018-01-22_07:52:22 - ＳＲ－１０８Ｘ => SR-108X
2018-01-22_07:52:22 - ＳＲ－１０９Ｘ => SR-109X
2018-01-22_07:52:22 - ＳＲ－１１０Ｘ => SR-110X
2018-01-22_07:52:22 - ＳＲ－１１１Ｘ => SR-111X
2018-01-22_07:52:22 - [加納/かのう]　[一活/いっかつ] => [ Kanou / kanou ] [ Ichi Katsu / ikkatsu ]
2018-01-22_07:52:22 - [夜須/やず]　[天慧/てんけい] => [ Yoru Su / yazu ] [ Ten Kei / tenkei ]
2018-01-22_07:52:22 - [早矢馬/はやま]　[鏑郎/かぶろう] => [ Sou Ya Uma / hayama ] [ Kabura Rou / kaburou ]
2018-01-22_07:52:22 - ザナーク・アバロニク => zana^ku . abaroniku
2018-01-22_07:52:22 - ラウ・セム => rau . semu
2018-01-22_07:52:22 - バド・アッド => bado . addo
2018-01-22_07:52:22 - リン・クール => rin . ku^ru
2018-01-22_07:52:22 - Saving KUP file
