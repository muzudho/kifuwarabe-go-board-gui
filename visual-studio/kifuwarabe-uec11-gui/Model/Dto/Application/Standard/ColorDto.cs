using System;

namespace KifuwarabeGoBoardGui.Model.Dto
{
    public class ColorDto
    {
        /// <summary>
        /// 透明☆（＾～＾）
        /// </summary>
        private static ColorDto transparent = new ColorDto(0, 0, 0, 0);

        /// <summary>
        /// 白☆（＾～＾）
        /// </summary>
        private static ColorDto white = new ColorDto(255, 255, 255, 255);

        /// <summary>
        /// 黒☆（＾～＾）
        /// </summary>
        private static ColorDto black = new ColorDto(255, 0, 0, 0);

        public ColorDto(byte a, byte r, byte g, byte b)
        {
            this.A = a;
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static ColorDto Transparent => transparent;
        public static ColorDto White => white;
        public static ColorDto Black => black;

        /// <summary>
        /// 透明度。
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// 赤み。
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// 緑み。
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// 青み。
        /// </summary>
        public byte B { get; set; }

        public bool Equals(ColorDto opponent)
        {
            if (opponent==null)
            {
                throw new ArgumentNullException(nameof(opponent));
            }

            return this.A == opponent.A &&
                this.R == opponent.R &&
                this.G == opponent.G &&
                this.B == opponent.B;
        }
    }
}
