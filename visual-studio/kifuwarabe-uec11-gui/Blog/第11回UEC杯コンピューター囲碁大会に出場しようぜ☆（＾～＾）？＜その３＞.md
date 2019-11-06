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

＜書きかけ＞

