# Standard application dto(data transfer object)


C# と Rust でデータ構造を共通化させるための仕様だぜ☆（＾～＾） JSONで投げつけてデシリアライズを狙うぜ☆（＾～＾）


# Application dto

## Property type

| name        | description            |
| ----------- | ---------------------- |
| null        | Use to erase.          |
| bool        | true or false.         |
| number      | double type.           |
| string      | string.                |
| string-list | Example: "a", "b", "c" |

## Embeded UI name

| name                | description |
| ------------------- | ----------- |
| line-column-numbers |             |
| column-size         |             |
| info                |             |
| interval-msec       |             |
| left1               |             |
| left2               |             |
| left3               |             |
| left4               |             |
| moveMarker          | WIP.        |
| right1              |             |
| right2              |             |
| right3              |             |
| line-row-numbers    |             |
| row-size            |             |
| stars               |             |
| top1                |             |
| top2                |             |


## メソッド

* Parse
* ToJson


# Board model

## プロパティ

| name       | type        |
| ---------- | ----------- |
| RowSize    | int         |
| ColumnSize | int         |
| Stones     | List<Stone> |


# Color

石の指定は、色名で行います。 `black` や `yellow-green`、 `#FFFF0000` といった書き方が可能です。  
色名は .NET Core が勝手に判定します。


# Property

## プロパティ

| name    | type  |
| ------- | ----- |
| Title   | title |
| Value   | bool  |
| Visible | bool  |

## メソッド

* ValueAsText()

string-list 型のみ、`","` で Join して `"` で挟む。エスケープはまだやってない。  
`$"\"{string.Join("\",\"", this.Value)}\""`  


書きかけ
