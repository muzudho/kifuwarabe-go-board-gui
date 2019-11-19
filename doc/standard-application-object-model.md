# Standard application object model


C# と Rust でデータ構造を共通化させるための仕様だぜ☆（＾～＾） JSONで投げつけてデシリアライズを狙うぜ☆（＾～＾）


# Application object model

## 定数

|name|description|
|b-hama||
|b-name||
|b-time||
|column-size||
|info||
|interval-msec||
|komi||
|move||
|ply||
|row-size||
|w-hama||
|w-name||
|w-time||


## プロパティ

* Board : BoardModel
* Booleans : Dictionary<string, PropertyBool>
* Numbers
* Strings
* StringLists

## メソッド

* Parse
* ToJson

書きかけ
