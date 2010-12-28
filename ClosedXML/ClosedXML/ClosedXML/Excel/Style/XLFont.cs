﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ClosedXML.Excel
{
    internal class XLFont : IXLFont
    {
        IXLStylized container;
        public XLFont()
            : this(null, XLWorkbook.DefaultStyle.Font)
        {

        }
        public XLFont(IXLStylized container, IXLFont defaultFont)
        {
            this.container = container;
            if (defaultFont != null)
            {
                bold = defaultFont.Bold;
                italic = defaultFont.Italic;
                underline = defaultFont.Underline;
                strikethrough = defaultFont.Strikethrough;
                verticalAlignment = defaultFont.VerticalAlignment;
                shadow = defaultFont.Shadow;
                fontSize = defaultFont.FontSize;
                fontColor = defaultFont.FontColor;
                fontName = defaultFont.FontName;
                fontFamilyNumbering = defaultFont.FontFamilyNumbering;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Bold.ToString());
            sb.Append("-");
            sb.Append(Italic.ToString());
            sb.Append("-");
            sb.Append(Underline.ToString());
            sb.Append("-");
            sb.Append(Strikethrough.ToString());
            sb.Append("-");
            sb.Append(VerticalAlignment.ToString());
            sb.Append("-");
            sb.Append(Shadow.ToString());
            sb.Append("-");
            sb.Append(FontSize.ToString());
            sb.Append("-");
            sb.Append(FontColor.Color.ToHex());
            sb.Append("-");
            sb.Append(FontName);
            sb.Append("-");
            sb.Append(FontFamilyNumbering.ToString());
            return sb.ToString();
        }

        #region IXLFont Members

        private Boolean bold;
        public Boolean Bold
        {
            get
            {
                return bold;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.Bold = value);
                else
                    bold = value;
            }
        }

        private Boolean italic;
        public Boolean Italic
        {
            get
            {
                return italic;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.Italic = value);
                else
                    italic = value;
            }
        }

        private XLFontUnderlineValues underline;
        public XLFontUnderlineValues Underline
        {
            get
            {
                return underline;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.Underline = value);
                else
                    underline = value;
            }
        }

        private Boolean strikethrough;
        public Boolean Strikethrough
        {
            get
            {
                return strikethrough;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.Strikethrough = value);
                else
                    strikethrough = value;
            }
        }

        private XLFontVerticalTextAlignmentValues verticalAlignment;
        public XLFontVerticalTextAlignmentValues VerticalAlignment
        {
            get
            {
                return verticalAlignment;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.VerticalAlignment = value);
                else
                    verticalAlignment = value;
            }
        }

        private Boolean shadow;
        public Boolean Shadow
        {
            get
            {
                return shadow;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.Shadow = value);
                else
                    shadow = value;
            }
        }

        private Double fontSize;
        public Double FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.FontSize = value);
                else
                    fontSize = value;
            }
        }

        private IXLColor fontColor;
        public IXLColor FontColor
        {
            get
            {
                return fontColor;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.FontColor = value);
                else
                    fontColor = value;
            }
        }

        private String fontName;
        public String FontName
        {
            get
            {
                return fontName;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.FontName = value);
                else
                    fontName = value;
            }
        }

        private XLFontFamilyNumberingValues fontFamilyNumbering;
        public XLFontFamilyNumberingValues FontFamilyNumbering
        {
            get
            {
                return fontFamilyNumbering;
            }
            set
            {
                if (container != null && !container.UpdatingStyle)
                    container.Styles.ForEach(s => s.Font.FontFamilyNumbering = value);
                else
                    fontFamilyNumbering = value;
            }
        }

        #endregion

        public Double GetWidth(String text)
        {
            if (StringExtensions.IsNullOrWhiteSpace(text))
                return 0;

            System.Drawing.Font stringFont = new System.Drawing.Font(fontName, (float)fontSize);
            return GetWidth(stringFont, text);
        }
        private static readonly Graphics g = Graphics.FromImage(new Bitmap(200, 200));
        private Double GetWidth(System.Drawing.Font stringFont, string text)
        {
            System.Drawing.Font drawfont = new System.Drawing.Font(fontName, (float)fontSize);
            Int32 charWidth = (Int32)g.MeasureString("X", drawfont).Width;
            return 2 + Math.Truncate((text.Length * charWidth) / charWidth * 256.0) / 256.0 - 0.71;
        }

        public Double GetHeight()
        {
            System.Drawing.Font stringFont = new System.Drawing.Font(fontName, (float)fontSize);
            return GetHeight(stringFont);
        }

        private Double GetHeight(System.Drawing.Font stringFont)
        {
            Size textSize = TextRenderer.MeasureText("X", stringFont);
            var val = (double)textSize.Height * 0.85;
            return val;
        }


        public Boolean Equals(IXLFont other)
        {
            return
                   this.Bold.Equals(other.Bold)
                && this.Italic.Equals(other.Italic)
                && this.Underline.Equals(other.Underline)
                && this.Strikethrough.Equals(other.Strikethrough)
                && this.VerticalAlignment.Equals(other.VerticalAlignment)
                && this.Shadow.Equals(other.Shadow)
                && this.FontSize.Equals(other.FontSize)
                && this.FontColor.Equals(other.FontColor)
                && this.FontName.Equals(other.FontName)
                && this.FontFamilyNumbering.Equals(other.FontFamilyNumbering)
                ;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((XLFont)obj);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Bold.GetHashCode();
                hash = hash * 23 + Italic.GetHashCode();
                hash = hash * 23 + Underline.GetHashCode();
                hash = hash * 23 + Strikethrough.GetHashCode();
                hash = hash * 23 + VerticalAlignment.GetHashCode();
                hash = hash * 23 + Shadow.GetHashCode();
                hash = hash * 23 + FontSize.GetHashCode();
                hash = hash * 23 + FontColor.GetHashCode();
                hash = hash * 23 + FontName.GetHashCode();
                hash = hash * 23 + FontFamilyNumbering.GetHashCode();
                return hash;
            }
        }
    }
}