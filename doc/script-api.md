# Script API


このプログラムは何を書いているのか……という顔をしているお前らのために。


# テキスト・ファイル入出力 という謎のシステム☆（＾～＾）

![screen-shot-1a1.png](./img/screen-shot-1a1.png)  
↑  

1. Visual studio code を使って `./input.txt` ファイルを開けだぜ☆（＾～＾） ファイルをロックせず、ファイルの内容がリアルタイムに反映される優れものだぜ☆（＾～＾）
2. お前は input.txt に何かテキストを打ち込むといいだろう☆（＾～＾）　そして [Ctrl]+[S]キーでも使ってファイルを保存したら　キーボードから手を放せだぜ☆（＾～＾） 触んな☆ｍ９（＾～＾） 
3. `kifuwarabe-uec11-gui.exe` は 2秒間隔ぐらいで `./input.txt` ファイルを ファイルをロックせずに覗き見にくるぜ☆（＾～＾）

![screen-shot-1a2.png](./img/screen-shot-1a2.png)  
↑  

4. `kifuwarabe-uec11-gui.exe` は input.txt ファイルの内容が が空でなければ 消す☆（＾～＾） そのときテキストは複数行コマンド（スクリプト）として読み込むぜ☆（＾～＾）
5. お前は Visual studio code で `input.txt` ファイルの中身が消えたことに顔色を真っ青にしながら　つぎのテキストを打てだぜ☆（＾～＾）
6. `kifuwarabe-uec11-gui.exe` は input.txt を消しもするが `communication.log` にも書き出している☆（＾～＾）　調子乗って長文書いたら１つのファイルが膨れ上がるからな☆ｍ９（＾～＾）


# 行頭 `#` はコメント


Example:


```
#banana
# banana
## banana
 #banana
 # banana
```


# このアプリケーションを すごいすんなり終了させるなら `exit`


Example:

```
exit
exit banana
```


最初の単語で判定している☆（＾～＾） たった４文字の短いキータイピングで このアプリケーションのウィンドウ閉じる（終わる）からな☆（＾～＾） いいのかそれで☆（＾～＾）  


# 画面に　ちょこっと　大して重要でもないメッセージを表示するなら `info`


Example:


```
info 勉強したって無駄だぜ☆（＾～＾）\n 世の中に天才はいっぱいいる割に天才の使い方を知っている凡人は少ない☆（＾～＾）\n　ふてくされてだらだらと人工知能でも作って無意味に過ごそうぜ☆（＾～＾）\n　お前はまずひまつぶしにラムダ計算から入門して数論に進みマルチ・コア・プログラミングを覚えて全ての数学者・物理学者・プログラマーを倒せだぜ☆ｍ９（＾～＾）\n 簡単だろ☆（＾ｑ＾）
```


勝率とか勝敗はこれで描けだぜ☆（＾～＾）  


Escape sequence:  

|Typing|Description|
|---|---|
|`\n`|改行|
|`\\`|`\`|

それ以外のエスケープ文字は全部無効☆（＾～＾）  


# 名前=値 になってるのを設定するのは `set`


Example:


```
set name = value
```


|name|type|description|
|----|----|-----------|
|ply|int|何手目か☆（＾～＾）|
|move|CellAddress|最後の着手☆（＾～＾） `A1` とか入れろだぜ☆（＾～＾）|
|b-name|string|黒番の氏名☆（＾～＾）|
|b-time|string|黒番の持ち時間☆（＾～＾）飾り☆（＾～＾）<br/>とりあえず `23:59` みたいな書式で入れとけだぜ☆（＾～＾）|
|b-hama|int|黒番のアゲハマ☆（＾～＾）取り揚げた白石の数だぜ☆（＾～＾）<br/>日本ルールだけ使う☆（＾～＾）<br/>白石はハマグリで出来てるのが由来でハマといえば石のことだぜ☆（＾～＾）|
|w-name|string|白番の氏名☆（＾～＾）|
|w-time|string|白番の持ち時間☆（＾～＾）|
|w-hama|int|白番のアゲハマ☆（＾～＾）|
|komi|double|コミ☆（＾～＾）<br/>囲碁は先手黒番有理なんで、白にあげるハンディキャップだぜ☆（＾～＾）<br/>昭和初期まで先後２番打ってたが、１番で済ませたくなって<br/>互先（たがいせん）ルールが作られて使われ始めた☆（＾～＾）<br/>コミがいくつかはプロの公式戦の先後の勝率を見て稀に更新されている☆<br/>えっ、話しが長いだって☆（＾～＾）？お前が黙ってろだぜ☆（＾～＾）|
|info|string|GUIの画面上にちょこっと文字を表示する、とって付けたような機能☆（＾～＾）<br/>シンタックス・シュガーの `info message` 構文の方を使えだぜ☆（＾～＾）|
|interval-msec|int|`input.txt` を読みに来る間隔だぜ☆（＾～＾）<br/>ミリ秒で指定できるが、短すぎるとマシンがイカれるぜ☆（＾～＾）|
|row-size|int|行数☆（＾～＾） 0～19☆（＾～＾）|
|column-size|int|列数☆（＾～＾） 0～19☆（＾～＾）|


# 盤に石を置くなら `space`、 `black`、 `white` を使いこなせだぜ☆（＾～＾）


Example:


```
# 1個
black H1
white J10
space T19

# 複数個
black A1 B2 C3 D4

# 矩形塗りつぶし
black A1:C3

# 混合
white A1 B2:D4 E5 F6:H8
```

お絵描きができるな☆（＾～＾）


# ウィジェットを非表示にしたいなら `widget` コマンドを使えだぜ☆（＾～＾）

Example:

```
widget b-hama.visible = false
widget w-hama.visible = false
widget komi vsible = false
```

ウィジェットには以下のようなものがあるぜ☆（＾～＾）

|Name|Description|
|---|---|
|ply|何手目|
|move|最後の着手|
|b-name|先手（黒番）の氏名|
|b-time|黒の持ち時間|
|b-hama|黒のアゲハマ|
|w-name|後手（白番）の氏名|
|w-time|白の持ち時間|
|w-hama|白のアゲハマ|
|komi|白のコミ|


# 連珠盤に変える例

初期状態は　囲碁盤　になっている☆（＾～＾） これを連珠盤にしたかったら こう書き替えろだぜ☆（＾～＾）

Example:

```
set b-name = Kifuwarabe
set w-name = Warabemoti
set row-size = 15
set column-size = 15
widget row-numbers.value = "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
widget column-numbers.value = "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o"
widget b-hama.visible = false
widget w-hama.visible = false
widget komi.visible = false
black h8
white h9
black i10
```

