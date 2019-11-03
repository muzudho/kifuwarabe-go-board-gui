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
|8|152|J|8|
|9|171|K|9|
|10|190|L|10|
|11|209|M|11|
|12|228|N|12|
|13|247|O|13|
|14|266|P|14|
|15|285|Q|15|
|16|304|R|16|
|17|323|S|17|
|18|342|T|18|

![KIFUWARABE_80x100x8_01_Futu.gif](https://crieit.now.sh/upload_images/5ac9fa3b390b658160717a7c1ef5008a5dbe2d3d81958.gif)
「　↑何だぜ　この表は☆？」

＜書きかけ＞