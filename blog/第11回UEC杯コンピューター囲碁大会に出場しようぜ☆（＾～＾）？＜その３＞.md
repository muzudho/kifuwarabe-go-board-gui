![20191110wpf47.png](https://crieit.now.sh/upload_images/e2a0885fe0a773d3a3cbdbddb61e91b75dc6ef3bb9678.png)
(2019-11-10 2:00頃 [Git hub](https://github.com/muzudho/kifuwarabe-uec11-gui))

# 2019-11-06 19:30頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　GUIをもう少し作り込みたい☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　できあいのものでいいのに……☆」

![20191106wpf40.png](https://crieit.now.sh/upload_images/99f4fb7d5d2183ceeafe01c161993ff75dc2a31ca7fd8.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　連珠では　アゲハマ　とか使わないだろ☆
欄の表示、非表示は　どういうコマンドにしよかな……☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　コンピューター囲碁をベースにするの？
何もない床をベースにするの？」

```
set w-hama.visibility = visible
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑こんな感じにするのが自然かだぜ☆？」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　一貫性がなくなるだろ☆」

```
set w-hama.value = 14
```

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　↑例えば　こう書くなら一貫性があるが、打鍵が　めんどくさい☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　こんな　くそＧＵＩ　の仕様に　そんなにガチにならんでも……☆」

```
widget w-hama.visible = true
```

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　↑ウィジェット用のコマンドを別に用意したら？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　それは　ありだな☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　プロパティって　どんなものが要るんだぜ☆？」

|Name|Description|
|---|---|
|value|`set b-name = kifuwarabe` みたいな働き。|
|visible|`true`: 表示。 `false`: 非表示。|

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　↑`visible` だけでいい気がするぜ☆　ガチにやりだしたら結局 CSS が欲しくなるだろ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ウィジェットは　どんなものがあるんだぜ☆？」

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

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　↑囲碁ベースなら　こうだな☆」

# 2019-11-06 20:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　飯☆」

# 2019-11-06 21:00頃

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　ちゃっちゃと `widget` 構文を実装しましょう！」

MainController.cs

```
    case "widget":
        {
            var args = (WidgetInstructionArgument)instruction.Argument;
            switch (args.Name)
            {
                case "ply":
                    switch (args.Property)
                    {
                        case "visible":
                            switch (args.Property)
                            {
                                case "true":
                                    view.plyCanvas.Visibility = Visibility.Visible;
                                    break;
                                case "false":
                                    view.plyCanvas.Visibility = Visibility.Hidden;
                                    break;
                            }
                            break;
                    }

                    break;
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑雰囲気的には　こういうのを書いていけばいいんだろ☆　一般化できんかなあ……☆」

MainController.cs

```
        private static void ChangeCanvasProperty(Canvas canvas, WidgetInstructionArgument args)
        {
            switch (args.Property)
            {
                case "visible":
                    switch (args.Value)
                    {
                        case "true":
                            canvas.Visibility = Visibility.Visible;
                            break;
                        case "false":
                            canvas.Visibility = Visibility.Hidden;
                            break;
                    }
                    break;
            }
        }
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑Canvas をいじる部分を　関数に切りだして……☆」

MainController.cs

```
                        case "widget":
                            {
                                var args = (WidgetInstructionArgument)instruction.Argument;
                                switch (args.Name)
                                {
                                    case "ply":
                                        ChangeCanvasProperty(view.plyCanvas, args);
                                        break;

                                    case "move":
                                        ChangeCanvasProperty(view.lastMoveCanvas, args);
                                        break;

                                    case "b-name":
                                        ChangeCanvasProperty(view.blackNameCanvas, args);
                                        break;

                                    case "b-time":
                                        ChangeCanvasProperty(view.blackTimeCanvas, args);
                                        break;

                                    case "b-hama":
                                        ChangeCanvasProperty(view.blackAgehamaCanvas, args);
                                        break;

                                    case "w-name":
                                        ChangeCanvasProperty(view.whiteNameCanvas, args);
                                        break;

                                    case "w-time":
                                        ChangeCanvasProperty(view.whiteTimeCanvas, args);
                                        break;

                                    case "w-hama":
                                        ChangeCanvasProperty(view.whiteAgehamaCanvas, args);
                                        break;

                                    case "komi":
                                        ChangeCanvasProperty(view.komiCanvas, args);
                                        break;
                                }
                            }
                            break;
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑一網打尽だぜ☆」

![20191106wpf41.png](https://crieit.now.sh/upload_images/50d9e4fbb04f44230bf4c0aef73fa3385dc2c0b8ebca9.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑とりあえず　簡素ながら　アゲハマと　コミを非表示にできるようにしたぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　ドキュメントに説明を書き足せだぜ☆」

![20191106wpf42.png](https://crieit.now.sh/upload_images/54aa987a4e3c0932d0c3422e727a98185dc2c24c5a9c7.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑これで　連珠盤　になっただろ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　`I列`　が無いぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　はぁ～あ☆（／＿＼）」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　列番号のウィジェットなんて　どうすればいいんだぜ☆？」

```
widget column-numbers.value = A,B,C,D,E,F,G,H,J,K,L,M,N,O,P
```

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　↑こうじゃないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　はぁ～あ☆（／＿＼）なんで国際囲碁は　特別仕様なんだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　カンマ区切りにするのか☆？　他のは　スペース区切りなのに☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　それぐらい　特別仕様　として認めるかだぜ☆」

# 2019-11-06 22:00頃

BoardModel.cs

```
        public BoardModel()
        {
            this.Stones = new List<Stone>();
            for (int i = 0; i < this.GetCellCount(); i++)
            {
                // 初期値は 空点 で☆（＾～＾）
                this.Stones.Add(Stone.None);
            }

            // 1桁の数は、文字位置の調整がうまく行かないので勘で調整☆（＾～＾）
            this.RowNumbers = new List<string>()
            {
                "  1", "  2", "  3", "  4", "  5", "  6", "  7", "  8", "  9", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19"
            };

            // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
            this.ColumnNumbers = new List<string>()
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
                "L", "M", "N", "O", "P", "Q", "R", "S", "T"
            };
        }
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　可変盤はつらいよな☆　こういう変更に対応する作りにしないといけない☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　位置調整のために　半角空白まで入れるなんて……☆　いつの時代だぜ☆」

```
widget row-numbers.value = "  1", "  2", "  3", "  4", "  5", "  6", "  7", "  8", "  9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"
```

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　↑ダブルクォーテーションは必須だな☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　特別仕様が１個あるだけで　大変だぜ☆」

```
# 国際式の囲碁。19列。I列がない。
widget row-numbers.value = "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
widget column-numbers.value = "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T"

# 連珠。15列。
widget row-numbers.value = "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
widget column-numbers.value = "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o"
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　行番号は逆順に並べてくれだぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　ぬぎぎぎぎ！」

# 2019-11-06 23:00頃

![20191106wpf43.png](https://crieit.now.sh/upload_images/52e38857b76530a8add7ffd387ba16d35dc2d3eb3dde2.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　表示は　連珠盤　に近づいてきたが、 `i11` という入力はできないぜ☆
囲碁盤に i列 は無いからな☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　直せだぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　列番号の表示は　ただのラベルだからな……☆　コマンド入力を　ラベルに対応づけるのは　むずかしいが
やらなければ　入力で混乱する……☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　アルファベット１文字と、数字２文字　という構造なんだが……☆　こんなんエディットできないぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　その構造は保ってていいんじゃないの？」

# 2019-11-06 24:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　内部も、入力系も、最初に国際式の囲碁をベースにしたのが　全部　ダメの原因だぜ……☆
それをキャンセルして白紙状態に戻さないと　他に変換できないのが　無駄……☆」

![20191106wpf44.png](https://crieit.now.sh/upload_images/5f0bd041a2a0a453c4f4230bfdeefb625dc2e6ca3f654.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑星の位置も、国際囲碁式なのか、連珠式なのか　どちらで指定するのか　はっきりさせないといけない☆」

```
# 国際式の囲碁。19路盤。I列がない。
widget stars.value = "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4"

# 13路盤
widget stars.value = "D4", "G7", "K4", "D10", "K10"

# 9路盤
widget stars.value = "E5"

# 10路盤。将棋盤☆（＾～＾）
widget stars.value = "D4", "G4", "D7", "G7"

# 連珠。15列。
widget stars.value = "d4", "l4", "h8", "d12", "l12"
```

![20191106wpf45.png](https://crieit.now.sh/upload_images/b48bb78f9ec096b747143a7d099797575dc2ea59dddbe.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑連珠盤でけた☆　寝る☆（＾～＾）」

# 2019-11-07 19:15頃

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　もっと盤面って１行で入力できないものなの？」

```
board wwwww/bbbbb/...../wbwbw
```

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　↑例えば　白石が w で、黒石が b で、スペースを `.` にするのよ」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　フォーサイス記法をアレンジした感じか……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　１行に　こだわらなくてもいいんじゃないか☆？」

```
board 9 wwwww
board 8 bbbbb
board 7 .....
board 6 wbwbw
```

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　行ずつ指定する形にすれば　人間にも見やすいだろう☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　わらちゃんは天才ね！」

# 2019-11-07 20:30頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　いかの塩辛を食べた……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　次は　インスタント・コーヒーだな☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
（ず……ずずぃ……☆）　＃　インスタント・コーヒーを飲む音

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　行番号が分かったら、どうやって　セルのインデックスを取得するんだっけ☆？」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　`CellAddress` クラスになんかいいメソッド作ってないのかだぜ☆？」

# 2019-11-07 21:30頃

![20191107wpf46.png](https://crieit.now.sh/upload_images/a2ca472fd5c6cbcdbd021e2475c2a7ee5dc40d5ed1ccc.png)

```
board 19 bbbbbwwwww.....bbbb
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　はい、 `board` コマンドを実装したぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　`output.json` は JSONファイルなのに 入力はなんで `input.txt` の独自コマンドなんだぜ☆？
JSON で入力させろだぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　お父ん、ストレスで飛びそう☆」


output.txt:

```
{"board":{"rowSize":19,"columnSize":19,"stones":[1,1,1,1,1,2,2,2,2,2,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],"rowNumbers":["19","18","17","16","15","14","13","12","11","10","9","8","7","6","5","4","3","2","1"],"columnNumbers":["A","B","C","D","E","F","G","H","J","K","L","M","N","O","P","Q","R","S","T"],"starCellAddresses":["D16","K16","Q16","D10","K10","Q10","D4","K4","Q4"]},"state":{"intervalMsec":2000,"ply":0,"lastMoveIndex":-1,"blackName":"","blackTime":"","blackHama":0,"whiteName":"","whiteTime":"","whiteHama":0,"komi":0,"info":""}}
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　確かに　これを　そのまま　デシリアライズ　できればお得だが、そんなデータ構造してないしな☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　JSON に合わせたデータ構造にしたらよくない？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　設計のレベルから大改造だな……☆
`ApplicationDataModel` とかいうクラス名で　この構造を作るかだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　JSONは Java Script Object Notation だろ☆ 合わせるなら `ApplicationObjectModel` の方が近くないか☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　じゃあ　それで☆」

# 2019-11-07 22:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　もともと `State` と `BoardModel` しかないコードなんで　なんとかなりそうだが……☆」
 
![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ほとんどのメソッドが　グローバル変数を引数で渡されるという　とんでもない作りになったが　まあいいだろう☆」

```
JSON {ここにJSON}
```

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　↑こんな感じのコマンドを作りましょうよ」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　大文字４つのコマンドでいいのかだぜ☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　まあ　いいだろう、こんな　くそＧＵＩ　の仕様に　こだわらなくても☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　JSON をごそっと差し替えても、再描画が対応してない……☆
どこが変わったのか　部分ごとに　再描画しないといけないぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　めんどくさい作り　してんな……☆」

# 2019-11-07 23:00頃

[The New JSON Serializer In .NET Core 3](https://www.c-sharpcorner.com/article/the-new-json-serializer-in-net-core-3/)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　デシリアライズしても、JSON を読み込めてないぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　キャメル・ケースなんかに　したからでは☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　セッターが　プライベートだと　ダメなんじゃないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　カプセル化も　へったくれ　も無いな……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　アノテーションは無いのかだぜ☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　見つからんなぁ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　完全に復元するには、白アゲハマ・ウィジェットの visible とかも JSON に出しておかないといけないのか……☆」

# 2019-11-08 24:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ヌル例外が出て　スタック・トレースが無い……☆
何かを　数分かけて　ダウンロードしたら　出るようになったが……☆　今日は終わり☆」

# 2019-11-09 sat 09:45頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　今日は１時間だけ　何かしようぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　UIウィジェットは　画面上にずっと表示されっぱなしだから
リペイント・メソッドなんか　作ってないんだよな☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　気に入らねぇ☆！　表示されているものを　表示しようぜ☆！」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ウィジェットを　プログラムで付けたIDではなく、ユーザーが付けた名前で検索できる方法が必要だぜ☆
例えば……☆」

```
var widget = FindWidget("黒アゲハマ");
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑こういうメソッドが必要だぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　しかし JSON では お父んが付けた `b-hama` がウィジェット名になっているぜ☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　そっちも　ユーザーが設定できるようにしたいぜ☆」

[C#のWPFで名前からコントロールを取得する](https://araramistudio.jimdo.com/2016/12/05/wpfで名前からコントロールを取得する/)

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　↑名前でウィジェットを検索できるんじゃないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　Git hub でソースコード見て　名前を調べて
狙い撃ちで　開発者の想定しない操作を　されてしまう☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　洗濯機の中で　何　回ってんの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　出かける服だぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　何着て　出かけんの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　出かける服以外のやつしかないよな☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　フーン」


CanvasWidgetController.cs:


```
        private static Dictionary<string, string> nameDictionary = new Dictionary<string, string>()
            {
                { "ply", "plyCanvas" },
                { "move", "lastMoveCanvas" },
                { "b-name", "blackNameCanvas" },
                { "b-time", "blackTimeCanvas" },
                { "b-hama", "blackAgehamaCanvas" },
                { "w-name", "whiteNameCanvas" },
                { "w-time", "whiteTimeCanvas" },
                { "w-hama", "whiteAgehamaCanvas" },
                { "komi", "komiCanvas" },
                { "info", "infoCanvas" },
            };

        public static string GetNameBy(string widgetName)
        {
            if (nameDictionary.ContainsKey(widgetName))
            {
                return nameDictionary[widgetName];
            }

            return string.Empty;
        }
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　if文や switch文に頼って　キータイピングしまくるのは　悪いコーディングだぜ☆
↑このように　データは　構造に入れるのが　マスト　だぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　その name の使い道は オブジェクトを探して取ってくることしか無いんだから、
直接　オブジェクトを検索して返すところまで　やったら？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　そうしよ☆」

# 2019-11-09 sat 12:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　１時間遅刻したかと思ったんだが、２３時間　早かったようだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　戻ってこなくていいのに……☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ちょっとべつのブログを書くぜ☆（＾～＾）休憩☆（＾～＾）」

# 2019-11-09 sat 13:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　思ったんだが☆、」

```
widget row-numbers.value = "15", "14", "13", "12", "11"
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑これは☆」

```
set row-numbers = "15", "14", "13", "12", "11"
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑これでよくない☆？」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　プロパティの値の書式は　プログラム側がよろしく判定するということか☆　自動推論かだぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　パーサーが複雑になるじゃない」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　行番号は `Board` に、　プレイヤー名は `State` に、と分けているんだが、
これを `Widgets` に統一したいと思うんだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　name, value, visible の３属性をもつ構造体のことかだぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　それは Widget なのかなあ？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　じゃあ `Props` でいいだろ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　React の用語と混乱する☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　混乱しない用語とか　無いもんな……☆ `Properties` にするぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　Java の用語と混乱する☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　諦めろ☆」

```
set row-numbers = '15', '14', '13', '12', '11'
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　C# で開発していると　ダブル・クォーテーションのエスケープがめんどくさいんで、
スクリプトは　シングル・クォーテーション　にしたらどうだぜ☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　CSVの慣習に反するから　イケてないわねぇ」

```
"intervalMsec":{"name":"interval-msec","value":"2000","visible":true}
```

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　↑ `name` の内容を２回打ち込むのも イケてなくないか☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　それは　いけてないな……☆」

# 2019-11-09 sat 14:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　遅い昼飯を食べるかだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　空振りお出かけ　は止めろだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　JSON にも 数値型と 論理型があるだろ☆
全部　文字列型にしてしまうのは　イケてないのでは☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　それは　いけてないな……☆」

# 2019-11-09 sat 15:00頃

```
"column-numbers":{"value":"\u0022a\u0022, \u0022b\u0022, \u0022c\u0022, ...(omitted)
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`\u0022a` って何☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　ダブル・クォーテーションなんじゃないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　とほほほほ……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　string型のリストをシリアライズ、デシリアライズできないのかだぜ☆？」

# 2019-11-09 sat 16:00頃

output.json:

```
"column-numbers":{"value":["a","b","c","d","e","f","g","h","i","j","k","l","m","n","o"],"visible":true},
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`List<string>` 型もデシリアライズしてくれた……☆　ラッキー☆」

# 2019-11-09 sat 17:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　行番号に位置調整のために半角空白が入っていて、これをトリムしてはいけないというのが　つらい……☆」

# 2019-11-09 sat 18:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　大量のマジック・ナンバーを消している……☆」

output.json:

```
,"rowNumbersTrimed":["15","14","13","12","11","10","9","8","7","6","5","4","3","2","1"]
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑こういう隠しデータまで　JSON に出てしまうの　なんとかならないの☆？」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　プロパティーになってるんだろ☆　ゲッター・メソッドにしようぜ☆？」

output.json:

```
{"board":{"rowSize":15,"columnSize":15,"stones":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},"properties":{"column-numbers":{"value":["a","b","c","d","e","f","g","h","i","j","k","l","m","n","o"],"visible":true},"row-numbers":{"value":["15","14","13","12","11","10","  9","  8","  7","  6","  5","  4","  3","  2","  1"],"visible":true},"stars":{"value":["d4","l4","h8","d12","l12"],"visible":true},"interval-msec":{"value":2000,"visible":true},"ply":{"value":0,"visible":true},"move":{"value":0,"visible":true},"b-name":{"value":"Kifuwarabe","visible":true},"b-time":{"value":"00:00","visible":true},"b-hama":{"value":0,"visible":false},"w-name":{"value":"Warabemoti","visible":true},"w-time":{"value":"00:00","visible":true},"w-hama":{"value":0,"visible":false},"komi":{"value":6.5,"visible":false},"info":{"value":"","visible":true}}}
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑JSONは　こんな感じで　どうだぜ☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　`"visible":false` はデフォルトにして、JSONに出力しないようにできないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　調べてみるぜ☆」

[Hiding C# properties when serialize with JSON.NET](https://stackoverflow.com/questions/24306666/hiding-c-sharp-properties-when-serialize-with-json-net)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑常時隠すのは　あるのかも知れないが、ゲッター・メソッドでも　できるしな☆」

[How to serialize and deserialize JSON in .NET](https://docs.microsoft.com/ja-jp/dotnet/standard/serialization/system-text-json-how-to)
[Reducing Serialized JSON Size](https://www.newtonsoft.com/json/help/html/ReducingSerializedJSONSize.htm)
[DefaultValueAttribute Class](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.defaultvalueattribute?redirectedfrom=MSDN&view=netcore-3.0)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑ここに載ってなければ　なさそう……☆」

```
[DefaultValue(0.0)]
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　↑こういうの付けても　変わりなし☆」

# 2019-11-09 sat 19:00頃

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dc2a156b1dd7.gif)
「　`black` も `white` も小文字なのに、 `JSON` だけ　大文字なのは　やはり　気にかかるぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　細かいやつだな……☆　じゃあ `json` コマンドな☆」

# 2019-11-09 sat 20:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`set` コマンドが　バグった☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`.ToString()` が付け加えられて暗黙の文字列変換がされてしまうのは　困りものだぜ☆」

# 2019-11-09 sat 20:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　列番号を変更するのと、`black` で黒石を置くのを　同じテキストで実行すると　古い列番号で置かれてしまう☆」

```
set b-hama = 3
widget b-hama.visible = false
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`set` 構文と `widget` 構文の２つあるの大変なんで……☆、」

```
set b-hama = 3
set b-hama.value = 3
set b-hama.visible = false
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　`set` 構文に統一しようぜ☆？ `.` が無ければ `.value` が補完されるということで☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dc2a2f671baf.gif)
「　そうしましょう！」

# 2019-11-09 sat 21:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　ラムダ式の構文の練習をしている☆」

# 2019-11-09 sat 22:00～23:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　関数型プログラミングで if 分岐したあとの return 書くとき　頭が沸騰するぜ☆」

# 2019-11-10 sat 00:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　パーサーを　ラムダ式計算に書き換えたんで
`set` 構文と `widget` 構文の統一をやってみるぜ☆」

# 2019-11-10 sat 01:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　まだ途中☆（＾～＾）」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　石と、ウィジェットが　異なるコードで　同じ命令でも異なる働きをするんだよな☆（＾～＾）」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　１行ずつ分けてループで回しながらパースするように変更☆
コマンドを分けないと実行できなかったバグが取れたぜ☆」

![20191110wpf47.png](https://crieit.now.sh/upload_images/e2a0885fe0a773d3a3cbdbddb61e91b75dc6ef3bb9678.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　見た目は変わっていないが、バグが取れたぜ☆（＾～＾）
Ｃ＃で　ラムダ計算風に書いたプログラムのサンプルとしても使えるだろう☆」

```
{"board":{"rowSize":15,"columnSize":15,"stones":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},"properties":{"column-numbers":{"value":["a","b","c","d","e","f","g","h","i","j","k","l","m","n","o"],"visible":true},"row-numbers":{"value":["15","14","13","12","11","10","  9","  8","  7","  6","  5","  4","  3","  2","  1"],"visible":true},"stars":{"value":["d4","l4","h8","d12","l12"],"visible":true},"interval-msec":{"value":2000,"visible":true},"ply":{"value":0,"visible":true},"move":{"value":"g9","visible":true},"b-name":{"value":"Kifuwarabe","visible":true},"b-time":{"value":"00:00","visible":true},"b-hama":{"value":0,"visible":false},"w-name":{"value":"Warabemoti","visible":true},"w-time":{"value":"00:00","visible":true},"w-hama":{"value":0,"visible":false},"komi":{"value":6.5,"visible":false},"info":{"value":"","visible":true}}}
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dc2a1340659b.gif)
「　JSONもすっきりしただろ☆」

＜書きかけ＞