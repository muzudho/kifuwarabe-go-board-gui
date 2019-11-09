namespace UnitTestProject1
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using KifuwarabeUec11Gui.Controller;

    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// �ȒP�Ȏ��s���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            var boardModel = new BoardModel();

            /*
            // �ȒP�Ȏ��s���i�O�`�O�j
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i=0; i<100; i++)
            {
                builder.Append($"{i},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */

            //*
            // �ȒP�Ȏ��s���i�O�`�O�j
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < boardModel.ColumnSize; i++)
            {
                builder.Append($"{i * boardModel.ColumnSize},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */
        }

        /// <summary>
        /// �v���p�e�B�l�̐ݒ���e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgumentTest()
        {
            Assert.AreEqual("b-name = Kifuwarabe", SetsInstructionArgument.Parse("set b-name = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-time = 10:00", SetsInstructionArgument.Parse("set  b-time  =  10:00  ", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-hama = ", SetsInstructionArgument.Parse("set b-hama =", 3).Item1?.ToDisplay());
        }

        /// <summary>
        /// ���ێ��͌�̃Z���Ԓn�\�L���e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestColorInstructionArgumentTest()
        {
            var model = new ApplicationObjectModel();

            Assert.AreEqual("A19 K1 T1", ColorInstructionArgument.Parse("black A19 K1 T1", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("B19 K2 S1", ColorInstructionArgument.Parse("white B19 K2 S1", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("C19 K3 R1", ColorInstructionArgument.Parse("space C19 K3 R1", 5, model).Item1?.ToDisplay(model));

            // �����^���i�O�`�O�j
            Assert.AreEqual("A2:B1 C4:D3 E5", ColorInstructionArgument.Parse("space A2:B1 C4:D3 E5", 5, model).Item1?.ToDisplay(model));

            // �啶���E�������͋�ʂ��邺���i�O�`�O�j
            Assert.AreNotEqual("A19 K1 T1", ColorInstructionArgument.Parse("black a19 k1 t1", 5, model).Item1?.ToDisplay(model));
        }

        /// <summary>
        /// ���ێ��͌�̃Z���Ԓn�\�L���e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestInternationalCellRange()
        {
            var model = new ApplicationObjectModel();

            // �Ƃ肠�������̃e�X�g�̃X�^�[�g��0�ɑ����Ă��������i�O�`�O�j
            var start = 0;

            Assert.AreEqual(5, CellRange.Parse("C7:E9", start, model, (matched, curr)=>
            {
                Assert.AreEqual("C7:E9", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, CellRange.Parse("E9:C7", start, model, (matched, curr)=>
            {
                Assert.AreEqual("E9:C7", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // �Z�k�\�L���i�O�`�O�j
            Assert.AreEqual(5, CellRange.Parse("F5:F5", start, model, (matched, curr)=>
            {
                Assert.AreEqual("F5", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // �����C���f�b�N�X���m�F���i�O�`�O�j
            /*
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
             */
            {
                var indexes = new List<int>();

                // I��͖������Ƃɒ��Ӂ��i�O�`�O�j�I
                // �E���オ�聙�i�O�`�O�j
                CellRange.Parse("H7:K9", start, model, (matched, curr) =>
                {
                    Assert.AreEqual("H7:K9", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (index) =>
                    {
                        indexes.Add(index);
                    });

                    return curr;
                });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I��͖������Ƃɒ��Ӂ��i�O�`�O�j�I
                CellRange.Parse("K9:H7", start, model, (matched, curr)=>
                {
                    Assert.AreEqual("K9:H7", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (index) =>
                    {
                        indexes.Add(index);
                    });

                    return curr;
                });

                // K9 J9 H9 K8 J8 H8 K7 J7 H7
                Assert.AreEqual("199 198 197 218 217 216 237 236 235", string.Join(' ', indexes));
            }

            // �\�L�m�F���i�O�`�O�j
            {
                var signs = new List<string>();

                // I��͖������Ƃɒ��Ӂ��i�O�`�O�j�I
                CellRange.Parse("H7:K9", start, model, (matched, curr)=>
                {
                    Assert.AreEqual("H7:K9", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (indexO0) =>
                    {
                        signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
                    });

                    return curr;
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I��͖������Ƃɒ��Ӂ��i�O�`�O�j�I
                CellRange.Parse("K9:H7", start, model, (matched, curr)=>
                {
                    Assert.AreEqual("K9:H7", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (indexO0) =>
                    {
                        signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
                    });

                    return curr;
                });

                Assert.AreEqual("K9 J9 H9 K8 J8 H8 K7 J7 H7", string.Join(' ', signs));
            }
        }

        /// <summary>
        /// ���ێ��͌�̃Z���Ԓn�\�L���e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestInternationalCellAddress()
        {
            var appModel = new ApplicationObjectModel();

            // �Ƃ肠�������̃e�X�g�̃X�^�[�g��0�ɑ����Ă��������i�O�`�O�j
            var start = 0;

            // 2�����i�O�`�O�j
            var list1 = new List<string>()
            {
                "A1","B2","C3",                          "D4",                          "E5",                          "F6",                          "G7",                          "H8",                          "J9"
            };

            foreach (var item in list1)
            {
                Assert.AreEqual(2, CellAddress.Parse(item, start, appModel, (matched, curr) =>
                {
                    Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                    if (matched != null)
                    {
                        return curr;
                    }
                    else
                    {
                        return start;
                    }
                }));
            }

            // 3�����i�O�`�O�j
            var list2 = new List<string>()
            {
                "K10",
                "L11",
                "M12",
                "N13",
                "O14",
                "P15",
                "Q16",
                "R17",
                "S18",
                "T19"
            };

            foreach (var item in list2)
            {
                Assert.AreEqual(3, CellAddress.Parse(item, start, appModel, (matched, curr) =>
                {
                    Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                    if (matched == null)
                    {
                        return start;
                    }

                    return curr;
                }));
            }

            // �啶���E�������͋�ʂ��邺���i�O�`�O�j�����Z�b�g�̗�ԍ��ɏ������͖��������i�O�`�O�j
            Assert.AreEqual(start, CellAddress.Parse("a1", 0, appModel, (matched, curr)=>
            {
                Assert.IsNull(matched?.ToDisplayTrimed(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // �啶���E�������͋�ʂ��邺���i�O�`�O�j
            Assert.AreEqual(3, CellAddress.Parse("T19", 0, appModel, (matched, curr)=>
            {
                Assert.AreNotEqual("t19", matched?.ToDisplayTrimed(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));
        }

        /// <summary>
        /// ���ێ��͌�̗�ԍ��̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            var model = new ApplicationObjectModel();

            Assert.AreEqual("A", ColumnAddress.Parse("A", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("B", ColumnAddress.Parse("B", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("C", ColumnAddress.Parse("C", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("D", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 3, model).Item1?.ToDisplay(model));
            Assert.AreEqual("E", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 4, model).Item1?.ToDisplay(model));
            Assert.AreEqual("F", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("G", ColumnAddress.Parse("G", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("H", ColumnAddress.Parse("H", 0, model).Item1?.ToDisplay(model));

            // I is a null.
            Assert.IsNull(ColumnAddress.Parse("I", 0, model).Item1?.ToDisplay(model));

            Assert.AreEqual("J", ColumnAddress.Parse("J", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("K", ColumnAddress.Parse("K", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("L", ColumnAddress.Parse("L", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("M", ColumnAddress.Parse("M", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("N", ColumnAddress.Parse("N", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("O", ColumnAddress.Parse("O", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("P", ColumnAddress.Parse("P", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("Q", ColumnAddress.Parse("Q", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("R", ColumnAddress.Parse("R", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("S", ColumnAddress.Parse("S", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("T", ColumnAddress.Parse("T", 0, model).Item1?.ToDisplay(model));
        }

        /// <summary>
        /// ���ێ��͌�̍s�ԍ����e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestRowAddress()
        {
            var model = new ApplicationObjectModel();

            // �C���f�b�N�X�m�F���i�O�`�O�j�����I�ɂ͍s�ԍ��́@�Ђ�����Ԃ��Ă��邺���i�O�`�O�j
            Assert.AreEqual(18, RowAddress.Parse("1", 0, model).Item1?.NumberO0);
            Assert.AreEqual(17, RowAddress.Parse("2", 0, model).Item1?.NumberO0);
            Assert.AreEqual(16, RowAddress.Parse("3", 0, model).Item1?.NumberO0);
            Assert.AreEqual(15, RowAddress.Parse("1234", 3, model).Item1?.NumberO0);
            Assert.AreEqual(14, RowAddress.Parse("12345", 4, model).Item1?.NumberO0);
            Assert.AreEqual(13, RowAddress.Parse("123456", 5, model).Item1?.NumberO0);
            Assert.AreEqual(12, RowAddress.Parse("7", 0, model).Item1?.NumberO0);
            Assert.AreEqual(11, RowAddress.Parse("8", 0, model).Item1?.NumberO0);
            Assert.AreEqual(10, RowAddress.Parse("9", 0, model).Item1?.NumberO0);
            Assert.AreEqual(9, RowAddress.Parse("10", 0, model).Item1?.NumberO0);
            Assert.AreEqual(8, RowAddress.Parse("11", 0, model).Item1?.NumberO0);
            Assert.AreEqual(7, RowAddress.Parse("12", 0, model).Item1?.NumberO0);
            Assert.AreEqual(6, RowAddress.Parse("12345678901213", 12, model).Item1?.NumberO0);
            Assert.AreEqual(5, RowAddress.Parse("123456789012314", 13, model).Item1?.NumberO0);
            Assert.AreEqual(4, RowAddress.Parse("1234567890123415", 14, model).Item1?.NumberO0);
            Assert.AreEqual(3, RowAddress.Parse("16", 0, model).Item1?.NumberO0);
            Assert.AreEqual(2, RowAddress.Parse("17", 0, model).Item1?.NumberO0);
            Assert.AreEqual(1, RowAddress.Parse("18", 0, model).Item1?.NumberO0);
            Assert.AreEqual(0, RowAddress.Parse("19", 0, model).Item1?.NumberO0);

            // �\�L�m�F���i�O�`�O�j
            Assert.AreEqual("1", RowAddress.Parse("1", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("2", RowAddress.Parse("2", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("3", RowAddress.Parse("3", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("4", RowAddress.Parse("1234", 3, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("5", RowAddress.Parse("12345", 4, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("6", RowAddress.Parse("123456", 5, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("7", RowAddress.Parse("7", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("8", RowAddress.Parse("8", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("9", RowAddress.Parse("9", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("10", RowAddress.Parse("10", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("11", RowAddress.Parse("11", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("12", RowAddress.Parse("12", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("13", RowAddress.Parse("12345678901213", 12, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("14", RowAddress.Parse("123456789012314", 13, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("15", RowAddress.Parse("1234567890123415", 14, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("16", RowAddress.Parse("16", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("17", RowAddress.Parse("17", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("18", RowAddress.Parse("18", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("19", RowAddress.Parse("19", 0, model).Item1?.ToDisplayTrimed(model));
        }

        /// <summary>
        /// �P�ꊮ�S��v�̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestExactlyKeyword()
        {
            // �Ƃ肠�������̃e�X�g�̃X�^�[�g��0�ɑ����Ă��������i�O�`�O�j
            var start = 0;

            // �P�ꊮ�S��v�̃e�X�g���i�O�`�O�j
            Assert.AreEqual(5, ExactlyKeyword.Parse("black", "black", start, (matched, curr) =>
            {
                Assert.AreEqual("black", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, ExactlyKeyword.Parse("white", "white", start, (matched, curr) =>
            {
                Assert.AreEqual("white", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, ExactlyKeyword.Parse("start", "start", start, (matched, curr) =>
            {
                Assert.AreEqual("start", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // �z���C�g�X�y�[�X�̃e�X�g���i*�O�`�O*�j
            Assert.AreEqual(5, WhiteSpace.Parse("     ", start, (matched, curr) =>
            {
                Assert.AreEqual("     ", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // �ŏ��Ƀ}�b�`����P��̃e�X�g���i�O�`�O�j
            Assert.AreEqual(5, Word.Parse("black a19 k10 t1", 0, (matched, curr) =>
            {
                Assert.AreEqual("black", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(12, WordUpToDelimiter.Parse("!", "Hello, world!", 0, (matched, curr) =>
            {
                Assert.AreEqual("Hello, world", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));
        }

        /// <summary>
        /// ��ԍ��̃e�X�g���i�O�`�O�j
        /// </summary>
        [TestMethod]
        public void TestColumnNumbers()
        {
            var model = new ApplicationObjectModel();
            var columnNumbers = model.Properties[ColumnNumbersController.OutsideName].ToTextList();

            Assert.AreEqual(0, columnNumbers.IndexOf("A"));
            Assert.AreEqual(1, columnNumbers.IndexOf("B"));
            Assert.AreEqual(2, columnNumbers.IndexOf("C"));
            Assert.AreEqual(3, columnNumbers.IndexOf("D"));
            Assert.AreEqual(4, columnNumbers.IndexOf("E"));
            Assert.AreEqual(5, columnNumbers.IndexOf("F"));
            Assert.AreEqual(6, columnNumbers.IndexOf("G"));
            Assert.AreEqual(7, columnNumbers.IndexOf("H"));
            Assert.AreNotEqual(8, columnNumbers.IndexOf("I"));
            Assert.AreEqual(8, columnNumbers.IndexOf("J"));
            Assert.AreEqual(9, columnNumbers.IndexOf("K"));
            Assert.AreEqual(10, columnNumbers.IndexOf("L"));
            Assert.AreEqual(11, columnNumbers.IndexOf("M"));
            Assert.AreEqual(12, columnNumbers.IndexOf("N"));
            Assert.AreEqual(13, columnNumbers.IndexOf("O"));
            Assert.AreEqual(14, columnNumbers.IndexOf("P"));
            Assert.AreEqual(15, columnNumbers.IndexOf("Q"));
            Assert.AreEqual(16, columnNumbers.IndexOf("R"));
            Assert.AreEqual(17, columnNumbers.IndexOf("S"));
            Assert.AreEqual(18, columnNumbers.IndexOf("T"));
        }
    }
}
