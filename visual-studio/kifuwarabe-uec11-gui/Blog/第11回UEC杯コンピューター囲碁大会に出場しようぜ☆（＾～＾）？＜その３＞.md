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
「　行番号のウィジェットなんて　どうすればいいんだぜ☆？」

```
widget row-number.value = A,B,C,D,E,F,G,H,J,K,L,M,N,O,P
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


＜書きかけ＞

