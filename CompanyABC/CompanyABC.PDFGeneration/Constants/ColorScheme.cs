﻿using MigraDoc.DocumentObjectModel;

namespace CompanyABC.PDFGeneration.Constants
{
    public static class ColorScheme
    {
        public readonly static Color TableBorder = new Color(81, 125, 192);
        public readonly static Color TableBlue = new Color(235, 240, 249);
        public readonly static Color TableGray = new Color(242, 242, 242);
        public readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
        public readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
        public readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
    }
}
