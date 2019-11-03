# 2019-11-03 10:30頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　眠……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　寝坊は良いことだぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　１マスずつ指定するの　めんどうなんだけど。
`j10-k10` みたいに指定できないの？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　`-` は引き算に見えるので、Excelを真似て `j10:k10` だな☆
これもパーサーを作るとすれば `RangeCell` クラスとか作るといいかも知れないな☆」

[kifuwarabe-uec11-gui](https://github.com/muzudho/kifuwarabe-uec11-gui)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　ソースは ↑Git hub の方を見てくれだぜ☆」


CellAddress.cs:


```
        public static int ToIndex(int rowNumber, int columnNumber)
        {
            return (19 - rowNumber) * 19 + columnNumber;
        }

        public int ToIndex()
        {
            return ToIndex(this.RowAddress.Number, this.ColumnAddress.Number);
        }
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　行番号、列番号から配列のインデックスを求める式なんだが、
static関数に抽出しておいた方が　応用が効くよな☆」


```
        public static CellAddress FromIndex(int rowNumber, int columnNumber)
        {
            return new CellAddress(new RowAddress(rowNumber), new ColumnAddress(columnNumber));
        }

        public static CellAddress FromIndex(int index)
        {
            var rowNumber = index / 19;
            var columnNumber = index % 19;
            return new CellAddress(new RowAddress(rowNumber), new ColumnAddress(columnNumber));
        }
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　行番号、列番号から セル番地オブジェクトを作る static関数があると便利☆
引数は　行、列　の並び順に変更した☆」


# 2019-11-03 12:00頃


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　国際式の囲碁盤は　I列　が欠番なので、　行番号　が下から上に並んでるので、
Excel のつもりでプログラム書くと　混乱するんだが☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　国際式の囲碁盤なんか覚えても　何も嬉しくないわよ！
内部は Excel 式に作って、入出力だけ　国際式の囲碁盤のフィルターをかましなさい」


ExcelGoBard.cs:


```
namespace kifuwarabe_uec11_gui.Script
{
    /// <summary>
    /// Excel式の囲碁盤。
    /// </summary>
    public static class ExcelGoBoard
    {
        public static CellAddress ToInternationalGoCellAddress(CellAddress cellAddress)
        {
            return cellAddress;
        }

        public static RowAddress ToInternationalGoRowAddres(RowAddress rowAddress)
        {
            return rowAddress;
        }

        public static ColumnAddress ToInternationalGoColumnAddres(ColumnAddress columnAddress)
        {
            return columnAddress;
        }
    }
}
```


InternationalGoBoard.cs


```
namespace kifuwarabe_uec11_gui.Script
{
    /// <summary>
    /// 国際式囲碁（International go）の囲碁盤
    /// </summary>
    public static class InternationalGoBoard
    {
        public static CellAddress ToExcelCellAddress(CellAddress cellAddress)
        {
            return cellAddress;
        }

        public static RowAddress ToExcelRowAddres(RowAddress rowAddress)
        {
            return rowAddress;
        }

        public static ColumnAddress ToExcelColumnAddres(ColumnAddress columnAddress)
        {
            return columnAddress;
        }
    }
}
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　まず　透過フィルターを作り、ここに　変換コードを　集めていくぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　あっ、ダメだぜ☆
言語処理系は　コードが　ぶわーっと爆発的に増えていくんで、
メモリ節約よりも　コーディング量節約、
実行速度よりも　キータイピング速度の方を優先しなくてはいけないぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　じゃあ　昼飯抜きな☆」

# 2019-11-03 13:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　ご飯は炊く☆」

# 2019-11-03 14:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
（カタ　カタ　カタ　カタ）　＃　キータイピングの音。
「　…………☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　なんで　ご飯　できてんのに食べないんだぜ☆？」


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
（すっ）　＃　キータイピングを止める擬音。


Output:


```
c7:e9           | C7 D7 E7 C8 D8 E8 C9 D9 E9.
e9:c7           | E9 D9 C9 E8 D8 C8 E7 D7 C7.
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　よし、 Excel式の範囲表記で Foreach 回すのでけた☆　飯食べよ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　ブログに貼り付けるのに適さないコード量わらう」

# 2019-11-03 15:00頃

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　コーヒーだろ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　くれだぜ☆」


```
black k10 a7:b10 m12 e1:e13 f17
```


![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　↑コンポジション（混合）な書き方って　できないの？」


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　そのように作れば　できるが……☆　その場合　大で小を兼ねるようにする☆
例えば……☆」


```
black k10:k10 a7:b10 m12:m12 e1:e13 f17:f17
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑上のような　書き方を認めることだぜ☆
`k10` は、 `k10:k10` の省略形ということにするんだぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　認めましょう！」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　お父ん、そろそろ　テストコードを書けだぜ☆
目視で確認できる分量じゃなくなっただろ☆」

[.NET Coreで単体テストを行う](https://www.buildinsider.net/language/dotnetcore/07)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑じゃあ MSTest で☆」

# 2019-11-03 16:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
（せっ　せっ　せっ）　＃　忙しい擬音。

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　テストコードに移行中か☆」

![20191103wpf16.png](https://crieit.now.sh/upload_images/c331c3533e1283b8caf0b78119945db75dbe804fde087.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　よっしゃ、まずまず　移行でけたぜ☆」

# 2019-11-03 17:00頃

![20191103wpf17.png](https://crieit.now.sh/upload_images/1c8b4181776923b7c36d7e162551e2be5dbe8d5e8569c.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　これで　`input.txt`　ファイルを保存すれば　5秒以内に　囲碁盤の `a2:b1` などの範囲、
盤の左下あたりだな、ここらへんの石が　空白に変わるはずだぜ☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　わたしが合図をしたら　保存してくれだぜ☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　もう保存した☆」

![20191103wpf17a1.png](https://crieit.now.sh/upload_images/d3137ce713ce087bcd5e7a86a54d01c95dbe8dcd6ffde.png)

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　上下が　逆じゃない？　それに　１９行ではなく　１８行から石が消えてるから　１　ずれてるわよ」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　じゃあ　上下を逆にして　１ずらせば　直るんだぜ☆」

# 2019-11-03 18:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　逆さ、直らんなあ☆」

![20191103wpf19.png](https://crieit.now.sh/upload_images/a5614633f290bb4a6381b8ab842efc455dbe9de601954.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑もう１回試しだぜ☆」

![20191103wpf19b1.png](https://crieit.now.sh/upload_images/369e01bb0eb8435399d05c3db9d9da615dbe9e018984f.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑ええいっ　くそっ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　なんで　そんなところが消えるの？」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　きふわらべ　が始まっているな☆」


Excel like:

|Row|Index|Column|Index|
|---|---|---|---|
|0|0|A|0|
|1|19|B|1|
|2|38|C|2|
|3|57|D|3|
|4|76|E|4|
|5|95|F|5|
|6|114|G|6|
|7|133|H|7|
|8|152|I|8|
|9|171|J|9|
|10|190|K|10|
|11|209|L|11|
|12|228|M|12|
|13|247|N|13|
|14|266|O|14|
|15|285|P|15|
|16|304|Q|16|
|17|323|R|17|
|18|342|S|18|


![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　↑何だぜ　この表は☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　がっつり　テストしようぜ☆？」

# 2019-11-03 19:00頃

![20191103wpf20b1.png](https://crieit.now.sh/upload_images/21b227e0037be04b5027b136593316ae5dbea79ab8098.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑static関数を使うと　継承した子クラスでも　ここだけ継承せず　親のメソッド使う　みたいになるのか☆
static 外そ……☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　さっきの表は Excel式 の盤で、国際式囲碁だと　行が　ひっくり返るのかだぜ☆」

International go:

|Row|Index|Column|Index|
|---|---|---|---|
|0|342|A|0|
|1|323|B|1|
|2|304|C|2|
|3|285|D|3|
|4|266|E|4|
|5|247|F|5|
|6|228|G|6|
|7|209|H|7|
|8|190|J|8|
|9|171|K|9|
|10|152|L|10|
|11|133|M|11|
|12|114|N|12|
|13|95|O|13|
|14|76|P|14|
|15|57|Q|15|
|16|38|R|16|
|17|19|S|17|
|18|0|T|18|

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　じゃあ　書いたテスト　間違ってるだろ☆」

# 2019-11-03 20:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　上下を　ひっくり返し過ぎているのかも知れない☆
見直そうぜ☆」

# 2019-11-03 20:30頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　１つ気づいたことがあるぜ☆　`H7`　は 7 行目だが、 0 で始まる行数で数えると 6 だぜ☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　はい、はい！　何年プログラムやってんの？！」


Can be read in Z-Shaped:


```
|Row |Index|Column|Index|
|----|---|---|---|
| "1"|  0|A| 0|
| "2"| 19|B| 1|
| "3"| 38|C| 2|
| "4"| 57|D| 3|
| "5"| 76|E| 4|
| "6"| 95|F| 5|
| "7"|114|G| 6|
| "8"|133|H| 7|
| "9"|152|I| 8|
|"10"|171|J| 9|
|"11"|190|K|10|
|"12"|209|L|11|
|"13"|228|M|12|
|"14"|247|N|13|
|"15"|266|O|14|
|"16"|285|P|15|
|"17"|304|Q|16|
|"18"|323|R|17|
|"19"|342|S|18|
```


International Go:


```
|Row |Index|Column|Index|
|----|---|---|---|
| "1"|342|A| 0|
| "2"|323|B| 1|
| "3"|304|C| 2|
| "4"|285|D| 3|
| "5"|266|E| 4|
| "6"|247|F| 5|
| "7"|228|G| 6|
| "8"|209|H| 7|
| "9"|190|J| 8|
|"10"|171|K| 9|
|"11"|152|L|10|
|"12"|133|M|11|
|"13"|114|N|12|
|"14"| 95|O|13|
|"15"| 76|P|14|
|"16"| 57|Q|15|
|"17"| 38|R|16|
|"18"| 19|S|17|
|"19"|  0|T|18|
```


![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　こう表記しなおそう☆」


```
reversedRow = 19 - row
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑あと、行番号をひっくり返すとき　こう書けばいいかと思っていたんだが……☆」


```
reversedRow = 18 - row
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑0 から始まるので こうなのでは☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　はい、はい、はい！　何年プログラムやってんの？！
マジック・ナンバーも　定数にしてしまいなさい！」

# 2019-11-03 21:00頃

![20191103wpf21.png](https://crieit.now.sh/upload_images/01b1667496d7fa4f496e5eaef81498c95dbec433ad684.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑３回目ぐらいの挑戦だぜ☆」

![20191103wpf21b1.png](https://crieit.now.sh/upload_images/d97039187eeddf99bb6120517b5cdc335dbec44e798b3.png)

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　↑変わりないわね」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　お父ん☆　手こずるなら　囲碁プログラミングで　手こずろうぜ☆？
パーサーに時間費やしても　何にもならないぜ☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　何にもなれた人に尊敬できる人物はいない☆　何にもならないところを進めるのは　わたしだぜ☆」

![20191103wpf22.png](https://crieit.now.sh/upload_images/a34827bd4c44068587d888539957416c5dbec603649da.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑４回目ぐらいの挑戦だぜ☆
内部的には　テキスト読みと同じく　Z字方向にインデックスが振られている☆
内部的な計算で　上下ひっくり返すのは　要らないかもしれない☆　処理を省いてみたぜ☆」

![20191103wpf22b1.png](https://crieit.now.sh/upload_images/c31e8d456a999b66ab5fe702be166b935dbec65375837.png)

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　うーん☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　うーん？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　うーん☆？」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　あー、わかったぜ☆
コマンドを読み取るパーサーが　Z字方向式　を使っている☆　これを　国際囲碁式　に変えよう☆」

![20191103wpf23.png](https://crieit.now.sh/upload_images/7ff6393f04e81909ef30a072f1d3bedb5dbec8ec535ae.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑５回目ぐらいの挑戦だぜ☆」

![20191103wpf23b1.png](https://crieit.now.sh/upload_images/f40317c387327dde67545c20684b96d85dbec9f701cbc.png)

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　で、いつになったら　囲碁プログラミングを始めるんだぜ☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　これで　囲碁の盤面は　作れるようになったのかしら？！

![20191103wpf24.png](https://crieit.now.sh/upload_images/1496f4917c7ef08daed10e6761fa559a5dbecf19d8e64.png)

input.txt

```
space a19:t1
white e15:p5
white c13:d7
white f16:n16
white g4:n4
white q13:r8
black g13:j11
black m14:o10
black f7:k7
black j6:n6
black e18:f15
black o18:r15
```

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑コマンドライン形式の　お絵描きソフトが完成した☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　くそっ☆！」

# 2019-11-03 22:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　黒白　交互に置いて　手数を進めてくれるコマンドが欲しいよな☆」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　`Play` コマンドでいいんじゃないの？！

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　禁じ手や、石を取るルールを組み込むのは　まだ　むずかしいんで……☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　大会の１か月前に　のんびり　している☆」

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　最後に着手した石の上に　赤い四角　を置くぐらいは　したいな……☆」

# 2019-11-03 23:00頃

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　画面の再描画処理の　うまい方法が分からん……☆　雑に書き進めるか……☆」

![20191103wpf26.png](https://crieit.now.sh/upload_images/dead39bb81da0f4f71725a779ece63195dbee086116ba.png)

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　`set agehama 10` コマンドも作ってしまったらどうだぜ☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　あんたら何で GUI 作ってんのよ！　さっさと　思考部　作りなさいよ！」

[How to: Paint an Area with a Linear Gradient](https://docs.microsoft.com/ja-jp/dotnet/framework/wpf/graphics-multimedia/how-to-paint-an-area-with-a-linear-gradient)
[WPFで画像の周囲に境界線を付けるにはどうすればよいですか。](https://codeday.me/jp/qa/20190125/168395.html)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑Xaml があれば　ゲーム画面　簡単に作れるんじゃないのか……☆
グラデーションを描く方法も調べよう……☆」

![20191103wpf27.png](https://crieit.now.sh/upload_images/ac2ed908dc0d6b61bd73700e1200b17d5dbeeab224ae7.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑簡単に　角丸の欄が描ける☆　もっと調整すれば　かっこよく　なりそう☆」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　連珠、囲碁というより　麻雀のＵＩっぽくないか☆？」

![OKAZAKI_Yumemi_80x80x8_02_Syaberu.gif](https://crieit.now.sh/upload_images/058791c2dd4c1604ce1bd9ec26d490ae5dbe2d6e24eb1.gif)
「　長方形にするから　麻雀牌っぽくなるのよ！　丸にしなさい、
ちっがーう！　GUI に凝るのは　沼なのよ！」

# 2019-11-04 00:00頃

![20191103wpf28.png](https://crieit.now.sh/upload_images/fa6cfd68c075357cfcf44fa411d0ce5e5dbeef87ed5a0.png)

![KITASHIRAKAWA_Chiyuri_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/3da2d4690cf2c3f101c5cbc0e48729f55dbe2d1941edb.gif)
「　↑こりゃ　面白いぜ☆！」

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　寝ればいいのに……☆」

＜書きかけ＞